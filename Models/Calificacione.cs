using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.Text.Json.Serialization; // Ya lo tienes, para [JsonIgnore]

namespace APIControlEscolar.Models
{
    public class Calificacione
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdCalificacion { get; set; }

        // [Required(ErrorMessage = "El ID del alumno es obligatorio.")]
        // Asegura que este campo esté presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID del alumno debe ser un número positivo.")]
        // Valida que el ID del alumno sea mayor que cero.
        [Required(ErrorMessage = "El ID del alumno es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del alumno debe ser un número positivo.")]
        public int IdAlumno { get; set; }

        // [Required(ErrorMessage = "El ID de la materia es obligatorio.")]
        // Asegura que este campo esté presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID de la materia debe ser un número positivo.")]
        // Valida que el ID de la materia sea mayor que cero.
        [Required(ErrorMessage = "El ID de la materia es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la materia debe ser un número positivo.")]
        public int IdMateria { get; set; }

        // [Required(ErrorMessage = "El ID del maestro es obligatorio.")]
        // Asegura que este campo esté presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID del maestro debe ser un número positivo.")]
        // Valida que el ID del maestro sea mayor que cero.
        [Required(ErrorMessage = "El ID del maestro es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del maestro debe ser un número positivo.")]
        public int IdMaestro { get; set; }

        // [Required(ErrorMessage = "El ID del periodo es obligatorio.")]
        // Asegura que este campo esté presente.
        // [Range(1, int.MaxValue, ErrorMessage = "El ID del periodo debe ser un número positivo.")]
        // Valida que el ID del periodo sea mayor que cero.
        [Required(ErrorMessage = "El ID del periodo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del periodo debe ser un número positivo.")]
        public int IdPeriodo { get; set; }

        // [Range(0.0, 10.0, ErrorMessage = "La calificación parcial 1 debe estar entre 0.0 y 10.0.")]
        // Si las calificaciones son sobre 10 (o 100, ajusta el rango).
        // Si pueden ser nulas (decimal?), entonces [Required] no aplica.
        // Puedes agregar [Required] si la primera calificación parcial siempre debe ser ingresada.
        [Range(0.0, 10.0, ErrorMessage = "La calificación parcial 1 debe estar entre {1} y {2}.")]
        public decimal? CalificacionParcial1 { get; set; }

        // [Range(0.0, 10.0, ErrorMessage = "La calificación parcial 2 debe estar entre 0.0 y 10.0.")]
        [Range(0.0, 10.0, ErrorMessage = "La calificación parcial 2 debe estar entre {1} y {2}.")]
        public decimal? CalificacionParcial2 { get; set; }

        // [Range(0.0, 10.0, ErrorMessage = "La calificación parcial 3 debe estar entre 0.0 y 10.0.")]
        [Range(0.0, 10.0, ErrorMessage = "La calificación parcial 3 debe estar entre {1} y {2}.")]
        public decimal? CalificacionParcial3 { get; set; }

        [JsonIgnore]
        // [Range(0.0, 10.0, ErrorMessage = "La calificación final debe estar entre 0.0 y 10.0.")]
        // Si esta calificación se calcula y no se ingresa directamente, las validaciones podrían ser menos estrictas aquí
        // o podrían estar en el método de cálculo. El [JsonIgnore] indica que no se serializa al JSON de salida.
        // Si fuera un campo de entrada, necesitaría validación.
        [Range(0.0, 10.0, ErrorMessage = "La calificación final debe estar entre {1} y {2}.")]
        public decimal? CalificacionFinal { get; set; }

        [JsonIgnore]
        // [Required(ErrorMessage = "La fecha de registro es obligatoria.")]
        // Si 'Fecha_Registro' es un campo que tu sistema genera automáticamente al guardar (como DateTime.UtcNow),
        // no necesitas [Required] aquí, ya que no vendría en la solicitud inicial.
        // [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de registro inválido.")]
        // Proporciona metadatos para el tipo de dato.
        public DateTime? Fecha_Registro { get; set; }

        // SUGERENCIAS ADICIONALES (Propiedades de Navegación):
        // Puedes añadir propiedades de navegación para Alumno, Materia, Maestro, Periodo
        // para facilitar las relaciones en Entity Framework Core si no las tienes ya:
        // public virtual Alumno? AlumnoNavigation { get; set; }
        // public virtual Materia? MateriaNavigation { get; set; }
        // public virtual Maestro? MaestroNavigation { get; set; }
        // public virtual Periodo? PeriodoNavigation { get; set; }
    }
}