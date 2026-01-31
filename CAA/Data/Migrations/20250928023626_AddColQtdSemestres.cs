using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColQtdSemestres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtdSemestres",
                table: "Cursos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdSemestres",
                table: "Cursos");
        }
    }
}
