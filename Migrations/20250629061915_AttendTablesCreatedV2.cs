using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class AttendTablesCreatedV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMaestro",
                table: "Asistencia_Token",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_Token_IdMaestro",
                table: "Asistencia_Token",
                column: "IdMaestro");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Token_MAESTRO_IdMaestro",
                table: "Asistencia_Token",
                column: "IdMaestro",
                principalTable: "MAESTRO",
                principalColumn: "IdMaestro",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Token_MAESTRO_IdMaestro",
                table: "Asistencia_Token");

            migrationBuilder.DropIndex(
                name: "IX_Asistencia_Token_IdMaestro",
                table: "Asistencia_Token");

            migrationBuilder.DropColumn(
                name: "IdMaestro",
                table: "Asistencia_Token");
        }
    }
}
