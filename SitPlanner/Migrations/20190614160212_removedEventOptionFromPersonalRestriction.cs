using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class removedEventOptionFromPersonalRestriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRestriction_EventOption_EventOptionId",
                table: "PersonalRestriction");

            migrationBuilder.AlterColumn<int>(
                name: "EventOptionId",
                table: "PersonalRestriction",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalRestriction_EventOption_EventOptionId",
                table: "PersonalRestriction",
                column: "EventOptionId",
                principalTable: "EventOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRestriction_EventOption_EventOptionId",
                table: "PersonalRestriction");

            migrationBuilder.AlterColumn<int>(
                name: "EventOptionId",
                table: "PersonalRestriction",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalRestriction_EventOption_EventOptionId",
                table: "PersonalRestriction",
                column: "EventOptionId",
                principalTable: "EventOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
