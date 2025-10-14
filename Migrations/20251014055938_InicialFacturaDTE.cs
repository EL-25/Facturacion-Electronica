using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacturacionElectronicaSV.Migrations
{
    /// <inheritdoc />
    public partial class InicialFacturaDTE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emisor",
                columns: table => new
                {
                    IdEmisor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NIT = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    NRC = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CodActividad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DescActividad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoEstablecimiento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emisor", x => x.IdEmisor);
                });

            migrationBuilder.CreateTable(
                name: "Receptores",
                columns: table => new
                {
                    IdReceptor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CodActividad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DescActividad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptores", x => x.IdReceptor);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NIT = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    NRC = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CodActividad = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DescActividad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoEstablecimiento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    IdDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDTE = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CodigoGeneracion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroControl = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FormaPago = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalGravada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPagar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLetras = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EliminadoDTE = table.Column<bool>(type: "bit", nullable: false),
                    IdEmisor = table.Column<int>(type: "int", nullable: false),
                    IdReceptor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.IdDocumento);
                    table.ForeignKey(
                        name: "FK_Documentos_Emisor_IdEmisor",
                        column: x => x.IdEmisor,
                        principalTable: "Emisor",
                        principalColumn: "IdEmisor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentos_Receptores_IdReceptor",
                        column: x => x.IdReceptor,
                        principalTable: "Receptores",
                        principalColumn: "IdReceptor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesDocumento",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoItem = table.Column<int>(type: "int", nullable: false),
                    UnidadMedida = table.Column<int>(type: "int", nullable: false),
                    IdDocumento = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesDocumento", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_DetallesDocumento_Documentos_IdDocumento",
                        column: x => x.IdDocumento,
                        principalTable: "Documentos",
                        principalColumn: "IdDocumento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resumenes",
                columns: table => new
                {
                    IdResumen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalGravado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalExento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalNoSujeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPagar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdDocumento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumenes", x => x.IdResumen);
                    table.ForeignKey(
                        name: "FK_Resumenes_Documentos_IdDocumento",
                        column: x => x.IdDocumento,
                        principalTable: "Documentos",
                        principalColumn: "IdDocumento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDocumento_IdDocumento",
                table: "DetallesDocumento",
                column: "IdDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_IdEmisor",
                table: "Documentos",
                column: "IdEmisor");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_IdReceptor",
                table: "Documentos",
                column: "IdReceptor");

            migrationBuilder.CreateIndex(
                name: "IX_Resumenes_IdDocumento",
                table: "Resumenes",
                column: "IdDocumento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesDocumento");

            migrationBuilder.DropTable(
                name: "Resumenes");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Emisor");

            migrationBuilder.DropTable(
                name: "Receptores");
        }
    }
}
