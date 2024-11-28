using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace back_end.Migrations
{
    /// <inheritdoc />
    public partial class IniciandoMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consorcios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<int>(type: "int", nullable: true),
                    QuantidadeCotas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consorcios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsorcioId = table.Column<int>(type: "int", nullable: false),
                    NumeroCota = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cotas_Consorcios_ConsorcioId",
                        column: x => x.ConsorcioId,
                        principalTable: "Consorcios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cadastros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CotaId = table.Column<int>(type: "int", nullable: false),
                    NumeroCota = table.Column<int>(type: "int", nullable: false),
                    NomeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parcelamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadastros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cadastros_Cotas_CotaId",
                        column: x => x.CotaId,
                        principalTable: "Cotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Consorcios",
                columns: new[] { "Id", "QuantidadeCotas", "Tipo", "Titulo", "Valor" },
                values: new object[,]
                {
                    { 1, 10, "Imóvel", "Consórcio de Casa", 500000 },
                    { 2, 10, "Carro", "Consórcio de Carro", 200000 },
                    { 3, 10, "Serviço", "Consórcio de Reforma", 70000 }
                });

            migrationBuilder.InsertData(
                table: "Cotas",
                columns: new[] { "Id", "ConsorcioId", "NumeroCota", "Status", "Tipo", "Valor" },
                values: new object[,]
                {
                    { 1, 1, 5700, "Disponível", "Imovel", 50000m },
                    { 2, 1, 5642, "Disponível", "Imovel", 50000m },
                    { 3, 1, 7286, "Disponível", "Imovel", 50000m },
                    { 4, 1, 1038, "Disponível", "Imovel", 50000m },
                    { 5, 1, 2319, "Disponível", "Imovel", 50000m },
                    { 6, 1, 6343, "Disponível", "Imovel", 50000m },
                    { 7, 1, 4510, "Disponível", "Imovel", 50000m },
                    { 8, 1, 8512, "Disponível", "Imovel", 50000m },
                    { 9, 1, 9542, "Disponível", "Imovel", 50000m },
                    { 10, 1, 7792, "Disponível", "Imovel", 50000m },
                    { 11, 2, 5700, "Disponível", "Carro", 20000m },
                    { 12, 2, 5642, "Disponível", "Carro", 20000m },
                    { 13, 2, 7286, "Disponível", "Carro", 20000m },
                    { 14, 2, 1038, "Disponível", "Carro", 20000m },
                    { 15, 2, 2319, "Disponível", "Carro", 20000m },
                    { 16, 2, 6343, "Disponível", "Carro", 20000m },
                    { 17, 2, 4510, "Disponível", "Carro", 20000m },
                    { 18, 2, 8512, "Disponível", "Carro", 20000m },
                    { 19, 2, 9542, "Disponível", "Carro", 20000m },
                    { 20, 2, 7792, "Disponível", "Carro", 20000m },
                    { 21, 3, 5700, "Disponível", "Serviço", 7000m },
                    { 22, 3, 5642, "Disponível", "Serviço", 7000m },
                    { 23, 3, 7286, "Disponível", "Serviço", 7000m },
                    { 24, 3, 1038, "Disponível", "Serviço", 7000m },
                    { 25, 3, 2319, "Disponível", "Serviço", 7000m },
                    { 26, 3, 6343, "Disponível", "Serviço", 7000m },
                    { 27, 3, 4510, "Disponível", "Serviço", 7000m },
                    { 28, 3, 8512, "Disponível", "Serviço", 7000m },
                    { 29, 3, 9542, "Disponível", "Serviço", 7000m },
                    { 30, 3, 7792, "Disponível", "Serviço", 7000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cadastros_CotaId",
                table: "Cadastros",
                column: "CotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotas_ConsorcioId",
                table: "Cotas",
                column: "ConsorcioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cadastros");

            migrationBuilder.DropTable(
                name: "Cotas");

            migrationBuilder.DropTable(
                name: "Consorcios");
        }
    }
}
