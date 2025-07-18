using APIControlEscolar.Data;
using APIControlEscolar.Models;
using APIControlEscolar.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace APIControlEscolar.Controllers
{
    //[EnableCors("_myCorsPolicy")]
   // [Route("api/auth")]
    //[ApiController]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;
        private readonly CONTROL_ESCOLAR _context;
        public AuthController(IAuthService authService, IJwtService jwtService, CONTROL_ESCOLAR context, ILogger<AuthController> logger)
        {
            _authService = authService;
            _jwtService = jwtService;
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Registra un nuevo usuario y devuelve un token de autenticación.
        /// </summary>
        [HttpPost("registro")]
        public async Task<IActionResult> Register([FromBody] RegistroRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest(new { error = "El correo y la contraseña son obligatorios." });
                }

                var newUser = await _authService.RegisterUserAsync(request);
                
                int userId = newUser.IdAlumno ?? newUser.IdMaestro ?? 0;
                
                var token = await _jwtService.GenerarTokenConId(userId, newUser.Email, newUser.IdRol);

                return Ok(new { message = "Usuario registrado con éxito", token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en registro: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// 📌 Inicia sesión y devuelve un token de autenticación si las credenciales son correctas.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || request.IdRol <= 0)
                {
                    return BadRequest(new { error = "Asegurese de que los datos se estan enviando correctamente." });
                }

                var token = await _authService.AuthenticateAsync(request.Email, request.Password, request.IdRol);

                if (token == null)
                {
                    return Unauthorized(new { error = "Credenciales incorrectas" });
                }

                return Ok(new { message = "Inicio de sesión exitoso", token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en login: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
