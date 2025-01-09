﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autokereskedes.Data.Migrations
{
    /// <inheritdoc />
    public partial class uj2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Autos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
