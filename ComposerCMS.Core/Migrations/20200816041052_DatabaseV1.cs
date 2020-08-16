using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ComposerCMS.Core.Migrations
{
    public partial class DatabaseV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityHistory",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Entity = table.Column<string>(maxLength: 128, nullable: true),
                    Action = table.Column<string>(maxLength: 16, nullable: true),
                    Data = table.Column<string>(maxLength: 1024, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityHistory", x => x.ID);
                });

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
                name: "Blog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExternalResource",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Href = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalResource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Layout",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layout", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LayoutScript",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LayoutID = table.Column<Guid>(nullable: false),
                    ExternalResourceID = table.Column<Guid>(nullable: false),
                    LoadOrder = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutScript", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MenuID = table.Column<Guid>(nullable: false),
                    DisplayText = table.Column<string>(maxLength: 128, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    RouteID = table.Column<Guid>(maxLength: 256, nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LayoutID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    IsSystemPage = table.Column<bool>(nullable: false),
                    IsAbstract = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    SourceVersionID = table.Column<Guid>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    DateLastPublished = table.Column<DateTime>(nullable: true),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PageScript",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PageID = table.Column<Guid>(nullable: false),
                    ExternalResourceID = table.Column<Guid>(nullable: false),
                    LoadOrder = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageScript", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PageVersion",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PageID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageVersion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BlogID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    IsPinned = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EntityID = table.Column<string>(nullable: true),
                    OriginalEntityText = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    UserIDAdded = table.Column<Guid>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    DefaultRouteID = table.Column<Guid>(nullable: false),
                    MinimizeJs = table.Column<bool>(nullable: false),
                    MinimizeCss = table.Column<bool>(nullable: false),
                    UserIDLastUpdated = table.Column<Guid>(nullable: false),
                    ThemeKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
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
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "993ab932-df4d-47ba-902f-2ec313dc4e73", "1379d2eb-ed59-4bb5-b868-88a67d0cb9a2", "Admin", "ADMIN" },
                    { "4d323c3f-d805-4932-bb1e-02cc2d0f58b5", "0f993fae-cc79-4150-8f03-bd6a88706093", "Editor", "EDITOR" },
                    { "de6f9df8-d8ef-4dc4-b0b0-fc2dc2c51aed", "8a9f8cbd-9b6e-4154-b74d-40fd6d15cc66", "Author", "AUTHOR" },
                    { "a7f52a41-4c4c-45e0-9088-a89cb25dea92", "1c3f7f7a-7290-4569-88ae-566494c4ce43", "Contributor", "CONTRIBUTOR" },
                    { "e3c7b0e0-88f7-4bd0-b846-66c63db1f614", "954e96f5-460b-4f1c-9c59-cfc60dc2ba41", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "de0fa044-1d5b-44d7-a93e-66598b2b7c84", 0, "94932462-7815-4bae-804d-6bb4c9ea6a19", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEAx4YHIq0OhemLJMaqOlkSzjs2UA14L7pVtYCRlvPVB4bK9X8EYwObAcpwFUN4Iptg==", null, false, "047a57f7-c5a8-4649-aa2d-d82159529487", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "de0fa044-1d5b-44d7-a93e-66598b2b7c84", "993ab932-df4d-47ba-902f-2ec313dc4e73" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_Action",
                table: "ActivityHistory",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_DateAdded",
                table: "ActivityHistory",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_Entity",
                table: "ActivityHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_UserIDAdded",
                table: "ActivityHistory",
                column: "UserIDAdded");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Name",
                table: "Blog",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuID",
                table: "MenuItem",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_RouteID",
                table: "MenuItem",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Page_DateAdded",
                table: "Page",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_Page_Name",
                table: "Page",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Page_Path",
                table: "Page",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_PageVersion_DateAdded",
                table: "PageVersion",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_PageVersion_Name",
                table: "PageVersion",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PageVersion_PageID",
                table: "PageVersion",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_PageVersion_Path",
                table: "PageVersion",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_Post_BlogID",
                table: "Post",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IsPinned",
                table: "Post",
                column: "IsPinned");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IsPublic",
                table: "Post",
                column: "IsPublic");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IsPublished",
                table: "Post",
                column: "IsPublished");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Title",
                table: "Post",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityHistory");

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
                name: "Blog");

            migrationBuilder.DropTable(
                name: "ExternalResource");

            migrationBuilder.DropTable(
                name: "Layout");

            migrationBuilder.DropTable(
                name: "LayoutScript");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "PageScript");

            migrationBuilder.DropTable(
                name: "PageVersion");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
