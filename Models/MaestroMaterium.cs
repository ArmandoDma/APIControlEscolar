using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.ComponentModel.DataAnnotations.Schema; // Para [ForeignKey] o [Key] compuesto si es necesario

namespace APIControlEscolar.Models
{
    // [PrimaryKey(nameof(IdMaestro), nameof(IdMateria))] // Descomentar si usas EF Core 6+ y quieres definir la clave compuesta directamente en el modelo.
    // Si no, EF Core la inferirá de las configuraciones de DbContext o convenciones.
    public class MaestroMaterium // Considera renombrar a 'MaestroMateria' para mayor claridad
    {
        // [Key]
        // [Column(Order = 1)] // Para claves compuestas con EF 5 o anterior o si necesitas un orden específico
        // [ForeignKey("Maestro")]
        [Required(ErrorMessage = "El ID del maestro es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del maestro debe ser un número positivo.")]
        public int IdMaestro { get; set; }

        // [Key]
        // [Column(Order = 2)] // Para claves compuestas con EF 5 o anterior o si necesitas un orden específico
        // [ForeignKey("Materia")]
        [Required(ErrorMessage = "El ID de la materia es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la materia debe ser un número positivo.")]
        public int IdMateria { get; set; }

        // Propiedades de navegación (para Entity Framework Core)
        // Estas propiedades son importantes para establecer las relaciones y realizar includes.
        // No llevan DataAnnotations de validación directa, ya que su validez depende de los IDs.
        // [Required(ErrorMessage = "La entidad Maestro es obligatoria para la relación.")]
        // public virtual Maestro Maestro { get; set; } = null!; // Podría ser nullable si la relación es opcional (poco común aquí)

        // [Required(ErrorMessage = "La entidad Materia es obligatoria para la relación.")]
        // public virtual Materium Materia { get; set; } = null!; // 'Materium' es el nombre que usas en el modelo de materia, asumo.
        // Si tu modelo de Materia se llama 'Materia', ajusta aquí.
    }
}