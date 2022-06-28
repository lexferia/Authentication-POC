using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.POC.API.JWT.Migrations
{
    public partial class InitialPOCUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("34c96152-329d-4463-81c2-481ce4e79e85"), new DateTime(2022, 6, 22, 7, 28, 14, 706, DateTimeKind.Utc).AddTicks(5440), "Administrator", null });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("6721a7a3-1855-40d0-b5a2-23205acf36f2"), new DateTime(2022, 6, 22, 7, 28, 14, 706, DateTimeKind.Utc).AddTicks(5444), "Editor", null });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("dd5ac686-f64a-4dd4-bd19-9427d7c34eea"), new DateTime(2022, 6, 22, 7, 28, 14, 706, DateTimeKind.Utc).AddTicks(5445), "Writer", null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Enabled", "Name", "Password", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("061b4cfa-e1ba-4983-b395-d93e651e5559"), new DateTime(2022, 6, 22, 7, 28, 14, 706, DateTimeKind.Utc).AddTicks(5602), "dev@philippines.com", true, "Lester Feria", "123456", null, null, new Guid("34c96152-329d-4463-81c2-481ce4e79e85"), null });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
