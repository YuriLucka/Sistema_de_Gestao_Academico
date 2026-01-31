using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEstagio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estagio",
                columns: table => new
                {
                    EstagioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    TipoContratoEstagioId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Integradora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VigenciaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VigenciaTermino = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Apolice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seguradora = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estagio", x => x.EstagioId);
                    table.ForeignKey(
                        name: "FK_Estagio_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estagio_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estagio_TipoContratoEstagio_TipoContratoEstagioId",
                        column: x => x.TipoContratoEstagioId,
                        principalTable: "TipoContratoEstagio",
                        principalColumn: "TipoContratoEstagioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estagio_CursoId",
                table: "Estagio",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estagio_EmpresaId",
                table: "Estagio",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estagio_TipoContratoEstagioId",
                table: "Estagio",
                column: "TipoContratoEstagioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estagio");
        }
    }
}
