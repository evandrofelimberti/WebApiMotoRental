using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMotoRental.Migrations
{
    public partial class LocacaoPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pessoaid",
                table: "locacao",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_locacao_pessoaid",
                table: "locacao",
                column: "pessoaid");

            migrationBuilder.AddForeignKey(
                name: "fk_locacao_pessoa_pessoaid",
                table: "locacao",
                column: "pessoaid",
                principalTable: "pessoa",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_locacao_pessoa_pessoaid",
                table: "locacao");

            migrationBuilder.DropIndex(
                name: "ix_locacao_pessoaid",
                table: "locacao");

            migrationBuilder.DropColumn(
                name: "pessoaid",
                table: "locacao");
        }
    }
}
