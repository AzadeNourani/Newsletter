using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsletterAPI.Migrations
{
    public partial class addnationalcodetopersonnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength:20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "SendNewslog",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    NewsID = table.Column<int>(type: "int", nullable: true),
                    SendStatus = table.Column<int>(type: "int", nullable: true),
                    SendTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceiveTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ViewStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Newslett__C8E", x => x.StatusID);
                    table.ForeignKey(
                        name: "FK__Newslette__Newsl__38",
                        column: x => x.NewsID,
                        principalTable: "Newsletter",
                        principalColumn: "NewsletterID");
                    table.ForeignKey(
                        name: "FK__Newslette__Perso__37AC",
                        column: x => x.EmployeeID,
                        principalTable: "Personnel",
                        principalColumn: "PersonnelID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterStatus_NewsletterID",
                table: "NewsletterStatus",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterStatus_PersonnelID",
                table: "NewsletterStatus",
                column: "PersonnelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsletterStatus");

            migrationBuilder.DropTable(
                name: "Newsletter");

            migrationBuilder.DropTable(
                name: "Personnel");
        }
    }
}
