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

            migrationBuilder.Sql(@"
                INSERT INTO TechSolutions.CRM.Customer ([name], surname, email, phonenumber, IdentityNumber) VALUES ('Francois', 'Venter', 'venter109@gmail.com', '0766732450', '9501315106086');
                INSERT INTO TechSolutions.CRM.Customer ([name], surname, email, phonenumber, IdentityNumber) VALUES ('Piet', 'Vermaas', 'pietv@test.co', '0712315849', '9501315106087');
                INSERT INTO TechSolutions.CRM.Address (AddressName, AddressType, AddressLine1, City, PostalCode, Province, CustomerId) VALUES ('Home', 'Private', '731 Sheba Street', 'Pretoria', '0081', 'Gauteng', 1);
            ");
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
