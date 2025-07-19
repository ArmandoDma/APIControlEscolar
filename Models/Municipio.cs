using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class Municipio
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        // No necesita [Required] ya que es un tipo de valor (int) y la DB la autogenerará/asignará.
        public int IdMunicipio { get; set; }

        [Required(ErrorMessage = "El nombre del municipio es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del municipio no puede exceder los 50 caracteres.")]
        // Ajusta la longitud máxima según tus necesidades y la base de datos.
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El ID de estado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de estado debe ser un número positivo.")]
        public int IdEstado { get; set; }

        // Propiedad de navegación para la relación con Estado.
        // Es recomendable incluirla si planeas cargar el objeto Estado relacionado en tus consultas.
        // No necesita [Required] directamente aquí si IdEstado ya es [Required].
        // Asegúrate de que el modelo 'Estado' también exista.
        // public virtual Estado Estado { get; set; } = null!; // Descomentar si deseas esta propiedad de navegación
    }
}