using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIControlEscolar.Models
{
    public class Maestro
    {
        public int IdMaestro { get; set; }
        [MaxLength(10)]
        public string NumeroEmpleado { get; set; } = null!;
        [MaxLength(50)]
        public string Nombre { get; set; } = null!;
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; } = null!;
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        [MaxLength(1)]
        public string? Genero { get; set; }              
        [MaxLength(10)]
        public string? Telefono { get; set; }
        [MaxLength(200)]
        public string? Direccion { get; set; }
        [MaxLength(10)]
        public string CodigoPostal { get; set; } = null!;
        public int IdMunicipio { get; set; }
        public int IdEstado { get; set; }
        [MaxLength(100)]
        public string Especialidad { get; set; } = null!;
        public int? IdTurno { get; set; }        
        [MaxLength(20)]
        public string? EstadoMaestro { get; set; }        
        public string? ImageMaestro { get; set; }
        [JsonIgnore]
        public virtual Usuario? Usuario { get; set; }

        public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
    }
}
