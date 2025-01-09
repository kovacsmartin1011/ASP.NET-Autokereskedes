using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autokereskedes.Data.Migrations
{
    /// <inheritdoc />
    public partial class B1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favourites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AutoId",
                table: "Favourites",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_AutoId",
                table: "Favourites",
                column: "AutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Autos_AutoId",
                table: "Favourites",
                column: "AutoId",
                principalTable: "Autos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Autos_AutoId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_AutoId",
                table: "Favourites");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favourites",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AutoId",
                table: "Favourites",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
