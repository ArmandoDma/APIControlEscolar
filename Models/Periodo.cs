using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System; // Necesario para DateTime (aunque uses DateOnly, a veces es útil para lógicas)
using APIControlEscolar.ValidationAttributes; // Asegúrate de tener esta referencia si usas un atributo personalizado como ValidacionPeriodo

namespace APIControlEscolar.Models
{
    public class Periodo
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdPeriodo { get; set; }

        [Required(ErrorMessage = "El nombre del periodo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del periodo no puede exceder los 50 caracteres.")]
        public string NombrePeriodo { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de inicio del periodo es obligatoria.")]
        // [DataType(DataType.Date, ErrorMessage = "Formato de fecha de inicio inválido.")] // DateOnly no siempre lo necesita, pero puede ser útil para metadatos
        // Opcional: Si los periodos no pueden empezar en el pasado (excepto si son registros históricos).
        // [FechaNoFutura(ErrorMessage = "La fecha de inicio no puede ser en el futuro.")] // Atributo personalizado si lo necesitas
        public DateOnly FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin del periodo es obligatoria.")]
        // [DataType(DataType.Date, ErrorMessage = "Formato de fecha de fin inválido.")] // Similar a FechaInicio
        // A continuación, una validación personalizada para asegurar que FechaFin sea posterior a FechaInicio.
        // Esta es una lógica de negocio clave y no se puede hacer con atributos estándar.
        // Opción 1: Un atributo de validación a nivel de clase (mejor si la validación involucra múltiples propiedades)
        // OOpción 2: Validar en el controlador (si es simple y no se reutiliza mucho)
        // Opción 3: Un atributo personalizado que compara la propiedad con otra (como el ejemplo PeriodoFechasValidasAttribute)
        public DateOnly FechaFin { get; set; }

        // --- Para la validación de que FechaFin sea posterior a FechaInicio ---
        // Opción Recomendada: Atributo de Validación a nivel de Clase (más limpio para lógica entre propiedades)
        // O puedes definir un atributo personalizado que reciba el nombre de la otra propiedad a comparar.

        // Ejemplo de cómo sería un atributo de validación a nivel de clase (tendrías que crearlo):
        // [PeriodoFechasValidas(ErrorMessage = "La fecha de fin debe ser posterior a la fecha de inicio.")]
        // public class Periodo
        // { ... } // El atributo iría encima de la declaración de la clase
    }
}