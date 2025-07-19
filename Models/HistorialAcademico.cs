using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class HistorialAcademico
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdHistorial { get; set; }

        [Required(ErrorMessage = "El ID del alumno es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del alumno debe ser un número positivo.")]
        public int IdAlumno { get; set; }

        [Required(ErrorMessage = "El ID de la materia es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la materia debe ser un número positivo.")]
        public int IdMateria { get; set; }

        [Required(ErrorMessage = "El ID del periodo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del periodo debe ser un número positivo.")]
        public int IdPeriodo { get; set; }

        // La calificación final es nullable (decimal?), lo que indica que puede no estar presente inicialmente.
        // Si siempre debe tener un valor, quita el '?' y añade [Required].
        // [Range(0.0, 10.0, ErrorMessage = "La calificación final debe estar entre {1} y {2}.")]
        // Asumiendo un rango de 0 a 10. Ajusta según tu sistema de calificación (ej. 0 a 100).
        [Range(0.0, 10.0, ErrorMessage = "La calificación final debe estar entre {1} y {2}.")]
        public decimal? CalificacionFinal { get; set; }

        // EstadoAcademico es nullable (string?), lo que indica que puede no estar presente inicialmente.
        // Si siempre debe tener un valor, quita el '?' y añade [Required].
        // [StringLength(50, ErrorMessage = "El estado académico no puede exceder los 50 caracteres.")]
        // [RegularExpression("^(Aprobado|Reprobado|Curso Especial|No Cursado)$", ErrorMessage = "Estado académico no válido.")]
        // Si tienes una lista fija de estados permitidos.
        [StringLength(10, ErrorMessage = "El estado académico no puede exceder los 10 caracteres.")]
        public string? EstadoAcademico { get; set; }

        // SUGERENCIAS ADICIONALES (Propiedades de Navegación):
        // Para facilitar las relaciones en Entity Framework Core si no las tienes ya:
        // public virtual Alumno? AlumnoNavigation { get; set; }
        // public virtual Materium? MateriaNavigation { get; set; } // Asumiendo que tu modelo se llama Materium
        // public virtual Periodo? PeriodoNavigation { get; set; }
    }
}