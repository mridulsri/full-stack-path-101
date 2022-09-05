using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Microservices.AuthServer.Persistence.Migrations
{
    public partial class user_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnoxTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnoxTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DOB", "Email", "Gender", "IsActive", "IsDeleted", "Name", "Password", "Phone", "Role", "UpdatedAt", "UserId", "Username" },
                values: new object[] { 1, new DateTime(2022, 9, 5, 13, 49, 5, 108, DateTimeKind.Utc).AddTicks(7131), new DateTime(2022, 9, 5, 13, 49, 5, 123, DateTimeKind.Utc).AddTicks(6958), "demo@demo.me", "Male", true, false, "demo", "10000.mpZUdQ1MrC9eSGYscDShDw==.mOTxr1anNM1XWtF/P8eEkVA+GcH8FkbaqEaZ48ecsxw=", "9350272167", "Standard", null, new Guid("6dcd3e4d-25c2-4446-9e6c-56a5846a3427"), "demo@demo.me" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnoxTokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
