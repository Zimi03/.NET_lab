using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingApp.Migrations
{
    /// <inheritdoc />
    public partial class BookTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUri",
                table: "Book",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortReview",
                table: "Book",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUri",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ShortReview",
                table: "Book");
        }
    }
}
