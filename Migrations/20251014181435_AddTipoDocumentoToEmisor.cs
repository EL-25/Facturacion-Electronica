using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacturacionElectronicaSV.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoDocumentoToEmisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoDocumento",
                table: "Emisor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoDocumento",
                table: "Emisor");
        }
    }
}
