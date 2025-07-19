using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class GenerarTokenRequest
    {
        // [Required(ErrorMessage = "El número de empleado es obligatorio.")]
        // Asegura que el campo 'NumeroEmpleado' no puede ser nulo o una cadena vacía/solo espacios.
        // [StringLength(20, MinimumLength = 5, ErrorMessage = "El número de empleado debe tener entre {2} y {1} caracteres.")]
        // Define una longitud máxima razonable para el número de empleado (ej. 20 caracteres).
        // Ajusta 'MinimumLength' y 'MaximumLength' según el formato real de tus números de empleado.
        // [RegularExpression(@"^[A-Z0-9]{5,20}$", ErrorMessage = "Formato de número de empleado inválido.")]
        // Si tu número de empleado es estrictamente alfanumérico (letras mayúsculas y números), usa esta Regex.
        // Si es solo numérico (ej. 123456), usa: @"^\d{5,20}$"
        [Required(ErrorMessage = "El número de empleado es obligatorio.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El número de empleado debe tener entre 5 y 20 caracteres.")] // Ajusta según tu formato real
        // Si sabes el formato exacto (ej. solo números, o un patrón específico), añade RegularExpression:
        // [RegularExpression(@"^\d+$", ErrorMessage = "El número de empleado solo debe contener dígitos.")] // Si es solo numérico
        public string NumeroEmpleado { get; set; } = null!;
    }
}