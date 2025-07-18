using System.ComponentModel.DataAnnotations;

namespace APIControlEscolar.Models
{
    public class Carrera
    {
        public int IdCarrera { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
        public string NombreCarrera { get; set; } = null!;

        public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
    }
}