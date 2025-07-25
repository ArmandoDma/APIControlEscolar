using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class AdminsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFin",
                table: "TURNO");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "TURNO");

            migrationBuilder.AddColumn<int>(
                name: "IdAdmin",
                table: "USUARIO",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lugar",
                table: "Extracurricular",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagenEvent",
                table: "Extracurricular",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Extracurricular",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Categoria",
                table: "Extracurricular",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageAlumno",
                table: "ALUMNO",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ADMINS",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Genero = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CodigoPostal = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    IdMunicipio = table.Column<int>(type: "int", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    EstadoAdmin = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Activo"),
                    ImageAdmin = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMINS", x => x.IdAdmin);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_IdAdmin",
                table: "USUARIO",
                column: "IdAdmin",
                unique: true,
                filter: "[IdAdmin] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_ADMIN",
                table: "USUARIO",
                column: "IdAdmin",
                principalTable: "ADMINS",
                principalColumn: "IdAdmin",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_ADMIN",
                table: "USUARIO");

            migrationBuilder.DropTable(
                name: "ADMINS");

            migrationBuilder.DropIndex(
                name: "IX_USUARIO_IdAdmin",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "IdAdmin",
                table: "USUARIO");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraFin",
                table: "TURNO",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraInicio",
                table: "TURNO",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AlterColumn<string>(
                name: "Lugar",
                table: "Extracurricular",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ImagenEvent",
                table: "Extracurricular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Extracurricular",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Categoria",
                table: "Extracurricular",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ImageAlumno",
                table: "ALUMNO",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
