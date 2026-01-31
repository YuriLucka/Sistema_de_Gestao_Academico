using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableParametroGeral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParametroGeral",
                columns: table => new
                {
                    ParametroGeralId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenhaFichaMedica = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametroGeral", x => x.ParametroGeralId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParametroGeral");
        }
    }
}
