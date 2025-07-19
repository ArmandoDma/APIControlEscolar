using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class Carrera
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        // No necesita [Required] ya que es un tipo de valor (int) y la DB la autogenerará/asignará.
        public int IdCarrera { get; set; }

        // [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
        // Asegura que el nombre de la carrera no sea nulo o vacío.
        // [StringLength(100, ErrorMessage = "El nombre de la carrera no puede exceder los 100 caracteres.")]
        // Define una longitud máxima razonable para el nombre de la carrera. Ajusta este valor
        // si tus nombres de carrera pueden ser más largos o si tienes una longitud específica en la DB.
        [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la carrera no puede exceder los 100 caracteres.")]
        public string NombreCarrera { get; set; } = null!;

        // ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
        // Esta es una propiedad de navegación para la relación uno-a-muchos (una carrera puede tener muchos alumnos).
        // Las propiedades de navegación no suelen llevar DataAnnotations de validación directa, ya que son colecciones
        // o referencias a otras entidades. Su validación se basa en la integridad referencial de los IDs.
        public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
    }
}