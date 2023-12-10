using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSolutionsCRM.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                schema: "CRM",
                table: "Customer",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "CRM",
                table: "Address",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                schema: "CRM",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Active",
                schema: "CRM",
                table: "Address");
        }
    }
}
