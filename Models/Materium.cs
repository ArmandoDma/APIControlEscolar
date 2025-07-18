namespace APIControlEscolar.Models;

public partial class Materium
{
    public int IdMateria { get; set; }

    public string NombreMateria { get; set; } = null!;

    public string ImageMat {  get; set; } = null!;

    public int IdGrado { get; set; }
    public Grado Grado { get; set; }   

    public int IdGrupo { get; set; }
    public Grupo Grupo { get; set; }   

    public int IdCarrera { get; set; }
    public Carrera Carrera { get; set; }  

    public int IdMaestro { get; set; }
    public Maestro Maestro { get; set; }

}
