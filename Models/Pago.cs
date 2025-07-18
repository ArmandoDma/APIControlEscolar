namespace APIControlEscolar.Models
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdAlumno { get; set; }
        public decimal Monto { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? Descripcion { get; set; }

     }
}