using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControlEscolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerasController : ControllerBase
    {
        private readonly CONTROL_ESCOLAR _context;

        public CarrerasController(CONTROL_ESCOLAR context)
        {
            _context = context;
        }

        // GET: api/carreras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrera>>> GetCarreras()
        {
            return await _context.Carreras.ToListAsync();
        }

        // GET: api/carreras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarrera(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera == null)
            {
                return NotFound();
            }

            return carrera;
        }

        // POST: api/carreras
        [HttpPost]
        public async Task<ActionResult<Carrera>> CreateCarrera(Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarrera), new { id = carrera.IdCarrera }, carrera);
        }

        // PUT: api/carreras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrera(int id, Carrera carrera)
        {
            if (id != carrera.IdCarrera)
            {
                return BadRequest("El ID de la carrera no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(carrera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/carreras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrera(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.IdCarrera == id);
        }
    }
}