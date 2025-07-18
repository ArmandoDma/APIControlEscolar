using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControlEscolar.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly CONTROL_ESCOLAR _context;
        private readonly IJwtService _jwtService;

        public AsistenciaController(CONTROL_ESCOLAR context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarAsistencia([FromBody] RegistrarAsistenciaRequest request)
        {
            // Obtén rol y ID desde JWT
            var rol = _jwtService.ObtenerRolDesdeToken();
            var perfilId = _jwtService.ObtenerPerfilIdDesdeToken();

            if (rol != "1") 
            {
                return Unauthorized(new { error = "Solo los alumnos pueden registrar asistencia." });
            }

            if (string.IsNullOrWhiteSpace(perfilId))
            {
                return Unauthorized(new { error = "No se pudo determinar el ID del alumno desde el token." });
            }

            int idAlumno = int.Parse(perfilId);

            // Busca el token válido
            var token = await _context.AsistenciaTokens
                .FirstOrDefaultAsync(t => t.Token == request.Token && t.IsActive && t.ExpiresAt > DateTime.UtcNow);

            if (token == null)
                return BadRequest("Token inválido o expirado.");

            // Evita duplicados en la misma fecha para el mismo token
            var hoy = DateTime.UtcNow.Date;
            var yaRegistrado = await _context.Asistencias.AnyAsync(a =>
                a.IdAlumno == idAlumno &&
                a.FechaHora.Date == hoy &&
                a.IdToken == token.IdToken);

            if (yaRegistrado)
                return BadRequest("Asistencia ya registrada para hoy con este token.");

            // Registra asistencia
            var asistencia = new Asistencia
            {
                IdAlumno = idAlumno,
                IdToken = token.IdToken,
                FechaHora = DateTime.UtcNow
            };

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Asistencia registrada correctamente." });
        }

        public class RegistrarAsistenciaRequest
        {
            public string Token { get; set; } = null!;
        }

    }
}
