using System.ComponentModel.DataAnnotations;

namespace APIControlEscolar.Models
{
    public class RegistroRequest
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres.")]
        // Opcional: [RegularExpression] para complejidad de contraseña
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$", ErrorMessage = "La contraseña debe contener al menos una mayúscula, una minúscula y un número.")]
        public string Password { get; set; } = null!;

        // ¡AJUSTE FINAL AQUÍ para matrículas de 5 dígitos!
        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        // El rango para un número entero de 5 dígitos va de 10,000 a 99,999.
        [Range(10000, 99999, ErrorMessage = "La matrícula debe ser un número de exactamente 5 dígitos.")]
        public int Matricula { get; set; } // Sigue siendo int, lo cual es correcto.

        [Required(ErrorMessage = "El ID de grado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de grado debe ser un número positivo.")]
        public int IdGrado { get; set; }

        [Required(ErrorMessage = "El ID de grupo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de grupo debe ser un número positivo.")]
        public int IdGrupo { get; set; }

        [Required(ErrorMessage = "El ID de periodo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de periodo debe ser un número positivo.")]
        public int IdPeriodo { get; set; }

        public int IdRol {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El ID de administrador debe ser un número positivo.")]
        public int? IdAdmin { get; set; }
    }
}