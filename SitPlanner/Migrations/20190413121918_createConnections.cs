using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class createConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessibilityRestriction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InviteeId = table.Column<int>(nullable: false),
                    TableId = table.Column<int>(nullable: false),
                    IsSittingAtTable = table.Column<bool>(nullable: false),
                    EventOptionId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibilityRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessibilityRestriction_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AccessibilityRestriction_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AccessibilityRestriction_Invitee_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "Invitee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AccessibilityRestriction_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "InviteeTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InviteeId = table.Column<int>(nullable: false),
                    TableId = table.Column<int>(nullable: false),
                    EventOptionId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InviteeTable_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InviteeTable_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InviteeTable_Invitee_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "Invitee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InviteeTable_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonalRestriction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MainInviteeId = table.Column<int>(nullable: false),
                    SecondaryInviteeId = table.Column<int>(nullable: false),
                    IsSittingTogether = table.Column<bool>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventOptionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalRestriction_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonalRestriction_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonalRestriction_Invitee_MainInviteeId",
                        column: x => x.MainInviteeId,
                        principalTable: "Invitee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonalRestriction_Invitee_SecondaryInviteeId",
                        column: x => x.SecondaryInviteeId,
                        principalTable: "Invitee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityRestriction_EventId",
                table: "AccessibilityRestriction",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityRestriction_EventOptionId",
                table: "AccessibilityRestriction",
                column: "EventOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityRestriction_InviteeId",
                table: "AccessibilityRestriction",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityRestriction_TableId",
                table: "AccessibilityRestriction",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeTable_EventId",
                table: "InviteeTable",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeTable_EventOptionId",
                table: "InviteeTable",
                column: "EventOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeTable_InviteeId",
                table: "InviteeTable",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeTable_TableId",
                table: "InviteeTable",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRestriction_EventId",
                table: "PersonalRestriction",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRestriction_EventOptionId",
                table: "PersonalRestriction",
                column: "EventOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRestriction_MainInviteeId",
                table: "PersonalRestriction",
                column: "MainInviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRestriction_SecondaryInviteeId",
                table: "PersonalRestriction",
                column: "SecondaryInviteeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessibilityRestriction");

            migrationBuilder.DropTable(
                name: "InviteeTable");

            migrationBuilder.DropTable(
                name: "PersonalRestriction");
        }
    }
}
