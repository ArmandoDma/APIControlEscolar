using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAlumno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarreraIdCarrera",
                table: "ALUMNO",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ALUMNO_CarreraIdCarrera",
                table: "ALUMNO",
                column: "CarreraIdCarrera");

            migrationBuilder.AddForeignKey(
                name: "FK_ALUMNO_CARRERA_CarreraIdCarrera",
                table: "ALUMNO",
                column: "CarreraIdCarrera",
                principalTable: "CARRERA",
                principalColumn: "IdCarrera");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ALUMNO_CARRERA_CarreraIdCarrera",
                table: "ALUMNO");

            migrationBuilder.DropIndex(
                name: "IX_ALUMNO_CarreraIdCarrera",
                table: "ALUMNO");

            migrationBuilder.DropColumn(
                name: "CarreraIdCarrera",
                table: "ALUMNO");
        }
    }
}
