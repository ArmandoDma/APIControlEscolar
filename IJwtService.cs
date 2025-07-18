using System.Security.Claims;

namespace APIControlEscolar
{
    public interface IJwtService
    {
        Task<string> GenerarTokenConId(int user,string email, int IdRol); 
        ClaimsPrincipal? ValidarToken(string token);  
        string? ObtenerPerfilIdDesdeToken();

        string? ObtenerRolDesdeToken();
    }
}
