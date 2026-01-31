using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableContatoInterno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Setor",
                table: "Contato");

            migrationBuilder.RenameColumn(
                name: "ContatoId",
                table: "Contato",
                newName: "ContatoInternoId");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Contato",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contato_DepartamentoId",
                table: "Contato",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Departamento_DepartamentoId",
                table: "Contato",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Departamento_DepartamentoId",
                table: "Contato");

            migrationBuilder.DropIndex(
                name: "IX_Contato_DepartamentoId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Contato");

            migrationBuilder.RenameColumn(
                name: "ContatoInternoId",
                table: "Contato",
                newName: "ContatoId");

            migrationBuilder.AddColumn<string>(
                name: "Setor",
                table: "Contato",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
