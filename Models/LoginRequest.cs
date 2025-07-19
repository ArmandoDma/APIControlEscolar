using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class LoginRequest
    {
        // [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        // Asegura que el campo Email no puede ser nulo o vacío.
        // [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        // Valida que el string se ajuste a un formato de correo electrónico estándar.
        // [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        // Limita la longitud máxima del correo electrónico para evitar entradas excesivamente largas
        // y para que se ajuste a los límites de la base de datos.
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        [StringLength(50, ErrorMessage = "El correo electrónico no puede exceder los 50 caracteres.")]
        public string Email { get; set; } = null!;

        // [Required(ErrorMessage = "La contraseña es obligatoria.")]
        // Asegura que el campo Password no puede ser nulo o vacío.
        // [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        // Aplica una longitud mínima a la contraseña, lo cual es una buena práctica de seguridad.
        // [MaxLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres.")]
        // Aunque la contraseña se hashea en el backend, limitar la longitud del input ayuda
        // a prevenir ataques de "denegación de servicio" con entradas muy grandes.
        // También puedes considerar una [RegularExpression] si tienes requisitos de complejidad
        // de contraseña (ej. mayúsculas, minúsculas, números, caracteres especiales).
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(10, ErrorMessage = "La contraseña no puede exceder los 10 caracteres.")]
        // Ejemplo de validación de complejidad (requiere al menos una mayúscula, una minúscula y un dígito)
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$", ErrorMessage = "La contraseña debe contener al menos una mayúscula, una minúscula y un número.")]
        public string Password { get; set; } = null!;

        // [Required(ErrorMessage = "El ID de rol es obligatorio.")]
        // Asegura que el ID de rol debe estar presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID de rol debe ser un número positivo.")]
        // Valida que el ID de rol sea mayor que cero, asumiendo que los IDs válidos son positivos.
        // Esto ayuda a evitar la inyección de roles inválidos.
        [Required(ErrorMessage = "El ID de rol es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de rol debe ser un número positivo.")]
        public int IdRol { get; set; }
    }
}
