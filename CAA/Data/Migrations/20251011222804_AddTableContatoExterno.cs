using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableContatoExterno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Departamento_DepartamentoId",
                table: "Contato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contato",
                table: "Contato");

            migrationBuilder.RenameTable(
                name: "Contato",
                newName: "ContatoInterno");

            migrationBuilder.RenameIndex(
                name: "IX_Contato_DepartamentoId",
                table: "ContatoInterno",
                newName: "IX_ContatoInterno_DepartamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContatoInterno",
                table: "ContatoInterno",
                column: "ContatoInternoId");

            migrationBuilder.CreateTable(
                name: "ContatoExterno",
                columns: table => new
                {
                    ContatoExternoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoExterno", x => x.ContatoExternoId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContatoInterno_Departamento_DepartamentoId",
                table: "ContatoInterno",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContatoInterno_Departamento_DepartamentoId",
                table: "ContatoInterno");

            migrationBuilder.DropTable(
                name: "ContatoExterno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContatoInterno",
                table: "ContatoInterno");

            migrationBuilder.RenameTable(
                name: "ContatoInterno",
                newName: "Contato");

            migrationBuilder.RenameIndex(
                name: "IX_ContatoInterno_DepartamentoId",
                table: "Contato",
                newName: "IX_Contato_DepartamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contato",
                table: "Contato",
                column: "ContatoInternoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Departamento_DepartamentoId",
                table: "Contato",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
