using APIControlEscolar.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControlEscolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesAlumnoController : Controller
    {
        private readonly CONTROL_ESCOLAR _context;
        private readonly IJwtService _jwtService;
        private readonly ILogger<CoursesAlumnoController> _logger;

        public CoursesAlumnoController(CONTROL_ESCOLAR context, IJwtService jwtService, ILogger<CoursesAlumnoController> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("materias")]
        public async Task<IActionResult> ObtenerMateriasConAlumnos()
        {
            try
            {
                var alumnoIdStr = _jwtService.ObtenerPerfilIdDesdeToken();
                if (!int.TryParse(alumnoIdStr, out int alumnoId) || alumnoId == 0)
                {
                    return Unauthorized(new { mensaje = "Token inválido o alumnoId incorrecto." });
                }

                var alumno = await _context.Alumnos
                    .FirstOrDefaultAsync(a => a.IdAlumno == alumnoId);

                if (alumno == null)
                    return NotFound(new { mensaje = "Alumno no encontrado." });

                var materias = await _context.Materias
                    .Where(m =>
                        m.IdGrado == alumno.IdGrado &&
                        m.IdGrupo == alumno.IdGrupo &&
                        m.IdCarrera == alumno.IdCarrera
                    )
                    .Select(m => new
                    {
                        m.IdMateria,
                        m.NombreMateria,
                        m.ImageMat,
                        Maestro = _context.Maestros
                            .Where(ma => ma.IdMaestro == m.IdMaestro)
                            .Select(ma => ma.Nombre + " " + ma.ApellidoPaterno + " " + ma.ApellidoMaterno)
                            .FirstOrDefault() ?? "Sin asignar",
                        Alumnos = _context.Alumnos
                            .Where(a =>
                                a.IdGrado == alumno.IdGrado &&
                                a.IdGrupo == alumno.IdGrupo &&
                                a.IdCarrera == alumno.IdCarrera // <-- mismo filtro
                            )
                            .Select(a => new {
                                a.IdAlumno,
                                a.Nombre,
                                a.ApellidoPaterno,
                                a.ApellidoMaterno
                            }).ToList()
                    }).ToListAsync();

                return Ok(materias);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener materias con alumnos: {ex.Message}");
                return StatusCode(500, new { error = "Ocurrió un error al obtener las materias con alumnos." });
            }
        }
    }
}
