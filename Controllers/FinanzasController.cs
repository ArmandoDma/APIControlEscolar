using APIControlEscolar.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APIControlEscolar.Controllers
{
    [EnableCors("_myCorsPolicy")]
    [Route("api/auth")]
    [ApiController]
    public class FinanzasController : Controller
    {
        private readonly CONTROL_ESCOLAR _context;
        private readonly IJwtService _jwtService;

        public FinanzasController(CONTROL_ESCOLAR context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Obtiene la situación financiera del alumno autenticado.
        /// </summary>
        [HttpGet("finanzas")]
        public async Task<IActionResult> GetSituacionFinanciera()
        {
            try
            {
                // Obtener el alumnoId desde el token
                var alumnoIdStr = _jwtService.ObtenerPerfilIdDesdeToken(); // Esto debería ser un string


                if (!int.TryParse(alumnoIdStr, out int alumnoId) || alumnoId == 0)
                {
                    return Unauthorized(new { mensaje = "Token inválido o formato de alumnoId incorrecto" });
                }

                // Consultar los pagos del alumno
                var pagos = await _context.Pagos
                    .Where(p => p.IdAlumno == alumnoId)
                    .OrderByDescending(p => p.FechaPago)
                    .Select(p => new
                    {
                        p.Monto,
                        p.FechaPago,
                        p.Descripcion
                    })
                    .ToListAsync();

                if (!pagos.Any())
                {
                    return NotFound("No se encontraron pagos para este alumno.");
                }

                // Calcular el total pagado
                var totalPagado = pagos.Sum(p => p.Monto);

                var resultado = new
                {
                    AlumnoId = alumnoId,
                    TotalPagado = totalPagado,
                    Pagos = pagos
                };

                return Ok(resultado);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}


