using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class InviteeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InviteeCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InviteeId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    EventOptionId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteeCategory", x => new { x.CategoryId, x.InviteeId });
                    //table.ForeignKey(
                    //    name: "FK_InviteeCategory_Category_CategoryId",
                    //    column: x => x.CategoryId,
                    //    principalTable: "Category",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InviteeCategory_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InviteeCategory_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    //table.ForeignKey(
                    //    name: "FK_InviteeCategory_Invitee_InviteeId",
                    //    column: x => x.InviteeId,
                    //    principalTable: "Invitee",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InviteeCategory_CategoryId",
                table: "InviteeCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeCategory_EventId",
                table: "InviteeCategory",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeCategory_EventOptionId",
                table: "InviteeCategory",
                column: "EventOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeCategory_InviteeId",
                table: "InviteeCategory",
                column: "InviteeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InviteeCategory");
        }
    }
}
