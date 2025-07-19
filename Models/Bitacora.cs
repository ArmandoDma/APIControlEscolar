using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.ComponentModel.DataAnnotations.Schema; // Puede ser útil para [NotMapped] o [ForeignKey] si es necesario

namespace APIControlEscolar.Models
{
    public class Bitacora
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdHistorialAccion { get; set; }

        // [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        // Asegura que el IdUsuario debe estar presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID de usuario debe ser un número positivo.")]
        // Valida que el ID de usuario sea mayor que cero, asumiendo que los IDs válidos son positivos.
        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de usuario debe ser un número positivo.")]
        public int IdUsuario { get; set; }

        // [Required(ErrorMessage = "La acción es obligatoria.")]
        // Asegura que la descripción de la acción no sea nula o vacía.
        // [StringLength(255, ErrorMessage = "La acción no puede exceder los 255 caracteres.")]
        // Define una longitud máxima razonable para la descripción de la acción.
        [Required(ErrorMessage = "La acción es obligatoria.")]
        [StringLength(255, ErrorMessage = "La acción no puede exceder los 255 caracteres.")]
        public string Accion { get; set; } = null!;

        // [Required(ErrorMessage = "La fecha y hora de la acción es obligatoria.")]
        // Si el campo puede ser nulo en la base de datos (DateTime?), entonces [Required] no es aplicable
        // a menos que quieras forzar su presencia en el modelo.
        // [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha y hora de acción inválido.")]
        // Proporciona metadatos para el tipo de dato.
        // Considera si 'FechaAccion' DEBE ser siempre establecida o puede ser nula.
        // Si siempre debe tener un valor, quita el '?' de DateTime? y aplica [Required].
        // Si puede ser nula, entonces no apliques [Required].
        public DateTime? FechaAccion { get; set; }

        // SUGERENCIA ADICIONAL: Propiedad de Navegación a Usuario (si existe)
        // Si tienes un modelo 'Usuario' y quieres relacionar la Bitacora con el usuario que realizó la acción.
        // [ForeignKey("IdUsuario")]
        // public virtual Usuario? Usuario { get; set; }
    }
}