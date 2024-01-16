using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRentalUserKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Readers_ReaderLibraryUserId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ReaderLibraryUserId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReaderLibraryUserId",
                table: "Rentals");

            migrationBuilder.AlterColumn<string>(
                name: "ReaderId",
                table: "Rentals",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ReaderId",
                table: "Rentals",
                column: "ReaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Readers_ReaderId",
                table: "Rentals",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "LibraryUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Readers_ReaderId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ReaderId",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "ReaderId",
                table: "Rentals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ReaderLibraryUserId",
                table: "Rentals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ReaderLibraryUserId",
                table: "Rentals",
                column: "ReaderLibraryUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Readers_ReaderLibraryUserId",
                table: "Rentals",
                column: "ReaderLibraryUserId",
                principalTable: "Readers",
                principalColumn: "LibraryUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
