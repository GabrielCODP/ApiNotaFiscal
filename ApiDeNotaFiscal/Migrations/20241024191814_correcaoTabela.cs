﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDeNotaFiscal.Migrations
{
    /// <inheritdoc />
    public partial class correcaoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Empresas_EmpresaId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EmpresaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Clientes");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteEmpresa");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaId",
                table: "Clientes",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Empresas_EmpresaId",
                table: "Clientes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId");
        }
    }
}
