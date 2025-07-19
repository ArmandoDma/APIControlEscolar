using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System; // Necesario para DateTime

// Opcional: Si vas a usar un atributo de validación personalizado para fechas futuras.
// using APIControlEscolar.ValidationAttributes; // Asegúrate de que esta ruta sea correcta

namespace APIControlEscolar.Models
{
    public partial class Grado
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        // No necesita [Required] ya que es un tipo de valor (int) y la DB la autogenerará/asignará.
        public int IdGrado { get; set; }

        [Required(ErrorMessage = "El nombre del grado es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre del grado no puede exceder los 20 caracteres.")]
        // Ejemplos: "Primero", "Segundo", "Tercero", "Cuarto", "Quinto", "Sexto".
        // Ajusta la longitud máxima si es necesario.
        public string NombreGrado { get; set; } = null!;

        // FechaCreacion es nullable (DateTime?), lo que indica que puede ser opcional o establecida por el sistema.
        // Si siempre debe tener un valor al crear, quita el '?' y añade [Required].
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de creación inválido.")]
        // Opcional: Si la fecha de creación no puede ser en el futuro.
        // Esto generalmente se maneja en el controlador (se asigna DateTime.UtcNow o DateTime.Now al crear),
        // pero puedes forzarlo con un atributo personalizado si los datos vienen del cliente.
        // [FechaNoFutura(ErrorMessage = "La fecha de creación no puede ser en el futuro.")]
        public DateTime? FechaCreacion { get; set; }

        // Si tuvieras relaciones con otras entidades (ej. muchas Materias pertenecen a un Grado),
        // aquí irían las propiedades de navegación, pero no requieren DataAnnotations de validación directa.
        // public virtual ICollection<Materia> Materias { get; set; } = new List<Materia>();
    }
}