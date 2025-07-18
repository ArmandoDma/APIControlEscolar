using Microsoft.EntityFrameworkCore;
using APIControlEscolar.Models;

namespace APIControlEscolar.Data
{
    public class CONTROL_ESCOLAR : DbContext
    {
        public CONTROL_ESCOLAR(DbContextOptions<CONTROL_ESCOLAR> options) : base(options) { }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Maestro> Maestros { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Materium> Materias { get; set; }
        public DbSet<HistorialAcademico> HistorialAcademico { get; set; }
        public DbSet<Calificacione> Calificaciones { get; set; }
        public DbSet<MaestroMaterium> MaestroMaterias { get; set; }
        public DbSet<Bitacora> Bitacoras { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<AsistenciaToken> AsistenciaTokens { get; set; }

        public DbSet<Extracurricular> Extracurricular {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Alumno
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno);
                entity.ToTable("ALUMNO");

                entity.HasIndex(e => e.Matricula).IsUnique();

                entity.Property(e => e.Matricula).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ApellidoPaterno).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ApellidoMaterno).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Genero).HasMaxLength(1).IsUnicode(false).IsFixedLength();
                entity.Property(e => e.Telefono).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Direccion).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.CodigoPostal).HasMaxLength(5).IsUnicode(false).IsFixedLength();
                entity.Property(e => e.EstadoAlumno).HasMaxLength(20).IsUnicode(false).HasDefaultValue("Activo");
                
                entity.HasOne(e => e.Grado)
                      .WithMany()
                      .HasForeignKey(e => e.IdGrado)
                      .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(e => e.Grupo)
                      .WithMany()
                      .HasForeignKey(e => e.IdGrupo)
                      .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(e => e.Carrera)
                      .WithMany()
                      .HasForeignKey(e => e.IdCarrera)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Configuración de AsistenciaToken
            modelBuilder.Entity<AsistenciaToken>(entity =>
            {
                entity.HasKey(e => e.IdToken); 
                entity.ToTable("Asistencia_Token");

                entity.Property(e => e.Token)
                      .HasMaxLength(255) 
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("datetime");

                entity.Property(e => e.ExpiresAt)
                      .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                      .HasDefaultValue(true);

                entity.HasOne(at => at.Maestro)
                      .WithMany() // O .WithMany(m => m.AsistenciaTokens) si lo quieres en Maestro
                      .HasForeignKey(at => at.IdMaestro)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Asistencia
            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(e => e.IdAsistencia); 
                entity.ToTable("Asistencia");

                entity.Property(e => e.FechaHora)
                      .HasColumnType("datetime");

                // Relación con Alumno
                entity.HasOne(e => e.Alumno)
                      .WithMany() // Si quieres una colección, agrega en Alumno: ICollection<Asistencia>
                      .HasForeignKey(e => e.IdAlumno)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con AsistenciaToken
                entity.HasOne(e => e.AsistenciaToken)
                      .WithMany(t => t.AsistenciaList)
                      .HasForeignKey(e => e.IdToken)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Configuración de Maestro
            modelBuilder.Entity<Maestro>(entity =>
            {
                entity.HasKey(e => e.IdMaestro);
                entity.ToTable("MAESTRO");

                entity.HasIndex(e => e.NumeroEmpleado).IsUnique();


                entity.Property(e => e.NumeroEmpleado).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ApellidoPaterno).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ApellidoMaterno).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Genero).HasMaxLength(1).IsUnicode(false).IsFixedLength();
                entity.Property(e => e.Telefono).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Direccion).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.CodigoPostal).HasMaxLength(5).IsUnicode(false).IsFixedLength();
                entity.Property(e => e.Especialidad).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.EstadoMaestro).HasMaxLength(20).IsUnicode(false).HasDefaultValue("Activo");
                entity.Property(e => e.ImageMaestro).HasMaxLength(255).IsUnicode(false);

            });

            // Configuración de Estado
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);
                entity.ToTable("ESTADO");

                entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);
            });

            // Configuración de Municipio
            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio);
                entity.ToTable("MUNICIPIO");

                entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);

            });

            // Configuración de Carrera
            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => e.IdCarrera);
                entity.ToTable("CARRERA");

                entity.HasIndex(e => e.NombreCarrera).IsUnique();

                entity.Property(e => e.NombreCarrera).HasMaxLength(100).IsUnicode(false);
            });

            // Configuración de Turno
            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasKey(e => e.IdTurno);
                entity.ToTable("TURNO");

                entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);
            });

            // Configuración de Grado
            modelBuilder.Entity<Grado>(entity =>
            {
                entity.HasKey(e => e.IdGrado);
                entity.ToTable("GRADO");

                entity.Property(e => e.NombreGrado).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");
            });

            // Configuración de Grupo
            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo);
                entity.ToTable("GRUPO");

                entity.Property(e => e.NombreGrupo).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");

                // Configura la FK y la relación
                entity.HasOne(e => e.Maestro)
                      .WithMany(m => m.Grupos)
                      .HasForeignKey(e => e.IdMaestro)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Alumnos)
                      .WithOne(a => a.Grupo)
                      .HasForeignKey(a => a.IdGrupo);
            });


            // Configuración de Periodo
            modelBuilder.Entity<Periodo>(entity =>
            {
                entity.HasKey(e => e.IdPeriodo);
                entity.ToTable("PERIODO");

                entity.Property(e => e.NombrePeriodo).HasMaxLength(50).IsUnicode(false);
            });

            // Configuración de Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);
                entity.ToTable("ROL");

                entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);
            });

            // Configuración de Materium
            modelBuilder.Entity<Materium>(entity =>
            {
                entity.HasKey(e => e.IdMateria);
                entity.ToTable("Materium");

                entity.Property(e => e.NombreMateria).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.ImageMat).HasMaxLength(255).IsUnicode(false);

                entity.HasOne(m => m.Grado)
                    .WithMany()
                    .HasForeignKey(m => m.IdGrado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Grupo)
                    .WithMany()
                    .HasForeignKey(m => m.IdGrupo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Carrera)
                    .WithMany()
                    .HasForeignKey(m => m.IdCarrera)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Maestro)
                    .WithMany()
                    .HasForeignKey(m => m.IdMaestro)
                    .OnDelete(DeleteBehavior.Restrict);
            });            

            // Configuración de HistorialAcademico
            modelBuilder.Entity<HistorialAcademico>(entity =>
            {
                entity.HasKey(e => e.IdHistorial);
                entity.ToTable("HistorialAcademico");

                entity.Property(e => e.CalificacionFinal).HasColumnType("decimal(5,2)");
                entity.Property(e => e.EstadoAcademico).HasMaxLength(20).IsUnicode(false).HasDefaultValue("En Curso");

            });

            // Configuración de Pago
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago);
                entity.ToTable("Pago");

                entity.Property(e => e.Monto).HasColumnType("decimal(10,2)");
                entity.Property(e => e.FechaPago).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Descripcion).HasMaxLength(255).IsUnicode(false);

            });

            // Configuración de Calificacione
            modelBuilder.Entity<Calificacione>(entity =>
            {
                entity.HasKey(e => e.IdCalificacion);
                entity.ToTable("CALIFICACIONE");

                entity.Property(e => e.CalificacionParcial1).HasColumnType("decimal(3,1)");
                entity.Property(e => e.CalificacionParcial2).HasColumnType("decimal(3,1)");
                entity.Property(e => e.CalificacionParcial3).HasColumnType("decimal(3,1)");
                entity.Property(e => e.CalificacionFinal).HasColumnType("decimal(10,6)")
                    .HasComputedColumnSql("(([CalificacionParcial1] + [CalificacionParcial2] + [CalificacionParcial3]) / 3)", stored: true);
                entity.Property(e => e.Fecha_Registro).HasColumnName("FECHA_REGISTRO").HasDefaultValueSql("GETDATE()");

            });

            // Configuración de MaestroMaterium
            modelBuilder.Entity<MaestroMaterium>(entity =>
            {
                // Clave compuesta
                entity.HasKey(e => new { e.IdMaestro, e.IdMateria });

            });

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF97056394F7");

                entity.ToTable("USUARIO");

                entity.HasIndex(e => e.IdRol, "IX_USUARIO_IdRol");

                entity.HasIndex(e => e.IdAlumno, "UQ__USUARIO__460B47416B50E3D2")
                    .IsUnique()
                    .HasFilter("([IdAlumno] IS NOT NULL)");

                entity.HasIndex(e => e.IdMaestro, "UQ__USUARIO__66B8F1886284146F")
                    .IsUnique()
                    .HasFilter("([IdMaestro] IS NOT NULL)");

                entity.HasIndex(e => e.Email, "UQ__USUARIO__A9D10534C692B840").IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValue("Activo");

                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Salt)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlumnoNavigation).WithOne(p => p.Usuario)
                    .HasForeignKey<Usuario>(d => d.IdAlumno)
                    .HasConstraintName("FK__USUARIO__IdAlumn__778AC167");

                entity.HasOne(d => d.IdMaestroNavigation).WithOne(p => p.Usuario)
                    .HasForeignKey<Usuario>(d => d.IdMaestro)
                    .HasConstraintName("FK__USUARIO__IdMaest__787EE5A0");

                entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USUARIO__IdRol__76969D2E");
            });

            // Configuración de Bitacora
            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.IdHistorialAccion);
                entity.ToTable("BITACORA");

                entity.Property(e => e.Accion).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.FechaAccion).HasDefaultValueSql("GETDATE()");

            });

            modelBuilder.Entity<Extracurricular>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Extracurricular");
                entity.Property(e => e.Nombre).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Tipo).HasMaxLength(100).IsRequired();
            });

        }
    }
}