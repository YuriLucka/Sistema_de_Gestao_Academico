using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTipoDesconto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Desconto");

            migrationBuilder.AddColumn<int>(
                name: "TipoDescontoId",
                table: "Desconto",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoDesconto",
                columns: table => new
                {
                    TipoDescontoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDesconto", x => x.TipoDescontoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desconto_TipoDescontoId",
                table: "Desconto",
                column: "TipoDescontoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desconto_TipoDesconto_TipoDescontoId",
                table: "Desconto",
                column: "TipoDescontoId",
                principalTable: "TipoDesconto",
                principalColumn: "TipoDescontoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desconto_TipoDesconto_TipoDescontoId",
                table: "Desconto");

            migrationBuilder.DropTable(
                name: "TipoDesconto");

            migrationBuilder.DropIndex(
                name: "IX_Desconto_TipoDescontoId",
                table: "Desconto");

            migrationBuilder.DropColumn(
                name: "TipoDescontoId",
                table: "Desconto");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Desconto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
