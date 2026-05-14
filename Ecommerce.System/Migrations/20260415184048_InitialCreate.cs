using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Imie = table.Column<string>(type: "text", nullable: false),
                    Nazwisko = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    NumerTelefonu = table.Column<string>(type: "text", nullable: false),
                    Adres = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PozycjeZamowienia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ZamowienieId = table.Column<Guid>(type: "uuid", nullable: false),
                    WariantProduktuId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ilosc = table.Column<int>(type: "integer", nullable: false),
                    CenaWChwiliZakupu = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjeZamowienia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nazwa = table.Column<string>(type: "text", nullable: false),
                    Marka = table.Column<string>(type: "text", nullable: false),
                    DataUtworzenia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KlientId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataZamowienia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CalkowitaWartosc = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AdresDostawy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warianty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProduktId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ean = table.Column<string>(type: "text", nullable: false),
                    Cena = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    StanMagazynowy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warianty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warianty_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atrybuty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WariantProduktuId = table.Column<Guid>(type: "uuid", nullable: false),
                    Klucz = table.Column<string>(type: "text", nullable: false),
                    Wartosc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atrybuty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atrybuty_Warianty_WariantProduktuId",
                        column: x => x.WariantProduktuId,
                        principalTable: "Warianty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atrybuty_WariantProduktuId",
                table: "Atrybuty",
                column: "WariantProduktuId");

            migrationBuilder.CreateIndex(
                name: "IX_Warianty_ProduktId",
                table: "Warianty",
                column: "ProduktId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atrybuty");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "PozycjeZamowienia");

            migrationBuilder.DropTable(
                name: "Zamowienia");

            migrationBuilder.DropTable(
                name: "Warianty");

            migrationBuilder.DropTable(
                name: "Produkty");
        }
    }
}
