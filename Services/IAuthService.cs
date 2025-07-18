
using APIControlEscolar.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace APIControlEscolar.Services
{
    public interface IAuthService
    {
        Task<Usuario> RegisterUserAsync(RegistroRequest request);
        Task<string> AuthenticateAsync(string email, string password, int IdRol);
    }
}
