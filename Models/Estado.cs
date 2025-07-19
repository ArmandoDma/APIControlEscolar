using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class Estado
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        // No necesita [Required] ya que es un tipo de valor (int) y la DB la autogenerará/asignará
        // o será un ID predefinido para estados.
        public int IdEstado { get; set; }

        // [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        // Asegura que el nombre del estado no puede ser nulo o vacío.
        // [StringLength(100, ErrorMessage = "El nombre del estado no puede exceder los 100 caracteres.")]
        // Define una longitud máxima razonable para el nombre del estado. Ajusta este valor
        // si tus nombres de estado pueden ser más largos o si tienes una longitud específica en la DB.
        [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del estado no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; } = null!;

        // SUGERENCIA ADICIONAL (Propiedades de Navegación):
        // Si tienes modelos como 'Municipio' o 'Alumno' y quieres establecer relaciones directas
        // para facilitar las consultas con Entity Framework Core, podrías añadir:
        // public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
        // public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
    }
}