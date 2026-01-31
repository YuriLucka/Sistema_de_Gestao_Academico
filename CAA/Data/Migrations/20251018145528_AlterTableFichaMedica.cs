using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFichaMedica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TratamentoSim",
                table: "FichaMedica",
                newName: "TratamentoMedico");

            migrationBuilder.RenameColumn(
                name: "OutrosProblemas",
                table: "FichaMedica",
                newName: "OutrosProblemasCronicos");

            migrationBuilder.RenameColumn(
                name: "OutrasInformacoes",
                table: "FichaMedica",
                newName: "InformacoesAdicionais");

            migrationBuilder.RenameColumn(
                name: "MedicamentoSim",
                table: "FichaMedica",
                newName: "OutrasDeficiencias");

            migrationBuilder.RenameColumn(
                name: "DeficienciaSimQual",
                table: "FichaMedica",
                newName: "Medicamentos");

            migrationBuilder.RenameColumn(
                name: "AlergiaSimQual",
                table: "FichaMedica",
                newName: "OutrasAlergias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TratamentoMedico",
                table: "FichaMedica",
                newName: "TratamentoSim");

            migrationBuilder.RenameColumn(
                name: "OutrosProblemasCronicos",
                table: "FichaMedica",
                newName: "OutrosProblemas");

            migrationBuilder.RenameColumn(
                name: "OutrasDeficiencias",
                table: "FichaMedica",
                newName: "MedicamentoSim");

            migrationBuilder.RenameColumn(
                name: "OutrasAlergias",
                table: "FichaMedica",
                newName: "AlergiaSimQual");

            migrationBuilder.RenameColumn(
                name: "Medicamentos",
                table: "FichaMedica",
                newName: "DeficienciaSimQual");

            migrationBuilder.RenameColumn(
                name: "InformacoesAdicionais",
                table: "FichaMedica",
                newName: "OutrasInformacoes");
        }
    }
}
