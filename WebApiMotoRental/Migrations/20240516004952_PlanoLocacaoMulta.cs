using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMotoRental.Migrations
{
    public partial class PlanoLocacaoMulta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "percentualmulta",
                table: "planolocacao",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "percentualmulta",
                table: "planolocacao");
        }
    }
}
