using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SitPlanner.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventOption_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arrangement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventOptionId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrangement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arrangement_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arrangement_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CapacityOfPeople = table.Column<int>(nullable: false),
                    TableType = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: true),
                    AccessibilityRestrictionId = table.Column<int>(nullable: true),
                    AccessibilityRestrictionId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InviteeTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InviteeId = table.Column<int>(nullable: true),
                    TableId = table.Column<int>(nullable: true),
                    EventOptionId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                    ArrangementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InviteeTable_Arrangement_ArrangementId",
                        column: x => x.ArrangementId,
                        principalTable: "Arrangement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InviteeTable_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InviteeTable_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InviteeTable_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InviteeCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InviteeId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    EventOptionId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteeCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InviteeCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InviteeCategory_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InviteeCategory_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessibilityRestriction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessibilityRestrictionName = table.Column<string>(nullable: true),
                    InviteeId = table.Column<int>(nullable: true),
                    EventOptionId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibilityRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessibilityRestriction_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessibilityRestriction_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    IsComing = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitee_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalRestriction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InviteeId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                    EventOptionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRestriction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalRestriction_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalRestriction_EventOption_EventOptionId",
                        column: x => x.EventOptionId,
                        principalTable: "EventOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalRestriction_Invitee_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "Invitee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Arrangement_EventId",
                table: "Arrangement",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrangement_EventOptionId",
                table: "Arrangement",
                column: "EventOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_EventId",
                table: "Category",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserId",
                table: "Event",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventOption_EventId",
                table: "EventOption",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitee_EventId",
                table: "Invitee",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitee_PersonalRestrictionId",
                table: "Invitee",
                column: "PersonalRestrictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitee_PersonalRestrictionId1",
                table: "Invitee",
                column: "PersonalRestrictionId1");

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

            migrationBuilder.CreateIndex(
                name: "IX_InviteeTable_ArrangementId",
                table: "InviteeTable",
                column: "ArrangementId");

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
                name: "IX_PersonalRestriction_InviteeId",
                table: "PersonalRestriction",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_AccessibilityRestrictionId",
                table: "Table",
                column: "AccessibilityRestrictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_AccessibilityRestrictionId1",
                table: "Table",
                column: "AccessibilityRestrictionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Table_EventId",
                table: "Table",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_AccessibilityRestriction_AccessibilityRestrictionId",
                table: "Table",
                column: "AccessibilityRestrictionId",
                principalTable: "AccessibilityRestriction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_AccessibilityRestriction_AccessibilityRestrictionId1",
                table: "Table",
                column: "AccessibilityRestrictionId1",
                principalTable: "AccessibilityRestriction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InviteeTable_Invitee_InviteeId",
                table: "InviteeTable",
                column: "InviteeId",
                principalTable: "Invitee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InviteeCategory_Invitee_InviteeId",
                table: "InviteeCategory",
                column: "InviteeId",
                principalTable: "Invitee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityRestriction_Invitee_InviteeId",
                table: "AccessibilityRestriction",
                column: "InviteeId",
                principalTable: "Invitee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Invitee_PersonalRestriction_PersonalRestrictionId",
            //    table: "Invitee",
            //    column: "PersonalRestrictionId",
            //    principalTable: "PersonalRestriction",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Invitee_PersonalRestriction_PersonalRestrictionId1",
            //    table: "Invitee",
            //    column: "PersonalRestrictionId1",
            //    principalTable: "PersonalRestriction",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventOption_Event_EventId",
                table: "EventOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitee_Event_EventId",
                table: "Invitee");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRestriction_Event_EventId",
                table: "PersonalRestriction");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRestriction_EventOption_EventOptionId",
                table: "PersonalRestriction");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRestriction_Invitee_InviteeId",
                table: "PersonalRestriction");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InviteeCategory");

            migrationBuilder.DropTable(
                name: "InviteeTable");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Arrangement");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "AccessibilityRestriction");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "EventOption");

            migrationBuilder.DropTable(
                name: "Invitee");

            migrationBuilder.DropTable(
                name: "PersonalRestriction");
        }
    }
}
