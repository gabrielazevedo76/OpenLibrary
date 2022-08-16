using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenLibrary.Data.Migrations
{
    public partial class AdjustBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Catogories_CategoryId",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catogories",
                table: "Catogories");

            migrationBuilder.RenameTable(
                name: "Catogories",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_CategoryId",
                table: "Book",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_CategoryId",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Catogories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Catogories",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catogories",
                table: "Catogories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Catogories_CategoryId",
                table: "Book",
                column: "CategoryId",
                principalTable: "Catogories",
                principalColumn: "Id");
        }
    }
}
