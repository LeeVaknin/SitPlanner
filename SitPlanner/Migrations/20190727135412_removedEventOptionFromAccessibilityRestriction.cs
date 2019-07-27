using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class removedEventOptionFromAccessibilityRestriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityRestriction_EventOption_EventOptionId",
                table: "AccessibilityRestriction");

            migrationBuilder.AlterColumn<int>(
                name: "EventOptionId",
                table: "AccessibilityRestriction",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityRestriction_EventOption_EventOptionId",
                table: "AccessibilityRestriction",
                column: "EventOptionId",
                principalTable: "EventOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityRestriction_EventOption_EventOptionId",
                table: "AccessibilityRestriction");

            migrationBuilder.AlterColumn<int>(
                name: "EventOptionId",
                table: "AccessibilityRestriction",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityRestriction_EventOption_EventOptionId",
                table: "AccessibilityRestriction",
                column: "EventOptionId",
                principalTable: "EventOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
