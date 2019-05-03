using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class RemoveInviteeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InviteeCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Invitee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invitee_CategoryId",
                table: "Invitee",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitee_Category_CategoryId",
                table: "Invitee",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitee_Category_CategoryId",
                table: "Invitee");

            migrationBuilder.DropIndex(
                name: "IX_Invitee_CategoryId",
                table: "Invitee");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Invitee");

            migrationBuilder.CreateTable(
                name: "InviteeCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventOptionId = table.Column<int>(nullable: false),
                    InviteeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteeCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InviteeCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                    table.ForeignKey(
                        name: "FK_InviteeCategory_Invitee_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "Invitee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
    }
}
