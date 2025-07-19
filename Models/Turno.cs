using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    // El atributo [TurnoHorasValidas] se ha eliminado ya que las propiedades de hora ya no existen.
    public class Turno
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        public int IdTurno { get; set; }

        [Required(ErrorMessage = "El nombre del turno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del turno no puede exceder los 50 caracteres.")]
        // El nombre solo puede ser "Vespertino" o "Matutino".
        [RegularExpression("^(Vespertino|Matutino)$", ErrorMessage = "El nombre del turno solo puede ser 'Vespertino' o 'Matutino'.")]
        public string Nombre { get; set; } = null!;

        // Las propiedades HoraInicio y HoraFin han sido eliminadas según tu indicación.
    }
}