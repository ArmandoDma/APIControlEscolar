namespace APIControlEscolar.Models
{
    public class HistorialAcademico
    {
        public int IdHistorial { get; set; }
        public int IdAlumno { get; set; }
        public int IdMateria { get; set; }
        public int IdPeriodo { get; set; }
        public decimal? CalificacionFinal { get; set; }
        public string? EstadoAcademico { get; set; }

    }
}