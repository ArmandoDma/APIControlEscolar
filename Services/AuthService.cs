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
            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                throw new Exception("Ya existe un usuario con ese correo.");

            string? passwordHash = null;
            string? salt = null;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                salt = GenerateRandomSalt();
                passwordHash = HashPassword(request.Password, salt);
            }

            var newUser = new Usuario
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                IdRol = request.IdRol,
                FechaRegistro = DateTime.Now,
                Estado = "Activo"
            };

            if (request.IdRol == 1) // Alumno
            {
                string matricula = request.Matricula.ToString();
                var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Matricula == matricula);
                if (alumno == null)
                    throw new Exception("La matrícula no corresponde a un alumno registrado.");
                newUser.IdAlumno = alumno.IdAlumno;
            }
            else if (request.IdRol == 2) // Maestro
            {
                string empleado = request.Matricula.ToString();
                var maestro = await _context.Maestros.FirstOrDefaultAsync(m => m.NumeroEmpleado == empleado);
                if (maestro == null)
                    throw new Exception("La matrícula no corresponde a un maestro registrado.");
                newUser.IdMaestro = maestro.IdMaestro;
            }
            else if (request.IdRol == 3) // Admin
            {
                if (!request.IdAdmin.HasValue)
                    throw new Exception("Se requiere el IdAdmin para registrar un administrador.");

                var admin = await _context.Admins.FindAsync(request.IdAdmin.Value);
                if (admin == null)
                    throw new Exception("No se encontró un administrador con ese ID.");

                newUser.IdAdmin = admin.IdAdmin;
            }

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
            if (user == null || string.IsNullOrEmpty(user.Salt) || string.IsNullOrEmpty(user.PasswordHash) ||
                HashPassword(password, user.Salt) != user.PasswordHash)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas.");
            }


            int userId = user.IdAlumno ?? user.IdMaestro ?? user.IdAdmin ?? 0;


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
