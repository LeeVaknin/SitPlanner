using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class addedTableTypeToAccessabilityRestriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableType",
                table: "AccessibilityRestriction",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableType",
                table: "AccessibilityRestriction");
        }
    }
}
