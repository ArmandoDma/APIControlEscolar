namespace APIControlEscolar.Models
{
    public class Extracurricular
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Cupo { get; set; }
        public string Lugar { get; set; } = null!;
        public string Tipo { get; set; } = null!; 
        public decimal? Precio { get; set; }        
        public string ImagenEvent {  get; set; }
    }
}
