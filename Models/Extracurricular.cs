using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class Extracurricular
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int Id { get; set; }

        // [Required(ErrorMessage = "La categoría es obligatoria.")]
        // [StringLength(50, ErrorMessage = "La categoría no puede exceder los 50 caracteres.")]
        // Puedes añadir un [RegularExpression] si hay categorías predefinidas (ej. Deportiva, Cultural, Académica).
        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(50, ErrorMessage = "La categoría no puede exceder los 50 caracteres.")]
        public string Categoria { get; set; } = null!; // Asumo que Categoria no es nullable, si lo es, agrega '?'

        // [Required(ErrorMessage = "El nombre de la actividad es obligatorio.")]
        // [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        [Required(ErrorMessage = "El nombre de la actividad es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        // [Required(ErrorMessage = "La descripción es obligatoria.")]
        // [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        // Ajusta la longitud si necesitas descripciones más largas.
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; } = null!;

        // [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        // [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de inicio inválido.")]
        // Puedes añadir una validación personalizada para asegurar que la fecha de inicio no sea en el pasado
        // o que FechaFin sea posterior a FechaInicio.
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de inicio inválido.")]
        public DateTime FechaInicio { get; set; }

        // [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        // [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de fin inválido.")]
        // Una validación personalizada para asegurar FechaFin > FechaInicio es crucial.
        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de fin inválido.")]
        public DateTime FechaFin { get; set; }

        // [Required(ErrorMessage = "El cupo es obligatorio.")]
        // [Range(1, 1000, ErrorMessage = "El cupo debe ser entre 1 y 1000.")] // Ajusta el rango según tus necesidades.
        // Asumiendo que el cupo siempre debe ser un número positivo.
        [Required(ErrorMessage = "El cupo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El cupo debe ser un número positivo.")] // O un rango específico como [Range(1, 500)]
        public int Cupo { get; set; }

        // [Required(ErrorMessage = "El lugar es obligatorio.")]
        // [StringLength(100, ErrorMessage = "El lugar no puede exceder los 100 caracteres.")]
        [Required(ErrorMessage = "El lugar es obligatorio.")]
        [StringLength(100, ErrorMessage = "El lugar no puede exceder los 100 caracteres.")]
        public string Lugar { get; set; } = null!;

        // [Required(ErrorMessage = "El tipo es obligatorio.")]
        // [StringLength(50, ErrorMessage = "El tipo no puede exceder los 50 caracteres.")]
        // Puedes añadir un [RegularExpression] si hay tipos predefinidos (ej. Taller, Conferencia, Competencia).
        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El tipo no puede exceder los 100 caracteres.")]
        public string Tipo { get; set; } = null!;

        // [Range(0.00, 100000.00, ErrorMessage = "El precio debe estar entre {1} y {2}.")]
        // Si el precio es opcional (nullable), no se necesita [Required].
        // Ajusta el rango máximo según la política de precios.
        [Range(0.00, 100000.00, ErrorMessage = "El precio debe estar entre {1} y {2}.")]
        public decimal? Precio { get; set; }

        // [StringLength(255, ErrorMessage = "La URL de la imagen no puede exceder los 255 caracteres.")]
        // [Url(ErrorMessage = "El formato de la URL de la imagen es inválido.")]
        // Si se espera una URL válida. Si es solo un nombre de archivo, el StringLength es suficiente.
        public string? ImagenEvent { get; set; } // Agregado '?' si puede ser nulo, como parece por la falta de 'null!'.
                                                 // Si siempre debe tener un valor, quita el '?' y añade [Required].
    }
}