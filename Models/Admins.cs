using APIControlEscolar.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIControlEscolar.Models
{
    public class Admins
    {
        public int IdAdmin {  get; set; }

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
        [DataType(DataType.Date)]
        [RangoFechaNacimiento(1950, ErrorMessage = "La fecha de nacimiento debe ser a partir del año 1950 y no puede ser en el futuro.")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(1, ErrorMessage = "El género debe ser un solo carácter (M/F/O).")]
        [RegularExpression("^[MFO]$", ErrorMessage = "El género debe ser 'M', 'F' u 'O'.")]
        public string? Genero { get; set; }

        [StringLength(20, MinimumLength = 10, ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        [Phone(ErrorMessage = "Formato de teléfono inválido.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener solo 10 dígitos.")]
        public string? Telefono { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede exceder los 255 caracteres.")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        [StringLength(5, ErrorMessage = "El código postal no puede exceder los 5 caracteres.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe ser de 5 dígitos.")]
        public string CodigoPostal { get; set; } = null!;

        [Required(ErrorMessage = "El ID de municipio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de municipio debe ser un número positivo.")]
        public int IdMunicipio { get; set; }

        [Required(ErrorMessage = "El ID de estado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de estado debe ser un número positivo.")]
        public int IdEstado { get; set; }

        [StringLength(20, ErrorMessage = "El estado del administrador no puede exceder los 20 caracteres.")]
        [RegularExpression("^(Activo|Inactivo|Jubilado|Licencia)$", ErrorMessage = "Estado no válido.")]
        public string? EstadoAdmin { get; set; }

        [StringLength(255, ErrorMessage = "La URL de la imagen no puede exceder los 255 caracteres.")]
        [Url(ErrorMessage = "El formato de la URL de la imagen es inválido.")]
        public string? ImageAdmin { get; set; }

        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }

    }
}
