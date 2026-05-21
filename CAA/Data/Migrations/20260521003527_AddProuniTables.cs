using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProuniTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProuniCampoDocumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TemFrenteVerso = table.Column<bool>(type: "bit", nullable: false),
                    Obrigatorio = table.Column<bool>(type: "bit", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProuniCampoDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProuniSubmissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCandidato = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CpfCandidato = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AnalistaId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DataAnalise = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProuniSubmissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProuniDocumentosAnexados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissaoId = table.Column<int>(type: "int", nullable: false),
                    CampoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    Lado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Arquivo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NomeArquivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProuniDocumentosAnexados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProuniDocumentosAnexados_ProuniCampoDocumentos_CampoDocumentoId",
                        column: x => x.CampoDocumentoId,
                        principalTable: "ProuniCampoDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProuniDocumentosAnexados_ProuniSubmissoes_SubmissaoId",
                        column: x => x.SubmissaoId,
                        principalTable: "ProuniSubmissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProuniDocumentosAnexados_CampoDocumentoId",
                table: "ProuniDocumentosAnexados",
                column: "CampoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProuniDocumentosAnexados_SubmissaoId",
                table: "ProuniDocumentosAnexados",
                column: "SubmissaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProuniDocumentosAnexados");

            migrationBuilder.DropTable(
                name: "ProuniCampoDocumentos");

            migrationBuilder.DropTable(
                name: "ProuniSubmissoes");
        }
    }
}
