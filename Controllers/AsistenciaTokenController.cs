using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Authorization;
using APIControlEscolar;

[Route("api/[controller]")]
[ApiController]
public class AsistenciaTokenController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly CONTROL_ESCOLAR _context;

    public AsistenciaTokenController(CONTROL_ESCOLAR context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    // Obtener tokens activos (ej: para frontend generar QR)
    [HttpGet("activos")]
    public async Task<IActionResult> GetActiveTokens()
    {
        var rol = _jwtService.ObtenerRolDesdeToken();
        var maestroId = _jwtService.ObtenerPerfilIdDesdeToken();

        if (rol != "2") // Maestro
            return Unauthorized(new { error = "Solo los maestros pueden consultar sus tokens." });

        var tokens = await _context.AsistenciaTokens
            .Where(t => t.IsActive && t.ExpiresAt > DateTime.UtcNow && t.IdMaestro == int.Parse(maestroId!))
            .ToListAsync();

        return Ok(tokens);
    }


    [Authorize]
    [HttpPost("generar")]
    public async Task<IActionResult> GenerateToken([FromBody] GenerarTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NumeroEmpleado))
            return BadRequest("El número de empleado es obligatorio.");
        
        var maestro = await _context.Maestros
            .FirstOrDefaultAsync(m => m.NumeroEmpleado == request.NumeroEmpleado);

        if (maestro == null)
            return BadRequest("Número de empleado no válido.");
        
        var maestroIdString = _jwtService.ObtenerPerfilIdDesdeToken();
        if (string.IsNullOrEmpty(maestroIdString))
            return Unauthorized("No se pudo obtener el ID del maestro desde el token.");

        int idMaestroFromToken = int.Parse(maestroIdString);
        
        if (maestro.IdMaestro != idMaestroFromToken)
            return Unauthorized("El número de empleado no coincide con el usuario autenticado.");
        
        var tokenString = Guid.NewGuid().ToString();
        var now = DateTime.UtcNow;
        var expiration = now.AddMinutes(1);

        var newToken = new AsistenciaToken
        {
            Token = tokenString,
            CreatedAt = now,
            ExpiresAt = expiration,
            IsActive = true,
            IdMaestro = maestro.IdMaestro
        };

        _context.AsistenciaTokens.Add(newToken);
        await _context.SaveChangesAsync();

        return Ok(newToken);
    }

    //to unactive token
    public class DesactivarTokenRequest
    {
        public string Token { get; set; }
    }

    [HttpPost("desactivar")]
    public async Task<IActionResult> DesactivarToken([FromBody] DesactivarTokenRequest request)
    {
        if (string.IsNullOrEmpty(request.Token))
            return BadRequest("Token es obligatorio.");

        var existingToken = await _context.AsistenciaTokens
            .FirstOrDefaultAsync(t => t.Token == request.Token && t.IsActive);

        if (existingToken == null)
            return NotFound("Token no encontrado o ya inactivo.");

        existingToken.IsActive = false;
        await _context.SaveChangesAsync();

        return Ok();
    }
}
