namespace APIControlEscolar.Models
{
    public class Bitacora
    {
        public int IdHistorialAccion { get; set; }
        public int IdUsuario { get; set; }
        public string Accion { get; set; } = null!;
        public DateTime? FechaAccion { get; set; }

    }
}