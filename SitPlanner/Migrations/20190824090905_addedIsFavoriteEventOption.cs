using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class addedIsFavoriteEventOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AccessibilityRestriction_Table_TableId",
            //    table: "AccessibilityRestriction");

            //migrationBuilder.DropIndex(
            //    name: "IX_AccessibilityRestriction_TableId",
            //    table: "AccessibilityRestriction");

            //migrationBuilder.DropColumn(
            //    name: "TableId",
            //    table: "AccessibilityRestriction");

            migrationBuilder.AddColumn<bool>(
                name: "isFavorite",
                table: "EventOption",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFavorite",
                table: "EventOption");

            //migrationBuilder.AddColumn<int>(
            //    name: "TableId",
            //    table: "AccessibilityRestriction",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AccessibilityRestriction_TableId",
            //    table: "AccessibilityRestriction",
            //    column: "TableId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AccessibilityRestriction_Table_TableId",
            //    table: "AccessibilityRestriction",
            //    column: "TableId",
            //    principalTable: "Table",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
