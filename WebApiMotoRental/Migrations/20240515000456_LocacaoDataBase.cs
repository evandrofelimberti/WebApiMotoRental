using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiMotoRental.Migrations
{
    public partial class LocacaoDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "planolocacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    quantidadedias = table.Column<int>(type: "integer", nullable: false),
                    valordia = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planolocacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    datainclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datainicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dataprevisaotermino = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datatermino = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    planolocacaoid = table.Column<int>(type: "integer", nullable: false),
                    valortotalaluguel = table.Column<double>(type: "double precision", nullable: false),
                    quantidadediasaluguel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locacao", x => x.id);
                    table.ForeignKey(
                        name: "fk_locacao_planolocacao_planolocacaoid",
                        column: x => x.planolocacaoid,
                        principalTable: "planolocacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_locacao_planolocacaoid",
                table: "locacao",
                column: "planolocacaoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locacao");

            migrationBuilder.DropTable(
                name: "planolocacao");
        }
    }
}
