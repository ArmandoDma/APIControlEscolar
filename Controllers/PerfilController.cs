using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControlEscolar.Controllers
{

    [EnableCors("_myCorsPolicy")]
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly CONTROL_ESCOLAR _context;
        private readonly IJwtService _jwtService;

        public PerfilController(CONTROL_ESCOLAR context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Obtiene el perfil del alumno autenticado.
        /// </summary>
        [HttpGet("perfil")]
        public async Task<IActionResult> GetPerfil()
        {
            try
            {
                var perfilId = _jwtService.ObtenerPerfilIdDesdeToken();
                var rolClaim = _jwtService.ObtenerRolDesdeToken();

                if (string.IsNullOrEmpty(perfilId) || string.IsNullOrEmpty(rolClaim))
                {
                    return Unauthorized("Token inválido o incompleto.");
                }

                if (!int.TryParse(rolClaim, out int rolId))
                {
                    return Unauthorized("Rol inválido.");
                }


                if (rolId == 1) // Alumno
                {
                    var alumno = await _context.Alumnos
                        .Include(a => a.Usuario)
                        .Include(c => c.Carrera)
                        .FirstOrDefaultAsync(a => a.IdAlumno.ToString() == perfilId);

                    if (alumno == null)
                        return NotFound("Alumno no encontrado.");

                    Console.WriteLine($"Carrera.Id: {alumno.Carrera?.IdCarrera}");
                    Console.WriteLine($"Carrera.Nombre: {alumno.Carrera?.NombreCarrera}");

                    return Ok(new
                    {
                        NombreCompleto = $"{alumno.Nombre} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}",
                        Telefono = alumno.Telefono,
                        Direccion = alumno.Direccion,
                        Matricula = alumno.Matricula,
                        Correo = alumno.Usuario?.Email,
                        Carrera = alumno.Carrera?.NombreCarrera,
                        Rol = "Estudiante",
                        Imagen = alumno.ImageAlumno
                    });
                }
                else if (rolId == 2) // Maestro
                {
                    var maestro = await _context.Maestros
                        .Include(m => m.Usuario)                        
                        .FirstOrDefaultAsync(m => m.IdMaestro.ToString() == perfilId);

                    if (maestro == null)
                        return NotFound("Maestro no encontrado.");

                    return Ok(new
                    {
                        NombreCompleto = $"{maestro.Nombre} {maestro.ApellidoPaterno} {maestro.ApellidoMaterno}",
                        Telefono = maestro.Telefono,
                        Direccion = maestro.Direccion,                        
                        Matricula = maestro.NumeroEmpleado,
                        Correo = maestro.Usuario?.Email,
                        Rol = "Maestro",
                        Imagen = maestro.ImageMaestro
                        
                    });
                }
                else if (rolId == 3)
                {
                    var admin = await _context.Admins
                        .Include(m => m.Usuario)
                        .FirstOrDefaultAsync(m => m.IdAdmin.ToString() == perfilId);

                    if (admin == null)
                        return NotFound("Maestro no encontrado.");

                    return Ok(new
                    {
                        NombreCompleto = $"{admin.Nombre} {admin.ApellidoPaterno} {admin.ApellidoMaterno}",
                        Telefono = admin.Telefono,
                        Direccion = admin.Direccion,
                        Matricula = admin.IdAdmin.ToString(),
                        Correo = admin.Usuario?.Email,
                        Rol = "Admin",
                        Imagen = admin.ImageAdmin
                    });
                }
                else
                {
                    return Unauthorized("Rol no permitido.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // 📌 Actualizar información personal del alumno
        [HttpPut("updateperfil")]
        public async Task<IActionResult> UpdatePerfil([FromBody] Alumno alumnoActualizado)
        {
            var alumnoId = _jwtService.ObtenerPerfilIdDesdeToken();
            if (alumnoId == null)
            {
                return Unauthorized("No se pudo obtener el ID del alumno.");
            }

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.IdAlumno.ToString() == alumnoId);
            if (alumno == null)
            {
                return NotFound("Alumno no encontrado.");
            }

            // Actualizar datos permitidos
            alumno.Nombre = alumnoActualizado.Nombre;
            alumno.ApellidoPaterno = alumnoActualizado.ApellidoPaterno;
            alumno.ApellidoMaterno = alumnoActualizado.ApellidoMaterno;
            alumno.Telefono = alumnoActualizado.Telefono;
            alumno.Direccion = alumnoActualizado.Direccion;

            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();

            return Ok("Perfil actualizado con éxito.");
        }

    }
}
