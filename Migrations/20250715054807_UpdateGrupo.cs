using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGrupo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMaestro",
                table: "GRUPO",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_GRUPO_IdMaestro",
                table: "GRUPO",
                column: "IdMaestro");

            migrationBuilder.AddForeignKey(
                name: "FK_GRUPO_MAESTRO_IdMaestro",
                table: "GRUPO",
                column: "IdMaestro",
                principalTable: "MAESTRO",
                principalColumn: "IdMaestro",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRUPO_MAESTRO_IdMaestro",
                table: "GRUPO");

            migrationBuilder.DropIndex(
                name: "IX_GRUPO_IdMaestro",
                table: "GRUPO");

            migrationBuilder.DropColumn(
                name: "IdMaestro",
                table: "GRUPO");
        }
    }
}
