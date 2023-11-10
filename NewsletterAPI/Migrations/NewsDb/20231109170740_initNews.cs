using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsletterAPI.Migrations.NewsDb
{
    public partial class initNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendNewsletterLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(type: "int", nullable: true),
                    NewsletterId = table.Column<int>(type: "int", nullable: true),
                    SendStatus = table.Column<int>(type: "int", nullable: true),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ViewStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendNewsletterLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendNewsletterLogs_Newsletter_NewsletterId",
                        column: x => x.NewsletterId,
                        principalTable: "Newsletter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SendNewsletterLogs_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SendNewsletterLogs_NewsletterId",
                table: "SendNewsletterLogs",
                column: "NewsletterId");

            migrationBuilder.CreateIndex(
                name: "IX_SendNewsletterLogs_PersonnelId",
                table: "SendNewsletterLogs",
                column: "PersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendNewsletterLogs");

            migrationBuilder.DropTable(
                name: "Newsletter");

            migrationBuilder.DropTable(
                name: "Personnels");
        }
    }
}
