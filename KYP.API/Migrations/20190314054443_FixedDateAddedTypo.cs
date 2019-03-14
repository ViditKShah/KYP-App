using Microsoft.EntityFrameworkCore.Migrations;

namespace KYP.API.Migrations
{
    public partial class FixedDateAddedTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAddded",
                table: "Photos",
                newName: "DateAdded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Photos",
                newName: "DateAddded");
        }
    }
}
