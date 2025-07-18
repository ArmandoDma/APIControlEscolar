using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class AttendTablesCreatedV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdMaestro",
                table: "Materium",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdCarrera",
                table: "Materium",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdGrado",
                table: "Materium",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdGrupo",
                table: "Materium",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materium_IdCarrera",
                table: "Materium",
                column: "IdCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_Materium_IdGrado",
                table: "Materium",
                column: "IdGrado");

            migrationBuilder.CreateIndex(
                name: "IX_Materium_IdGrupo",
                table: "Materium",
                column: "IdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_Materium_IdMaestro",
                table: "Materium",
                column: "IdMaestro");

            migrationBuilder.CreateIndex(
                name: "IX_ALUMNO_IdCarrera",
                table: "ALUMNO",
                column: "IdCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_ALUMNO_IdGrado",
                table: "ALUMNO",
                column: "IdGrado");

            migrationBuilder.CreateIndex(
                name: "IX_ALUMNO_IdGrupo",
                table: "ALUMNO",
                column: "IdGrupo");

            migrationBuilder.AddForeignKey(
                name: "FK_ALUMNO_CARRERA_IdCarrera",
                table: "ALUMNO",
                column: "IdCarrera",
                principalTable: "CARRERA",
                principalColumn: "IdCarrera",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ALUMNO_GRADO_IdGrado",
                table: "ALUMNO",
                column: "IdGrado",
                principalTable: "GRADO",
                principalColumn: "IdGrado",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ALUMNO_GRUPO_IdGrupo",
                table: "ALUMNO",
                column: "IdGrupo",
                principalTable: "GRUPO",
                principalColumn: "IdGrupo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materium_CARRERA_IdCarrera",
                table: "Materium",
                column: "IdCarrera",
                principalTable: "CARRERA",
                principalColumn: "IdCarrera",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materium_GRADO_IdGrado",
                table: "Materium",
                column: "IdGrado",
                principalTable: "GRADO",
                principalColumn: "IdGrado",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materium_GRUPO_IdGrupo",
                table: "Materium",
                column: "IdGrupo",
                principalTable: "GRUPO",
                principalColumn: "IdGrupo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materium_MAESTRO_IdMaestro",
                table: "Materium",
                column: "IdMaestro",
                principalTable: "MAESTRO",
                principalColumn: "IdMaestro",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ALUMNO_CARRERA_IdCarrera",
                table: "ALUMNO");

            migrationBuilder.DropForeignKey(
                name: "FK_ALUMNO_GRADO_IdGrado",
                table: "ALUMNO");

            migrationBuilder.DropForeignKey(
                name: "FK_ALUMNO_GRUPO_IdGrupo",
                table: "ALUMNO");

            migrationBuilder.DropForeignKey(
                name: "FK_Materium_CARRERA_IdCarrera",
                table: "Materium");

            migrationBuilder.DropForeignKey(
                name: "FK_Materium_GRADO_IdGrado",
                table: "Materium");

            migrationBuilder.DropForeignKey(
                name: "FK_Materium_GRUPO_IdGrupo",
                table: "Materium");

            migrationBuilder.DropForeignKey(
                name: "FK_Materium_MAESTRO_IdMaestro",
                table: "Materium");

            migrationBuilder.DropIndex(
                name: "IX_Materium_IdCarrera",
                table: "Materium");

            migrationBuilder.DropIndex(
                name: "IX_Materium_IdGrado",
                table: "Materium");

            migrationBuilder.DropIndex(
                name: "IX_Materium_IdGrupo",
                table: "Materium");

            migrationBuilder.DropIndex(
                name: "IX_Materium_IdMaestro",
                table: "Materium");

            migrationBuilder.DropIndex(
                name: "IX_ALUMNO_IdCarrera",
                table: "ALUMNO");

            migrationBuilder.DropIndex(
                name: "IX_ALUMNO_IdGrado",
                table: "ALUMNO");

            migrationBuilder.DropIndex(
                name: "IX_ALUMNO_IdGrupo",
                table: "ALUMNO");

            migrationBuilder.DropColumn(
                name: "IdCarrera",
                table: "Materium");

            migrationBuilder.DropColumn(
                name: "IdGrado",
                table: "Materium");

            migrationBuilder.DropColumn(
                name: "IdGrupo",
                table: "Materium");

            migrationBuilder.AlterColumn<string>(
                name: "IdMaestro",
                table: "Materium",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
