using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using APIControlEscolar.Data;
using APIControlEscolar.Models;

namespace APIControlEscolar
{
    public class JwtService : IJwtService
    {
        private readonly string _key = "A90AF569-2348-465E-AEA3-907F73755ABD";  // Usa una clave segura aquí
        private readonly string _issuer = "Jwt:Issuer";
        private readonly string _audience = "Jwt:Audience";
        private readonly ILogger<JwtService> _logger;
        private readonly CONTROL_ESCOLAR _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public JwtService(ILogger<JwtService> logger, CONTROL_ESCOLAR context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        /// <summary>
        /// Genera un token JWT para el usuario con el alumnoId.
        /// </summary>
        /// Generacion Token
        public async Task<string> GenerarTokenConId(int user, string email, int IdRol)
        {
            string rolNombre = IdRol == 1 ? "Alumno" : "Maestro";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim("IdRol", IdRol.ToString()),
                new Claim(ClaimTypes.Role, rolNombre)
            };

            // Agrega el claim correcto según el rol
            if (IdRol == 1)
                claims.Add(new Claim("alumnoId", user.ToString()));
            else if (IdRol == 2)
                claims.Add(new Claim("maestroId", user.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Valida el token JWT y obtiene los claims.
        /// </summary>
        /// 
        //Validacion de Token
        public ClaimsPrincipal? ValidarToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out _);

                return principal;
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError($"Token inválido: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al validar token: {ex.Message}");
                return null;
            }
        }


        public string? ObtenerRolDesdeToken()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;

                if (context == null)
                {
                    // Esto puede pasar si no estás dentro de una petición HTTP
                    return null;
                }

                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return null;
                }

                var token = authHeader.Replace("Bearer", "").Trim();

                var claimsPrincipal = ValidarToken(token);

                if (claimsPrincipal == null)
                {
                    return null;
                }

                var rol = claimsPrincipal.FindFirst("IdRol")?.Value;

                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el rol desde el token: {ex.Message}");
                return null;
            }
        }



        /// <summary>
        /// Obtiene el alumnoId desde el token JWT.
        /// </summary>
        public string? ObtenerPerfilIdDesdeToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var claimsPrincipal = ValidarToken(token);

            if (claimsPrincipal == null)
            {
                return null;
            }

            var id = claimsPrincipal.FindFirst("alumnoId")?.Value
                  ?? claimsPrincipal.FindFirst("maestroId")?.Value;

            return id;
        }
    }
}