using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AlumnoController : ControllerBase
{
    private readonly CONTROL_ESCOLAR _context;
    private readonly ILogger<AlumnoController> _logger;

    public AlumnoController(CONTROL_ESCOLAR context, ILogger<AlumnoController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var alumnos = await _context.Alumnos.ToListAsync();
            return Ok(alumnos);
        }
        catch (Exception ex)
        {
            _logger.LogError($"❌ Error al obtener alumnos: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{matricula}")]
    public async Task<IActionResult> GetByMatricula(string matricula)
    {
        try
        {
            var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Matricula == matricula);
            if (alumno == null)
            {
                return NotFound(new { error = "Alumno no encontrado" });
            }
            return Ok(alumno);
        }
        catch (Exception ex)
        {
            _logger.LogError($"❌ Error al obtener alumno por matrícula {matricula}: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Alumno alumno)
    {
        try
        {
            if (alumno == null || string.IsNullOrWhiteSpace(alumno.Matricula))
            {
                return BadRequest(new { error = "Matrícula y datos obligatorios no deben estar vacíos." });
            }

            var existe = await _context.Alumnos.AnyAsync(a => a.Matricula == alumno.Matricula);
            if (existe)
            {
                return Conflict(new { error = "Ya existe un alumno con esa matrícula." });
            }

            // Verificar entidades relacionadas
            var errores = new List<string>();

            if (!await _context.Estados.AnyAsync(e => e.IdEstado == alumno.IdEstado)) errores.Add("Estado");
            if (!await _context.Municipios.AnyAsync(m => m.IdMunicipio == alumno.IdMunicipio)) errores.Add("Municipio");
            if (!await _context.Carreras.AnyAsync(c => c.IdCarrera == alumno.IdCarrera)) errores.Add("Carrera");
            if (!await _context.Turnos.AnyAsync(t => t.IdTurno == alumno.IdTurno)) errores.Add("Turno");
            if (!await _context.Grados.AnyAsync(g => g.IdGrado == alumno.IdGrado)) errores.Add("Grado");
            if (!await _context.Grupos.AnyAsync(g => g.IdGrupo == alumno.IdGrupo)) errores.Add("Grupo");
            if (!await _context.Periodos.AnyAsync(p => p.IdPeriodo == alumno.IdPeriodo)) errores.Add("Periodo");

            if (errores.Any())
            {
                return BadRequest(new { error = $"No existen las siguientes entidades relacionadas: {string.Join(", ", errores)}." });
            }

            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByMatricula), new { matricula = alumno.Matricula }, alumno);
        }
        catch (Exception ex)
        {
            _logger.LogError($"❌ Error al crear alumno: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }



    [HttpPut("{matricula}")]
    public async Task<IActionResult> Update(string matricula, [FromBody] Alumno alumno)
    {
        try
        {
            if (alumno == null || matricula != alumno.Matricula)
            {
                return BadRequest(new { error = "Datos inválidos o la matrícula no coincide." });
            }

            var existingAlumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Matricula == matricula);
            if (existingAlumno == null)
            {
                return NotFound(new { error = "Alumno no encontrado" });
            }

            existingAlumno.Nombre = alumno.Nombre;
            existingAlumno.ApellidoPaterno = alumno.ApellidoPaterno;
            existingAlumno.ApellidoMaterno = alumno.ApellidoMaterno;
            existingAlumno.Genero = alumno.Genero;
            existingAlumno.FechaNacimiento = alumno.FechaNacimiento;
            existingAlumno.Telefono = alumno.Telefono;
            existingAlumno.Direccion = alumno.Direccion;
            existingAlumno.CodigoPostal = alumno.CodigoPostal;
            existingAlumno.IdMunicipio = alumno.IdMunicipio;
            existingAlumno.IdEstado = alumno.IdEstado;
            existingAlumno.IdCarrera = alumno.IdCarrera;
            existingAlumno.IdTurno = alumno.IdTurno;
            existingAlumno.EstadoAlumno = alumno.EstadoAlumno;
            existingAlumno.IdGrado = alumno.IdGrado;
            existingAlumno.IdGrupo = alumno.IdGrupo;
            existingAlumno.IdPeriodo = alumno.IdPeriodo;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Alumno actualizado con éxito" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"❌ Error al actualizar alumno {matricula}: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{matricula}")]
    public async Task<IActionResult> Delete(string matricula)
    {
        try
        {
            var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Matricula == matricula);
            if (alumno == null)
            {
                return NotFound(new { error = "Alumno no encontrado" });
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Alumno eliminado con éxito" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"❌ Error al eliminar alumno {matricula}: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }
}
