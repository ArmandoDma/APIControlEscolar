using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIControlEscolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly CONTROL_ESCOLAR _context;

        public AdminsController(CONTROL_ESCOLAR context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] Admins admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetAdmin), new { id = admin.IdAdmin }, admin);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Admins>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }
    }
}
