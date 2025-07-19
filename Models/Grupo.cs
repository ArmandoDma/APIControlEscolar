using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System; // Necesario para DateTime
using System.Collections.Generic; // Necesario para ICollection

// Opcional: Si vas a usar un atributo de validación personalizado para fechas futuras.
// using APIControlEscolar.ValidationAttributes; // Asegúrate de que esta ruta sea correcta

namespace APIControlEscolar.Models
{
    public partial class Grupo
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        // No necesita [Required] ya que es un tipo de valor (int) y la DB la autogenerará/asignará.
        public int IdGrupo { get; set; }

        [Required(ErrorMessage = "El nombre del grupo es obligatorio.")]
        [StringLength(10, ErrorMessage = "El nombre del grupo no puede exceder los 10 caracteres.")]
        // Ejemplos: "1A", "2B", "3C", "G101". Ajusta la longitud máxima según tus convenciones.
        public string NombreGrupo { get; set; } = null!;

        // FechaCreacion es nullable (DateTime?), lo que indica que puede ser opcional o establecida por el sistema.
        // Si siempre debe tener un valor al crear, quita el '?' y añade [Required].
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de creación inválido.")]
        // Opcional: Si la fecha de creación no puede ser en el futuro (si viene del cliente).
        // [FechaNoFutura(ErrorMessage = "La fecha de creación no puede ser en el futuro.")] // Atributo personalizado
        public DateTime? FechaCreacion { get; set; }

        [Required(ErrorMessage = "El ID del maestro asignado al grupo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del maestro debe ser un número positivo.")]
        public int IdMaestro { get; set; }

        // Propiedad de navegación para la relación de uno a muchos con Maestro (un maestro asignado a un grupo).
        // No necesita [Required] directamente en la propiedad de navegación si IdMaestro ya es [Required].
        public Maestro Maestro { get; set; } = null!;

        // Propiedad de navegación para la relación de uno a muchos con Alumno (un grupo tiene muchos alumnos).
        // Las propiedades de navegación (colecciones) no suelen llevar Data Annotations de validación aquí.
        // Su validez se maneja a través de la clave foránea en el modelo 'Alumno'.
        public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
    }
}