using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    // Considera renombrar la clase de 'Materium' a 'Materia' para mayor claridad y convención en C#.
    public partial class Materium
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdMateria { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la materia no puede exceder los 100 caracteres.")]
        public string NombreMateria { get; set; } = null!;

        [Required(ErrorMessage = "La URL de la imagen de la materia es obligatoria.")]
        [StringLength(255, ErrorMessage = "La URL de la imagen no puede exceder los 255 caracteres.")]
        [Url(ErrorMessage = "El formato de la URL de la imagen es inválido.")]
        public string ImageMat { get; set; } = null!;

        [Required(ErrorMessage = "El ID de grado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de grado debe ser un número positivo.")]
        public int IdGrado { get; set; }

        // Propiedad de navegación para la relación con Grado.
        // No necesita [Required] directamente en la propiedad de navegación si IdGrado es [Required].
        public Grado Grado { get; set; } = null!;

        [Required(ErrorMessage = "El ID de grupo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de grupo debe ser un número positivo.")]
        public int IdGrupo { get; set; }

        // Propiedad de navegación para la relación con Grupo.
        public Grupo Grupo { get; set; } = null!;

        [Required(ErrorMessage = "El ID de carrera es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de carrera debe ser un número positivo.")]
        public int IdCarrera { get; set; }

        // Propiedad de navegación para la relación con Carrera.
        public Carrera Carrera { get; set; } = null!;

        [Required(ErrorMessage = "El ID de maestro es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de maestro debe ser un número positivo.")]
        public int IdMaestro { get; set; }

        // Propiedad de navegación para la relación con Maestro.
        public Maestro Maestro { get; set; } = null!;

        // SUGERENCIAS ADICIONALES (Propiedades de Navegación para colecciones):
        // Si tuvieras relaciones de uno a muchos o muchos a muchos, irían aquí.
        // Por ejemplo, si una materia puede tener muchas calificaciones:
        // public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

        // Si tienes una tabla intermedia MaestroMaterium para una relación de muchos a muchos,
        // no la listarías directamente aquí, sino en la configuración de EF Core,
        // y la relación se manejaría a través de IdMaestro e IdMateria.
        // Sin embargo, si MaestroMaterium es tu tabla pivote y Materium *no* tiene un solo IdMaestro,
        // entonces la propiedad IdMaestro aquí sería incorrecta y deberías revisar tu diseño de DB.
        // Asumo que IdMaestro aquí es el "maestro principal" o "titular" de la materia.
    }
}