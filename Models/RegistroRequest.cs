using System.ComponentModel.DataAnnotations;

namespace APIControlEscolar.Models
{
    public class RegistroRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Password { get; set; } = null!;

        [Required]
        public string Matricula { get; set; } = null!;  // Matrícula del alumno o maestro

        [Required]
        public int IdGrado { get; set; }

        [Required]
        public int IdGrupo { get; set; }

        [Required]
        public int IdPeriodo { get; set; }
    }
}
