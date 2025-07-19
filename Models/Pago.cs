using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación

namespace APIControlEscolar.Models
{
    public class Pago
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdPago { get; set; }

        [Required(ErrorMessage = "El ID del alumno es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del alumno debe ser un número positivo.")]
        public int IdAlumno { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El monto debe ser un valor positivo entre {1} y {2}.")]
        // Se establece un mínimo de 0.01 para asegurar que no se registren pagos de cero o negativos.
        // Ajusta el rango máximo (1,000,000.00) según los montos máximos esperados de tus pagos.
        public decimal Monto { get; set; }

        // FechaPago es nullable (DateTime?), lo que indica que puede no estar presente en ciertos escenarios
        // (ej. pago recién iniciado, fecha de aplicación futura).
        // Si siempre debe tener un valor, quita el '?' y añade [Required].
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de pago inválido.")]
        // Opcional: Si la fecha de pago no puede ser en el futuro, puedes añadir una validación personalizada o en el controlador.
        public DateTime? FechaPago { get; set; }

        // Descripcion es nullable (string?), lo que indica que es opcional.
        // Si siempre debe tener una descripción, quita el '?' y añade [Required].
        [StringLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres.")]
        // Ajusta la longitud máxima según tus necesidades.
        public string? Descripcion { get; set; }

        // SUGERENCIAS ADICIONALES (Propiedades de Navegación):
        // Si tienes un modelo 'Alumno' y quieres relacionar el Pago con el alumno que lo realizó.
        // public virtual Alumno? Alumno { get; set; }
    }
}