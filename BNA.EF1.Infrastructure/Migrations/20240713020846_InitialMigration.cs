using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNA.EF1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cuil = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExampleClass",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExampleField = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InternalInfo = table.Column<string>(type: "nchar(8)", fixedLength: true, maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroCuenta = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    CodigoSucursal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Saldo = table.Column<double>(type: "float", nullable: false),
                    TipoCuenta = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_ClienteId",
                table: "Cuentas",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "ExampleClass");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
