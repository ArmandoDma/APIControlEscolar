namespace APIControlEscolar.Models
{
    public class AsistenciaToken
    {
        public int IdToken { get; set; }
        public string Token { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsActive { get; set; }

        public int IdMaestro { get; set; } 
        public Maestro Maestro { get; set; } = null!;  

        public ICollection<Asistencia> AsistenciaList { get; set; } = new List<Asistencia>();
    }
}
