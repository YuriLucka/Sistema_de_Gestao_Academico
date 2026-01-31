using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEnumTIpoDescontoValor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoDescontoValor",
                table: "TipoDesconto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoDescontoValor",
                table: "Desconto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoDescontoValor",
                table: "TipoDesconto");

            migrationBuilder.DropColumn(
                name: "TipoDescontoValor",
                table: "Desconto");
        }
    }
}
