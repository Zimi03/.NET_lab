using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIConnect.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    timestamp = table.Column<long>(type: "INTEGER", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrenciesRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExchangeCurrency = table.Column<string>(type: "TEXT", nullable: false),
                    ExchangeRate = table.Column<float>(type: "REAL", nullable: false),
                    BaseCurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrenciesRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrenciesRates_BaseCurrencies_BaseCurrencyId",
                        column: x => x.BaseCurrencyId,
                        principalTable: "BaseCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrenciesRates_BaseCurrencyId",
                table: "CurrenciesRates",
                column: "BaseCurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrenciesRates");

            migrationBuilder.DropTable(
                name: "BaseCurrencies");
        }
    }
}
