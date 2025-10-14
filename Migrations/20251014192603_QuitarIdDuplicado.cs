using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacturacionElectronicaSV.Migrations
{
    /// <inheritdoc />
    public partial class QuitarIdDuplicado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Receptores");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDocumento",
                table: "Receptores",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoDocumento",
                table: "Receptores",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Receptores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
