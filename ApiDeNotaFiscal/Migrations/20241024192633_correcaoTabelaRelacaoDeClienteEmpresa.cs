using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDeNotaFiscal.Migrations
{
    /// <inheritdoc />
    public partial class correcaoTabelaRelacaoDeClienteEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteEmpresa",
                columns: table => new
                {
                    ClientesClienteId = table.Column<int>(type: "int", nullable: false),
                    EmpresasEmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteEmpresa", x => new { x.ClientesClienteId, x.EmpresasEmpresaId });
                    table.ForeignKey(
                        name: "FK_ClienteEmpresa_Clientes_ClientesClienteId",
                        column: x => x.ClientesClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteEmpresa_Empresas_EmpresasEmpresaId",
                        column: x => x.EmpresasEmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteEmpresa_EmpresasEmpresaId",
                table: "ClienteEmpresa",
                column: "EmpresasEmpresaId");
        }
    }
}
