using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiMotoRental.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    datanascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pessoa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    tipousuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pessoadocumento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero = table.Column<string>(type: "text", nullable: false),
                    dataemissao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    imagemdocumento = table.Column<byte>(type: "smallint", nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    pessoaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pessoadocumento", x => x.id);
                    table.ForeignKey(
                        name: "fk_pessoadocumento_pessoa_pessoaid",
                        column: x => x.pessoaid,
                        principalTable: "pessoa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pessoadocumentocnh",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pessoadocumentoid = table.Column<int>(type: "integer", nullable: false),
                    datavencimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    primeirahabilitacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pessoadocumentocnh", x => x.id);
                    table.ForeignKey(
                        name: "fk_pessoadocumentocnh_pessoadocumento_pessoadocumentoid",
                        column: x => x.pessoadocumentoid,
                        principalTable: "pessoadocumento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pessoadocumentotipocnh",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pessoadocumentocnhid = table.Column<int>(type: "integer", nullable: false),
                    tipocnh = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pessoadocumentotipocnh", x => x.id);
                    table.ForeignKey(
                        name: "fk_pessoadocumentotipocnh_pessoadocumentocnh_pessoadocumentocn~",
                        column: x => x.pessoadocumentocnhid,
                        principalTable: "pessoadocumentocnh",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pessoadocumento_numero_tipo",
                table: "pessoadocumento",
                columns: new[] { "numero", "tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pessoadocumento_pessoaid",
                table: "pessoadocumento",
                column: "pessoaid");

            migrationBuilder.CreateIndex(
                name: "ix_pessoadocumentocnh_pessoadocumentoid",
                table: "pessoadocumentocnh",
                column: "pessoadocumentoid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pessoadocumentotipocnh_pessoadocumentocnhid",
                table: "pessoadocumentotipocnh",
                column: "pessoadocumentocnhid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pessoadocumentotipocnh");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "pessoadocumentocnh");

            migrationBuilder.DropTable(
                name: "pessoadocumento");

            migrationBuilder.DropTable(
                name: "pessoa");
        }
    }
}
