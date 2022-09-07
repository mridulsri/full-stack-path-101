using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Microservices.AuthServer.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnoxTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnoxTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DOB", "Email", "Gender", "IsActive", "IsDeleted", "Name", "Password", "Phone", "Role", "UpdatedAt", "UserId", "Username" },
                values: new object[] { 1, new DateTime(2022, 9, 7, 12, 0, 9, 697, DateTimeKind.Utc).AddTicks(449), new DateTime(2022, 9, 7, 12, 0, 9, 711, DateTimeKind.Utc).AddTicks(5390), "demo@demo.me", "Male", true, false, "demo", "10000.H6CDbmfgRncCz7qoMr55+g==.17yFVTXOBV2dPCMQiN/jiIqZ0Y32VMEGQ643fZKLRiU=", "9350272167", "Standard", null, new Guid("f9ff6086-dc4a-4a00-aa20-1b677b73e62d"), "demo@demo.me" });
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
