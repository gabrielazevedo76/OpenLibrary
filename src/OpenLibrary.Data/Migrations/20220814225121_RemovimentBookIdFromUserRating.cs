using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenLibrary.Data.Migrations
{
    public partial class RemovimentBookIdFromUserRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "UserRating");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Book");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "UserRating",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
