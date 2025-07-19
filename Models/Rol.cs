using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.Collections.Generic; // Ya lo tienes, para ICollection

namespace APIControlEscolar.Models
{
    public partial class Rol
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        // No necesita [Required] directamente en la propiedad si es autoincremental en la DB.
        // Si IdRol se envía manualmente, podrías añadir [Range(1, int.MaxValue)] para asegurar que sea positivo.
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede exceder los 50 caracteres.")]
        // Ajusta la longitud máxima (ej. 20, 30, 50) según los nombres de rol que esperes (ej. "Administrador", "Maestro", "Alumno").
        public string Nombre { get; set; } = null!;

        // Propiedad de navegación para la relación de uno a muchos con Usuario.
        // Las propiedades de navegación (colecciones) no suelen llevar Data Annotations de validación aquí.
        // Su validez se maneja a través de la clave foránea en el modelo 'Usuario'.
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}