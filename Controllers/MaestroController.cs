using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using APIControlEscolar.Models;
using APIControlEscolar.Data;
using Microsoft.AspNetCore.Authorization;

namespace APIControlEscolar.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class MaestroController : ControllerBase
{
    private readonly CONTROL_ESCOLAR _context;
    private readonly ILogger<MaestroController> _logger;

    public MaestroController(CONTROL_ESCOLAR context, ILogger<MaestroController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var maestros = await _context.Maestros
              //  .Include(m => m.IdEstadoNavigation)
             //   .Include(m => m.IdMunicipioNavigation)
            //    .Include(m => m.IdTurnoNavigation)
                .ToListAsync();

            return Ok(maestros);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener maestros: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("grupos")]
    public async Task<IActionResult> GetGruposConAlumnos()
    {
        // Supón que ya obtienes el IdMaestro del token JWT
        var claim = User.Claims.FirstOrDefault(c => c.Type == "maestroId");
        if (claim == null)
            return Unauthorized("No se encontró el ID del maestro en el token.");

        int maestroId = int.Parse(claim.Value);

        var grupos = await _context.Grupos
            .Where(g => g.IdMaestro == maestroId)
            .Include(g => g.Alumnos)
            .ToListAsync();

        return Ok(grupos);
    }

    [HttpGet("{numeroEmpleado}")]
    public async Task<IActionResult> GetByNumeroEmpleado(string numeroEmpleado)
    {
        try
        {
            var maestro = await _context.Maestros
               // .Include(m => m.IdEstadoNavigation)
              //  .Include(m => m.IdMunicipioNavigation)
              //  .Include(m => m.IdTurnoNavigation)
                .FirstOrDefaultAsync(m => m.NumeroEmpleado == numeroEmpleado);

            if (maestro == null)
            {
                return NotFound(new { error = "Maestro no encontrado" });
            }

            return Ok(maestro);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener maestro por número de empleado {numeroEmpleado}: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Maestro maestro)
    {
        try
        {
            if (maestro == null || string.IsNullOrWhiteSpace(maestro.NumeroEmpleado))
            {
                return BadRequest(new { error = "El número de empleado es obligatorio." });
            }

            var existe = await _context.Maestros.AnyAsync(m => m.NumeroEmpleado == maestro.NumeroEmpleado);
            if (existe)
            {
                return Conflict(new { error = "Ya existe un maestro con ese número de empleado." });
            }

            // Verificar relaciones
            var errores = new List<string>();

            if (!await _context.Estados.AnyAsync(e => e.IdEstado == maestro.IdEstado)) errores.Add("Estado");
            if (!await _context.Municipios.AnyAsync(m => m.IdMunicipio == maestro.IdMunicipio)) errores.Add("Municipio");
            if (maestro.IdTurno.HasValue && !await _context.Turnos.AnyAsync(t => t.IdTurno == maestro.IdTurno)) errores.Add("Turno");

            if (errores.Any())
            {
                return BadRequest(new { error = $"No existen las siguientes entidades relacionadas: {string.Join(", ", errores)}." });
            }

            _context.Maestros.Add(maestro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByNumeroEmpleado), new { numeroEmpleado = maestro.NumeroEmpleado }, maestro);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear maestro: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpPut("{numeroEmpleado}")]
    public async Task<IActionResult> Update(string numeroEmpleado, [FromBody] Maestro maestro)
    {
        try
        {
            if (maestro == null || numeroEmpleado != maestro.NumeroEmpleado)
            {
                return BadRequest(new { error = "Datos inválidos o el número de empleado no coincide." });
            }

            var existingMaestro = await _context.Maestros.FirstOrDefaultAsync(m => m.NumeroEmpleado == numeroEmpleado);
            if (existingMaestro == null)
            {
                return NotFound(new { error = "Maestro no encontrado" });
            }

            existingMaestro.Nombre = maestro.Nombre;
            existingMaestro.ApellidoPaterno = maestro.ApellidoPaterno;
            existingMaestro.ApellidoMaterno = maestro.ApellidoMaterno;
            existingMaestro.FechaNacimiento = maestro.FechaNacimiento;
            existingMaestro.Genero = maestro.Genero;
            existingMaestro.Telefono = maestro.Telefono;
            existingMaestro.Direccion = maestro.Direccion;
            existingMaestro.CodigoPostal = maestro.CodigoPostal;
            existingMaestro.IdMunicipio = maestro.IdMunicipio;
            existingMaestro.IdEstado = maestro.IdEstado;
            existingMaestro.Especialidad = maestro.Especialidad;
            existingMaestro.IdTurno = maestro.IdTurno;
            existingMaestro.EstadoMaestro = maestro.EstadoMaestro;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Maestro actualizado con éxito" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar maestro {numeroEmpleado}: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{numeroEmpleado}")]
    public async Task<IActionResult> Delete(string numeroEmpleado)
    {
        try
        {
            var maestro = await _context.Maestros.FirstOrDefaultAsync(m => m.NumeroEmpleado == numeroEmpleado);
            if (maestro == null)
            {
                return NotFound(new { error = "Maestro no encontrado" });
            }

            _context.Maestros.Remove(maestro);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Maestro eliminado con éxito" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar maestro {numeroEmpleado}: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }
}
