using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDeNotaFiscal.Migrations
{
    /// <inheritdoc />
    public partial class atualizarDadosTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscals_Clientes_ClienteId",
                table: "NotaFiscals");

            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscals_Empresas_EmpresaId",
                table: "NotaFiscals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotaFiscals",
                table: "NotaFiscals");

            migrationBuilder.RenameTable(
                name: "NotaFiscals",
                newName: "NotasFiscais");

            migrationBuilder.RenameIndex(
                name: "IX_NotaFiscals_EmpresaId",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_NotaFiscals_ClienteId",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotasFiscais",
                table: "NotasFiscais",
                column: "NotaFiscalId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteId",
                table: "NotasFiscais",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaId",
                table: "NotasFiscais",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteId",
                table: "NotasFiscais");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaId",
                table: "NotasFiscais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotasFiscais",
                table: "NotasFiscais");

            migrationBuilder.RenameTable(
                name: "NotasFiscais",
                newName: "NotaFiscals");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_EmpresaId",
                table: "NotaFiscals",
                newName: "IX_NotaFiscals_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_ClienteId",
                table: "NotaFiscals",
                newName: "IX_NotaFiscals_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotaFiscals",
                table: "NotaFiscals",
                column: "NotaFiscalId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscals_Clientes_ClienteId",
                table: "NotaFiscals",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscals_Empresas_EmpresaId",
                table: "NotaFiscals",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
