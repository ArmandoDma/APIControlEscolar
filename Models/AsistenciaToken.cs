using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.ComponentModel.DataAnnotations.Schema; // Puede ser útil para [NotMapped] o [ForeignKey] si es necesario

namespace APIControlEscolar.Models
{
    public class AsistenciaToken
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdToken { get; set; }

        // [Required(ErrorMessage = "El valor del token es obligatorio.")]
        // Asegura que el valor del token (la cadena GUID) no sea nulo o vacío.
        // [StringLength(36, MinimumLength = 36, ErrorMessage = "El token debe tener 36 caracteres (GUID).")]
        // Un GUID (Globally Unique Identifier) tiene un formato estándar de 36 caracteres (ej. "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx").
        // [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$",
        //     ErrorMessage = "El formato del token no es válido (GUID).")]
        // Esta Regex asegura que el token se ajuste al formato GUID estándar.
        [Required(ErrorMessage = "El valor del token es obligatorio.")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "El token debe tener 36 caracteres (GUID).")]
        [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$",
            ErrorMessage = "El formato del token no es válido (GUID).")]
        public string Token { get; set; } = null!;

        // [Required(ErrorMessage = "La fecha de creación del token es obligatoria.")]
        // Asegura que este campo esté presente.
        // [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha y hora de creación inválido.")]
        // Proporciona metadatos para el tipo de dato.
        public DateTime CreatedAt { get; set; }

        // [Required(ErrorMessage = "La fecha de expiración del token es obligatoria.")]
        // Asegura que este campo esté presente.
        // [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha y hora de expiración inválido.")]
        // [FutureDate(ErrorMessage = "La fecha de expiración debe ser en el futuro.")] // Ejemplo de validación personalizada.
        // Puedes añadir una validación personalizada para asegurar que ExpiresAt sea siempre posterior a CreatedAt.
        [Required(ErrorMessage = "La fecha de expiración del token es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha y hora de expiración inválido.")]
        public DateTime ExpiresAt { get; set; }

        // [Required(ErrorMessage = "El estado de actividad del token es obligatorio.")]
        // Aunque es un booleano (que siempre tiene un valor), es buena práctica si su ausencia en JSON
        // pudiera causar un valor inesperado (ej. default(bool) que es false).
        public bool IsActive { get; set; }

        // [Required(ErrorMessage = "El ID del maestro es obligatorio.")]
        // Asegura que el IdMaestro debe estar presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID del maestro debe ser un número positivo.")]
        // Valida que el ID del maestro sea mayor que cero, asumiendo que los IDs válidos son positivos.
        [Required(ErrorMessage = "El ID del maestro es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del maestro debe ser un número positivo.")]
        public int IdMaestro { get; set; }

        // [Required(ErrorMessage = "La entidad Maestro es obligatoria.")]
        // Indica que la relación con Maestro no puede ser nula.
        public Maestro Maestro { get; set; } = null!;

        // ICollection<Asistencia> AsistenciaList { get; set; } = new List<Asistencia>();
        // Esta es una propiedad de navegación para la relación uno-a-muchos (un token puede tener muchas asistencias).
        // No suele llevar DataAnnotations de validación directa, ya que es una colección.
        public ICollection<Asistencia> AsistenciaList { get; set; } = new List<Asistencia>();
    }
}