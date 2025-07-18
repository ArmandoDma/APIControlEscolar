using System;
using System.Collections.Generic;

namespace APIControlEscolar.Models;

public partial class Grupo
{
    public int IdGrupo { get; set; }
    public string NombreGrupo { get; set; } = null!;
    public DateTime? FechaCreacion { get; set; }
    public int IdMaestro { get; set; }
    public Maestro Maestro { get; set; } = null!;
    public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();

}
