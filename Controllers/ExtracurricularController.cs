using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControlEscolar.Controllers
{
    [EnableCors("_myCorsPolicy")]
    [Route("api/extracurricular")]
    [ApiController]
    public class ExtracurricularController : ControllerBase
    {
        private readonly CONTROL_ESCOLAR _context;
        private readonly IJwtService _jwtService;

        public ExtracurricularController(CONTROL_ESCOLAR context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Obtiene todas las actividades (Eventos y Talleres).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetActividades([FromQuery] string? tipo)
        {
            IQueryable<Extracurricular> query = _context.Extracurricular;

            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(a => a.Tipo == tipo);
            }

            var actividades = await query.ToListAsync();
            return Ok(actividades);
        }

        /// <summary>
        /// Crea una nueva actividad.
        /// Solo administradores pueden crear actividades.
        /// </summary>
        [HttpPost("extra")]
        public async Task<IActionResult> CrearActividad([FromBody] Extracurricular nuevaActividad)
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

                if (rolId != 3)
                {
                    return Unauthorized("No tienes permisos para crear actividades.");
                }

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(nuevaActividad.Nombre))
                    return BadRequest("El nombre es obligatorio.");

                if (nuevaActividad.Tipo == "Evento" && nuevaActividad.Precio == null)
                    return BadRequest("Un evento debe tener precio.");

                if (nuevaActividad.Tipo == "Taller")
                    nuevaActividad.Precio = 0;

                _context.Extracurricular.Add(nuevaActividad);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Actividad creada con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Edita una actividad existente.
        /// Solo administradores pueden editar.
        /// </summary>
        [HttpPut("extra/{id}")]
        public async Task<IActionResult> EditarActividad(int id, [FromBody] Extracurricular actividadActualizada)
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

                if (rolId != 3) // Solo Admin
                {
                    return Unauthorized("No tienes permisos para editar actividades.");
                }

                var actividad = await _context.Extracurricular.FirstOrDefaultAsync(a => a.Id == id);
                if (actividad == null)
                {
                    return NotFound("Actividad no encontrada.");
                }

                // Actualizar datos básicos
                actividad.Nombre = actividadActualizada.Nombre;
                actividad.Categoria = actividadActualizada.Categoria;
                actividad.Descripcion = actividadActualizada.Descripcion;
                actividad.FechaInicio = actividadActualizada.FechaInicio;
                actividad.FechaFin = actividadActualizada.FechaFin;
                actividad.Cupo = actividadActualizada.Cupo;
                actividad.Lugar = actividadActualizada.Lugar;
                actividad.Tipo = actividadActualizada.Tipo;
                actividad.ImagenEvent = actividadActualizada.ImagenEvent;                

                if (actividad.Tipo == "Evento")
                {
                    if (actividadActualizada.Precio == null)
                        return BadRequest("Un evento debe tener precio.");
                    actividad.Precio = actividadActualizada.Precio;
                }
                else if (actividad.Tipo == "Taller")
                {
                    actividad.Precio = 0; // Siempre gratis
                }

                _context.Extracurricular.Update(actividad);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Actividad actualizada con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
