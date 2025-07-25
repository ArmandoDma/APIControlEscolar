using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.Text.Json.Serialization;     // Para [JsonIgnore] si lo usas
using System; // Para DateTime

// Si decides implementar la validación para asegurar que sea UN SOLO Id (Alumno o Maestro),
// necesitarías un atributo a nivel de clase como el siguiente:
// using APIControlEscolar.ValidationAttributes; // Asumiendo que crearías un CustomValidationAttribute

namespace APIControlEscolar.Models
{
    // Opcional: Si un usuario DEBE estar asociado a un Alumno O un Maestro (no a ambos, y no a ninguno),
    // se requeriría un atributo de validación a nivel de clase:
    // [ValidarAsociacionUsuario(ErrorMessage = "Un usuario debe estar asociado a un Alumno o a un Maestro, pero no a ambos.")]
    public class Usuario
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El hash de la contraseña es obligatorio.")]
        [StringLength(255, MinimumLength = 64, ErrorMessage = "El hash de la contraseña debe tener entre {2} y {1} caracteres.")]
        // Asumiendo SHA-256 (64 hex chars) o SHA-512 (128 hex chars). Ajusta según tu algoritmo de hashing.
        public string PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = "El salt es obligatorio.")]
        [StringLength(255, MinimumLength = 32, ErrorMessage = "El salt debe tener entre {2} y {1} caracteres.")]
        // Asumiendo un salt de 32 a 64 caracteres (ej. Base64 de 16-32 bytes). Ajusta según cómo generes tu salt.
        public string Salt { get; set; } = null!;

        [Required(ErrorMessage = "El ID de rol es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de rol debe ser un número positivo.")]
        public int IdRol { get; set; }

        // IdAlumno es nullable (int?), lo que indica que puede no estar presente.
        // Si se proporciona, debe ser un ID válido.
        [Range(1, int.MaxValue, ErrorMessage = "El ID de alumno debe ser un número positivo.")]
        public int? IdAlumno { get; set; }

        // IdMaestro es nullable (int?), lo que indica que puede no estar presente.
        // Si se proporciona, debe ser un ID válido.
        [Range(1, int.MaxValue, ErrorMessage = "El ID de maestro debe ser un número positivo.")]
        public int? IdMaestro { get; set; }

        public int? IdAdmin { get; set; }

        // Estado es nullable (string?), lo que indica que es opcional.
        // Si siempre debe tener un valor, quita el '?' y añade [Required].
        [StringLength(10, ErrorMessage = "El estado no puede exceder los 10 caracteres.")]
        // Opcional: Recomiendo usar un [RegularExpression] si los estados son predefinidos.
        // [RegularExpression("^(Activo|Inactivo|Bloqueado|Suspendido)$", ErrorMessage = "Estado de usuario no válido.")]
        public string? Estado { get; set; }

        // FechaRegistro es nullable (DateTime?), lo que indica que es opcional.
        // Si siempre debe tener un valor, quita el '?' y añade [Required].
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de registro inválido.")]
        // Opcional: La fecha de registro no debería ser en el futuro.
        // [FechaNoFutura(ErrorMessage = "La fecha de registro no puede ser en el futuro.")] // Atributo personalizado
        public DateTime? FechaRegistro { get; set; }

        // Propiedades de navegación (para Entity Framework Core)
        // [JsonIgnore] para evitar ciclos de referencia si serializas el usuario y estas propiedades son cargadas.
        // Son nulas si el Id correspondiente es nulo, o si EF no pudo cargar la relación.
        // No necesitan DataAnnotations de validación directa.

        // [JsonIgnore] // Si no quieres que se serialice en JSON.
        public virtual Alumno? IdAlumnoNavigation { get; set; }

        // [JsonIgnore] // Si no quieres que se serialice en JSON.
        public virtual Maestro? IdMaestroNavigation { get; set; }

        public virtual Admins? IdAdminNavigation {  get; set; }

        // [Required(ErrorMessage = "La entidad Rol es obligatoria.")] // No es necesario si IdRol es [Required]
        // [JsonIgnore] // Si no quieres que se serialice en JSON.
        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}