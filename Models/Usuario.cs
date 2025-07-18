namespace APIControlEscolar.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public int IdRol { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMaestro { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public virtual Alumno? IdAlumnoNavigation { get; set; }

        public virtual Maestro? IdMaestroNavigation { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}