namespace APIControlEscolar.Models
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public string Nombre { get; set; } = null!;
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}