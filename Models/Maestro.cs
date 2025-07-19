using System.ComponentModel.DataAnnotations; // Necesario para los atributos de validación
using System.Text.Json.Serialization;     // Ya lo tienes, para [JsonIgnore]
using APIControlEscolar.ValidationAttributes; // ¡IMPORTANTE! Asegúrate de que este using apunte a la ubicación de tu atributo RangoFechaNacimientoAttribute

namespace APIControlEscolar.Models
{
    public class Maestro
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdMaestro { get; set; }

        [Required(ErrorMessage = "El número de empleado es obligatorio.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El número de empleado debe tener entre 5 y 50 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de empleado solo debe contener dígitos.")]
        public string NumeroEmpleado { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido paterno no puede exceder los 50 caracteres.")]
        public string ApellidoPaterno { get; set; } = null!;

        [Required(ErrorMessage = "El apellido materno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido materno no puede exceder los 50 caracteres.")]
        public string ApellidoMaterno { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha de nacimiento inválido.")] // Para metadatos
        // Aquí aplicamos el atributo de validación personalizada para el rango de fechas.
        // Se establece el año mínimo en 1950 y un mensaje de error claro.
        [RangoFechaNacimiento(1950, ErrorMessage = "La fecha de nacimiento debe ser a partir del año 1950 y no puede ser en el futuro.")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(1, ErrorMessage = "El género debe ser un solo carácter (M/F/O).")]
        [RegularExpression("^[MFO]$", ErrorMessage = "El género debe ser 'M', 'F' u 'O' (Masculino, Femenino, Otro).")]
        public string? Genero { get; set; }

        [StringLength(20, MinimumLength = 10, ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        [Phone(ErrorMessage = "Formato de teléfono inválido.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener solo 10 dígitos.")]
        public string? Telefono { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede exceder los 255 caracteres.")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        [StringLength(5, ErrorMessage = "El código postal no puede exceder los 5 caracteres.")]
        // Si el código postal es de 5 dígitos numéricos estrictos:
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe ser de 5 dígitos.")]
        public string CodigoPostal { get; set; } = null!;

        [Required(ErrorMessage = "El ID de municipio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de municipio debe ser un número positivo.")]
        public int IdMunicipio { get; set; }

        [Required(ErrorMessage = "El ID de estado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de estado debe ser un número positivo.")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        [StringLength(100, ErrorMessage = "La especialidad no puede exceder los 100 caracteres.")]
        public string Especialidad { get; set; } = null!;

        // IdTurno es nullable (int?), por lo que no se aplica [Required] a menos que cambies el tipo a int.
        // Si un maestro *siempre* debe tener un turno, cambia 'int?' a 'int' y agrega [Required].
        [Range(1, int.MaxValue, ErrorMessage = "El ID de turno debe ser un número positivo.")]
        public int? IdTurno { get; set; }

        [StringLength(20, ErrorMessage = "El estado del maestro no puede exceder los 20 caracteres.")]
        // Si tienes una lista fija de estados (ej. Activo, Inactivo, Jubilado), puedes usar:
        [RegularExpression("^(Activo|Inactivo|Jubilado|Licencia)$", ErrorMessage = "Estado de maestro no válido.")]
        public string? EstadoMaestro { get; set; }

        [StringLength(255, ErrorMessage = "La URL de la imagen no puede exceder los 255 caracteres.")]
        [Url(ErrorMessage = "El formato de la URL de la imagen es inválido.")]
        public string? ImageMaestro { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }

        // Propiedades de navegación para relaciones (no necesitan validación directa aquí)
        public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

        // Si tuvieras otras colecciones relacionadas, irían aquí:
        // public virtual ICollection<Materia> MateriasImpartidas { get; set; } = new List<Materia>();
    }
}