using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class AttendTablesCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ALUMNO",
                columns: table => new
                {
                    IdAlumno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Genero = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CodigoPostal = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IdMunicipio = table.Column<int>(type: "int", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    IdCarrera = table.Column<int>(type: "int", nullable: false),
                    IdTurno = table.Column<int>(type: "int", nullable: false),
                    IdGrado = table.Column<int>(type: "int", nullable: false),
                    IdGrupo = table.Column<int>(type: "int", nullable: false),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false),
                    EstadoAlumno = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Activo"),
                    ImageAlumno = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALUMNO", x => x.IdAlumno);
                });

            migrationBuilder.CreateTable(
                name: "Asistencia_Token",
                columns: table => new
                {
                    IdToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia_Token", x => x.IdToken);
                });

            migrationBuilder.CreateTable(
                name: "BITACORA",
                columns: table => new
                {
                    IdHistorialAccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Accion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaAccion = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BITACORA", x => x.IdHistorialAccion);
                });

            migrationBuilder.CreateTable(
                name: "CALIFICACIONE",
                columns: table => new
                {
                    IdCalificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlumno = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    IdMaestro = table.Column<int>(type: "int", nullable: false),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false),
                    CalificacionParcial1 = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    CalificacionParcial2 = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    CalificacionParcial3 = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    CalificacionFinal = table.Column<decimal>(type: "decimal(10,6)", nullable: true, computedColumnSql: "(([CalificacionParcial1] + [CalificacionParcial2] + [CalificacionParcial3]) / 3)", stored: true),
                    FECHA_REGISTRO = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CALIFICACIONE", x => x.IdCalificacion);
                });

            migrationBuilder.CreateTable(
                name: "CARRERA",
                columns: table => new
                {
                    IdCarrera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCarrera = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARRERA", x => x.IdCarrera);
                });

            migrationBuilder.CreateTable(
                name: "ESTADO",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "GRADO",
                columns: table => new
                {
                    IdGrado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGrado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRADO", x => x.IdGrado);
                });

            migrationBuilder.CreateTable(
                name: "GRUPO",
                columns: table => new
                {
                    IdGrupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGrupo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRUPO", x => x.IdGrupo);
                });

            migrationBuilder.CreateTable(
                name: "HistorialAcademico",
                columns: table => new
                {
                    IdHistorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlumno = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false),
                    CalificacionFinal = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    EstadoAcademico = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "En Curso")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAcademico", x => x.IdHistorial);
                });

            migrationBuilder.CreateTable(
                name: "MAESTRO",
                columns: table => new
                {
                    IdMaestro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroEmpleado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CodigoPostal = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IdMunicipio = table.Column<int>(type: "int", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    Especialidad = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IdTurno = table.Column<int>(type: "int", nullable: true),
                    EstadoMaestro = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Activo"),
                    ImageMaestro = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAESTRO", x => x.IdMaestro);
                });

            migrationBuilder.CreateTable(
                name: "MaestroMaterias",
                columns: table => new
                {
                    IdMaestro = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaestroMaterias", x => new { x.IdMaestro, x.IdMateria });
                });

            migrationBuilder.CreateTable(
                name: "Materium",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMateria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IdMaestro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materium", x => x.IdMateria);
                });

            migrationBuilder.CreateTable(
                name: "MUNICIPIO",
                columns: table => new
                {
                    IdMunicipio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNICIPIO", x => x.IdMunicipio);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlumno = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.IdPago);
                });

            migrationBuilder.CreateTable(
                name: "PERIODO",
                columns: table => new
                {
                    IdPeriodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePeriodo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERIODO", x => x.IdPeriodo);
                });

            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROL", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "TURNO",
                columns: table => new
                {
                    IdTurno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TURNO", x => x.IdTurno);
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    IdAsistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlumno = table.Column<int>(type: "int", nullable: false),
                    IdToken = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.IdAsistencia);
                    table.ForeignKey(
                        name: "FK_Asistencia_ALUMNO_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "ALUMNO",
                        principalColumn: "IdAlumno",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencia_Asistencia_Token_IdToken",
                        column: x => x.IdToken,
                        principalTable: "Asistencia_Token",
                        principalColumn: "IdToken",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Salt = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdAlumno = table.Column<int>(type: "int", nullable: true),
                    IdMaestro = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValue: "Activo"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USUARIO__5B65BF97056394F7", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK__USUARIO__IdAlumn__778AC167",
                        column: x => x.IdAlumno,
                        principalTable: "ALUMNO",
                        principalColumn: "IdAlumno");
                    table.ForeignKey(
                        name: "FK__USUARIO__IdMaest__787EE5A0",
                        column: x => x.IdMaestro,
                        principalTable: "MAESTRO",
                        principalColumn: "IdMaestro");
                    table.ForeignKey(
                        name: "FK__USUARIO__IdRol__76969D2E",
                        column: x => x.IdRol,
                        principalTable: "ROL",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALUMNO_Matricula",
                table: "ALUMNO",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_IdAlumno",
                table: "Asistencia",
                column: "IdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_IdToken",
                table: "Asistencia",
                column: "IdToken");

            migrationBuilder.CreateIndex(
                name: "IX_CARRERA_NombreCarrera",
                table: "CARRERA",
                column: "NombreCarrera",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MAESTRO_NumeroEmpleado",
                table: "MAESTRO",
                column: "NumeroEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_IdRol",
                table: "USUARIO",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "UQ__USUARIO__460B47416B50E3D2",
                table: "USUARIO",
                column: "IdAlumno",
                unique: true,
                filter: "([IdAlumno] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__USUARIO__66B8F1886284146F",
                table: "USUARIO",
                column: "IdMaestro",
                unique: true,
                filter: "([IdMaestro] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__USUARIO__A9D10534C692B840",
                table: "USUARIO",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropTable(
                name: "BITACORA");

            migrationBuilder.DropTable(
                name: "CALIFICACIONE");

            migrationBuilder.DropTable(
                name: "CARRERA");

            migrationBuilder.DropTable(
                name: "ESTADO");

            migrationBuilder.DropTable(
                name: "GRADO");

            migrationBuilder.DropTable(
                name: "GRUPO");

            migrationBuilder.DropTable(
                name: "HistorialAcademico");

            migrationBuilder.DropTable(
                name: "MaestroMaterias");

            migrationBuilder.DropTable(
                name: "Materium");

            migrationBuilder.DropTable(
                name: "MUNICIPIO");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "PERIODO");

            migrationBuilder.DropTable(
                name: "TURNO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "Asistencia_Token");

            migrationBuilder.DropTable(
                name: "ALUMNO");

            migrationBuilder.DropTable(
                name: "MAESTRO");

            migrationBuilder.DropTable(
                name: "ROL");
        }
    }
}
