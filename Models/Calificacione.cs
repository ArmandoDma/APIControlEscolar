using System.Text.Json.Serialization;

namespace APIControlEscolar.Models
{
    public class Calificacione
    {
        public int IdCalificacion { get; set; }
        public int IdAlumno { get; set; }
        public int IdMateria { get; set; }
        public int IdMaestro { get; set; }
        public int IdPeriodo { get; set; }
        public decimal? CalificacionParcial1 { get; set; }
        public decimal? CalificacionParcial2 { get; set; }
        public decimal? CalificacionParcial3 { get; set; }

        [JsonIgnore]
        public decimal? CalificacionFinal { get; set; }

        [JsonIgnore]
        public DateTime? Fecha_Registro { get; set; }

    }
}