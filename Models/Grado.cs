using System;
using System.Collections.Generic;

namespace APIControlEscolar.Models;

public partial class Grado
{
    public int IdGrado { get; set; }

    public string NombreGrado { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

}
