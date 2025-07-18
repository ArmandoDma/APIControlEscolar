namespace APIControlEscolar.Models
{
    public class Periodo
    {
        public int IdPeriodo { get; set; }
        public string NombrePeriodo { get; set; } = null!;
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }

    }
}