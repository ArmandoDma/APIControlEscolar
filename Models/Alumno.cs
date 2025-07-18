using System.ComponentModel.DataAnnotations;

namespace APIControlEscolar.Models
{
    public class Alumno
    {
        public int IdAlumno { get; set; }

        [MaxLength(10)]
        public string Matricula { get; set; } = null!;

        [MaxLength(50)]
        public string Nombre { get; set; } = null!;

        [MaxLength(50)]
        public string ApellidoPaterno { get; set; } = null!;

        [MaxLength(50)]
        public string ApellidoMaterno { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        [MaxLength(1)]
        public string? Genero { get; set; }

        [MaxLength(10)]
        public string? Telefono { get; set; }

        [MaxLength(100)]
        public string? Direccion { get; set; }

        [MaxLength(10)]
        public string CodigoPostal { get; set; } = null!;

        public int IdMunicipio { get; set; }
        public int IdEstado { get; set; }
        public int IdCarrera { get; set; }
        public int IdTurno { get; set; }
        public int IdGrado { get; set; }
        public int IdGrupo { get; set; }
        public int IdPeriodo { get; set; }

        [MaxLength(20)]
        public string? EstadoAlumno { get; set; }     
        public string? ImageAlumno { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public Grado Grado { get; set; } = null!;
        public Grupo Grupo { get; set; } = null!;
        public Carrera Carrera { get; set; } = null!;
    }
}
