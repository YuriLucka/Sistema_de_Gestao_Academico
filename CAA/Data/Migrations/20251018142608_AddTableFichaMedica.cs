using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableFichaMedica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FichaMedica",
                columns: table => new
                {
                    FichaMedicaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RA = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Diabetes = table.Column<bool>(type: "bit", nullable: false),
                    CriseConvulsiva = table.Column<bool>(type: "bit", nullable: false),
                    Taquicardia = table.Column<bool>(type: "bit", nullable: false),
                    Bronquite = table.Column<bool>(type: "bit", nullable: false),
                    Rinite = table.Column<bool>(type: "bit", nullable: false),
                    Sinusite = table.Column<bool>(type: "bit", nullable: false),
                    OutrosProblemas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MedicamentoSim = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AlergiaMedicamentos = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AlergiaInsetos = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AlergiaAlimentos = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AlergiaSimQual = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TratamentoSim = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ConvenioMedico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DefCegueira = table.Column<bool>(type: "bit", nullable: false),
                    DefBaixaVisao = table.Column<bool>(type: "bit", nullable: false),
                    DefSurdocegueira = table.Column<bool>(type: "bit", nullable: false),
                    DefSurdez = table.Column<bool>(type: "bit", nullable: false),
                    DefAuditiva = table.Column<bool>(type: "bit", nullable: false),
                    DefFisica = table.Column<bool>(type: "bit", nullable: false),
                    DefMultipla = table.Column<bool>(type: "bit", nullable: false),
                    DefIntelectual = table.Column<bool>(type: "bit", nullable: false),
                    DeficienciaSimQual = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OutrasInformacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NomeContato1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Contato1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomeContato2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Contato2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomeContato3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Contato3 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataPreenchimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaMedica", x => x.FichaMedicaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FichaMedica");
        }
    }
}
