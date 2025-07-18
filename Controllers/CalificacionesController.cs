using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using APIControlEscolar;
using APIControlEscolar.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using APIControlEscolar.Models;


[EnableCors("_myCorsPolicy")]
[Route("api/auth")]
[ApiController]
public class CalificacionesController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly CONTROL_ESCOLAR _context;
    private readonly ILogger<CalificacionesController> _logger;

    // Inyectamos el JwtService y ApplicationDbContext
    public CalificacionesController(IJwtService jwtService, CONTROL_ESCOLAR context, ILogger<CalificacionesController> logger)
    {
        _jwtService = jwtService;
        _context = context;
        _logger = logger;
    }

    
    ///  Obtiene las calificaciones del alumno.
    [HttpGet("calificaciones")]
    public async Task<IActionResult> ObtenerCalificaciones()
    {
        try
        {
            // Obtener el alumnoId desde el token
            var alumnoIdStr = _jwtService.ObtenerPerfilIdDesdeToken(); // Esto debería ser un string
            

            if (!int.TryParse(alumnoIdStr, out int alumnoId) || alumnoId == 0)
            {
                return Unauthorized(new { mensaje = "Token inválido o formato de alumnoId incorrecto" });
            }

            // Obtener calificaciones junto con datos adicionales
            var calificaciones = await _context.Calificaciones
               // .Include(c => c.IdMateriaNavigation)
                //.Include(c => c.)
                //.Include(c => c.IdAlumnoNavigation)
                //.ThenInclude(a => a.IdPeriodoNavigation)
                .Where(c => c.IdAlumno == alumnoId)
                .Select(c => new
                {
                    //Grado = c.IdAlumnoNavigation.IdGradoNavigation.NombreGrado,
                    //Grupo = c.IdAlumnoNavigation.IdGrupoNavigation.NombreGrupo,
                    //Periodo = c.IdAlumnoNavigation.IdPeriodoNavigation.NombrePeriodo,
                   // Materia = c.IdMateriaNavigation.NombreMateria,
                //    Maestro = $"{c.Maestro.Nombre} {c.Maestro.ApellidoPaterno} {c.Maestro.ApellidoMaterno}",
                    Parcial1 = c.CalificacionParcial1,
                    Parcial2 = c.CalificacionParcial2,
                    Parcial3 = c.CalificacionParcial3,
                    Final = c.CalificacionFinal,
                    Materia = c.IdMateria
                })
                .ToListAsync();

            return Ok(calificaciones);
        }
        catch (Exception ex)
        {
            _logger.LogError($" Error al obtener calificaciones: {ex.Message}");
            return StatusCode(500, new { error = "Ocurrió un error al obtener las calificaciones." });
        }
    }

    //POST Calif
    [Authorize(Roles = "Maestro")]
    [HttpPost("calificaciones")]
    public async Task<IActionResult> GuardarCalificacion([FromBody] Calificacione calificacion)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (calificacion.CalificacionParcial1.HasValue &&
                calificacion.CalificacionParcial2.HasValue &&
                calificacion.CalificacionParcial3.HasValue)
            {
                calificacion.CalificacionFinal = Math.Round(
                    (calificacion.CalificacionParcial1.Value +
                     calificacion.CalificacionParcial2.Value +
                     calificacion.CalificacionParcial3.Value) / 3, 2);
            }
            else
            {
                calificacion.CalificacionFinal = null;
            }

            calificacion.Fecha_Registro = DateTime.UtcNow;

            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Calificación registrada correctamente." });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al guardar calificación: {ex.Message}");
            return StatusCode(500, new { error = "Ocurrió un error al guardar la calificación." });
        }
    }


}
