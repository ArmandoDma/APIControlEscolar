using System.ComponentModel.DataAnnotations;

namespace APIControlEscolar.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Password { get; set; } = null!;

        [Required]
        public int IdRol {  get; set; }
    }
}
