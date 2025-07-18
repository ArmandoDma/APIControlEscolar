﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIControlEscolar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExtraV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Extracurricular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Extracurricular");
        }
    }
}
