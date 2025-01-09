using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autokereskedes.Data.Migrations
{
    /// <inheritdoc />
    public partial class ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Body_Type",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Autos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Extras",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fuel_Type",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Autos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Autos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body_Type",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Extras",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Fuel_Type",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Autos");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Autos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
