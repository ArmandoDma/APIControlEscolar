using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.ComponentModel.DataAnnotations.Schema; // Puede ser útil para [NotMapped] o [ForeignKey] si es necesario

namespace APIControlEscolar.Models
{
    public class Asistencia
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdAsistencia { get; set; }

        // [Required(ErrorMessage = "El ID del alumno es obligatorio.")]
        // Asegura que el IdAlumno debe estar presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID del alumno debe ser un número positivo.")]
        // Valida que el ID del alumno sea mayor que cero, asumiendo que los IDs válidos son positivos.
        public int IdAlumno { get; set; }

        // [Required(ErrorMessage = "La entidad Alumno es obligatoria.")]
        // Indica que la relación con Alumno no puede ser nula.
        // Entity Framework Core se encarga de cargarla si se usa .Include().
        // Esta validación es más sobre la relación que sobre el dato en sí.
        public Alumno Alumno { get; set; } = null!;

        // [Required(ErrorMessage = "El ID del token de asistencia es obligatorio.")]
        // Asegura que el IdToken debe estar presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID del token de asistencia debe ser un número positivo.")]
        // Valida que el ID del token sea mayor que cero.
        public int IdToken { get; set; }

        // [Required(ErrorMessage = "La entidad AsistenciaToken es obligatoria.")]
        // Indica que la relación con AsistenciaToken no puede ser nula.
        public AsistenciaToken AsistenciaToken { get; set; } = null!;

        // [Required(ErrorMessage = "La fecha y hora de la asistencia es obligatoria.")]
        // Asegura que el campo FechaHora debe estar presente.
        // [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha y hora inválido.")]
        // Proporciona metadatos para la fecha y hora.
        // Puedes añadir validaciones personalizadas si necesitas, por ejemplo, que la fecha no sea en el futuro.
        public DateTime FechaHora { get; set; }
    }
}
