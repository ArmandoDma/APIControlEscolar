using APIControlEscolar.Data;
using APIControlEscolar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace APIControlEscolar.Services
{
    public class AuthService : IAuthService
    {
        private readonly CONTROL_ESCOLAR _context;
        private readonly IJwtService _jwtService; // Agregamos la inyección de JwtService

        public AuthService(CONTROL_ESCOLAR context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        public async Task<Usuario> RegisterUserAsync(RegistroRequest request)
        {
            // Verificar si el usuario ya existe
            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new Exception("Ya existe un usuario con ese correo.");
            }

            // Verificar si la matrícula corresponde a un alumno o maestro
            var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Matricula == request.Matricula);
            var maestro = await _context.Maestros.FirstOrDefaultAsync(m => m.NumeroEmpleado == request.Matricula);

            if (alumno == null && maestro == null)
            {
                throw new Exception("La matrícula no corresponde a un alumno o maestro registrado.");
            }

            // Determinar el rol basado en si es alumno o maestro
            int idRol = alumno != null ? 1 : 2; // 1 = Alumno, 2 = Maestro

            // Generar salt y hash de la contraseña
            var salt = GenerateRandomSalt();
            var passwordHash = HashPassword(request.Password, salt);

            // Crear usuario
            var newUser = new Usuario
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                IdRol = idRol,
               
            };

            // Asignar ID correspondiente
            if (alumno != null) newUser.IdAlumno = alumno.IdAlumno;
            if (maestro != null) newUser.IdMaestro = maestro.IdMaestro;

            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        /// <summary>
        /// Autentica a un usuario y genera un token JWT si las credenciales son correctas.
        /// </summary>
        public async Task<string> AuthenticateAsync(string email, string password, int IdRol)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email y contraseña son obligatorios.");
            }

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.IdRol == IdRol);
            if (user == null || HashPassword(password, user.Salt) != user.PasswordHash)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas.");
            }

            int userId = user.IdAlumno ?? user.IdMaestro ?? 0;

            return await _jwtService.GenerarTokenConId(userId, user.Email, user.IdRol);

        }

        /// <summary>
        /// Genera un salt aleatorio para proteger la contraseña.
        /// </summary>
        private string GenerateRandomSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Aplica hash a la contraseña usando PBKDF2 con HMACSHA256.
        /// </summary>
        private string HashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            return Convert.ToBase64String(hashed);
        }
    }
}
