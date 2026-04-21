using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG7311_POE_P2.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceLevel",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceLevel",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contracts");
        }
    }
}
