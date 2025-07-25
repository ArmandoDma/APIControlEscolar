using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class AlumnoDateUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "ALUMNO",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "ALUMNO",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
