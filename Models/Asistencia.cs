namespace APIControlEscolar.Models
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }

        public int IdAlumno { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int IdToken { get; set;}
        public AsistenciaToken AsistenciaToken { get; set;} = null!;

        public DateTime FechaHora { get; set; }
    }
}
