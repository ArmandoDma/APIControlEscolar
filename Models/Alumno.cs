using APIControlEscolar.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Necesario para [NotMapped]

namespace APIControlEscolar.Models
{
    public class Alumno
    {
        // [Key]
        // Indica que esta propiedad es la clave primaria de la tabla.
        // Entity Framework Core la detecta por convención si se llama 'Id' o 'Id{ClassName}'.
        public int IdAlumno { get; set; }

        // [Required(ErrorMessage = "La matrícula es obligatoria.")]
        // Asegura que este campo no puede ser nulo o vacío.
        // [StringLength(10, MinimumLength = 6, ErrorMessage = "La matrícula debe tener entre 6 y 10 caracteres.")]
        // Define la longitud mínima y máxima permitida para la cadena.
        // [RegularExpression(@"^\d{6,10}$", ErrorMessage = "La matrícula debe contener solo números y tener entre 6 y 10 dígitos.")]
        // Valida que la cadena contenga solo dígitos y tenga la longitud especificada,
        // asumiendo que tu matrícula es numérica, como mencionaste anteriormente.

        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "La matrícula debe tener entre 5 y 10 caracteres.")]
        [RegularExpression(@"^\d{5,10}$", ErrorMessage = "La matrícula debe contener solo números y tener entre 5 y 10 dígitos.")]
        public string Matricula { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido paterno no puede exceder los 50 caracteres.")]
        public string ApellidoPaterno { get; set; } = null!;

        [Required(ErrorMessage = "El apellido materno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido materno no puede exceder los 50 caracteres.")]
        public string ApellidoMaterno { get; set; } = null!;

        // [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        // [DataType(DataType.Date, ErrorMessage = "Formato de fecha de nacimiento inválido.")]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // Aunque DateOnly maneja el formato, DataType.Date puede ser útil para fines de interfaz de usuario/metadatos.
        // Puedes agregar una validación personalizada para la edad si es necesario (ej., mayor de 18).

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [RangoFechaNacimiento(1950, ErrorMessage = "La fecha de nacimiento debe ser a partir del año 1950 y no puede ser en el futuro.")]
        // Si usas DateOnly:
        public DateTime FechaNacimiento { get; set; }

        // [StringLength(1, ErrorMessage = "El género debe ser un solo carácter (M/F/O).")]
        // [RegularExpression("^[MFO]$", ErrorMessage = "El género debe ser 'M', 'F' u 'O'.")]
        // Se usa '?' para indicar que es anulable, lo cual ya lo tienes.

        [StringLength(1, ErrorMessage = "El género debe ser un solo carácter (M/F/O).")]
        [RegularExpression("^[MFO]$", ErrorMessage = "El género debe ser 'M', 'F' u 'O' (Masculino, Femenino, Otro).")]
        public string? Genero { get; set; }

        // [StringLength(10, MinimumLength = 10, ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        // [Phone(ErrorMessage = "Formato de teléfono inválido.")]
        // [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener solo 10 dígitos.")]
        // Asumiendo un formato de 10 dígitos numéricos para México.

        [StringLength(10, MinimumLength = 10, ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        [Phone(ErrorMessage = "Formato de teléfono inválido.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener solo 10 dígitos.")]
        public string? Telefono { get; set; }

        [StringLength(225, ErrorMessage = "La dirección no puede exceder los 225 caracteres.")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe ser de 5 dígitos.")] // Si es el formato exacto de tu país.
         public string CodigoPostal { get; set; } = null!;
        // Podrías ajustar el Regex si el CP tiene un formato específico (ej., 5 dígitos numéricos en México).
        //[StringLength(5, ErrorMessage = "El código postal no puede exceder los 5 caracteres.")]
       

        // [Required(ErrorMessage = "El ID de municipio es obligatorio.")]
        // Puedes añadir aquí validación para que los IDs relacionados no sean 0 o negativos si eso es un problema.
        // La validación de que el ID exista en su tabla respectiva se hace en el controlador (como ya lo tenías).

        [Required(ErrorMessage = "El ID de municipio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de municipio debe ser un número positivo.")]
        public int IdMunicipio { get; set; }

        [Required(ErrorMessage = "El ID de estado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de estado debe ser un número positivo.")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El ID de carrera es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de carrera debe ser un número positivo.")]
        public int IdCarrera { get; set; }

        [Required(ErrorMessage = "El ID de turno es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de turno debe ser un número positivo.")]
        public int IdTurno { get; set; }

        [Required(ErrorMessage = "El ID de grado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de grado debe ser un número positivo.")]
        public int IdGrado { get; set; }

        [Required(ErrorMessage = "El ID de grupo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de grupo debe ser un número positivo.")]
        public int IdGrupo { get; set; }

        [Required(ErrorMessage = "El ID de periodo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de periodo debe ser un número positivo.")]
        public int IdPeriodo { get; set; }

        // [StringLength(20, ErrorMessage = "El estado del alumno no puede exceder los 20 caracteres.")]
        // [RegularExpression("^(Activo|Inactivo|Suspendido|Egresado)$", ErrorMessage = "Estado de alumno no válido.")]
        // Si tienes una lista fija de estados permitidos.

        [StringLength(20, ErrorMessage = "El estado del alumno no puede exceder los 20 caracteres.")]
        public string? EstadoAlumno { get; set; }

        // [MaxLength(255)] // Longitud típica para URLs de imágenes.
        // [Url(ErrorMessage = "El formato de la URL de la imagen es inválido.")]
        // Si se espera una URL válida.

        [MaxLength(255, ErrorMessage = "La URL de la imagen no puede exceder los 255 caracteres.")]
        [Url(ErrorMessage = "El formato de la URL de la imagen es inválido.")]
        public string? ImageAlumno { get; set; }

        // Las propiedades de navegación (relaciones con otras tablas) no suelen llevar Data Annotations de validación
        // aquí, ya que su validación es que el 'Id' correspondiente exista en la base de datos (lo que ya haces en el controlador).
        // [ValidateComplexType] // Para validar propiedades de navegación si fueran objetos complejos con sus propias validaciones.
        // [NotMapped] // Si no quieres que EF Core las mapee a la base de datos.
        public virtual Usuario? Usuario { get; set; }
        [JsonIgnore]
        public Grado? Grado { get; set; } = null!;
        [JsonIgnore]
        public Grupo? Grupo { get; set; } = null!;
        [JsonIgnore]
        public Carrera? Carrera { get; set; } = null!;

        // Asegúrate de que todas las propiedades de navegación tengan la anotación [Required] si no pueden ser null.
        // Por ejemplo, si un Alumno SIEMPRE debe tener un Grado:
        // [Required(ErrorMessage = "Debe asignarse un Grado al alumno.")]
        // public Grado Grado { get; set; } = null!;
    }
}