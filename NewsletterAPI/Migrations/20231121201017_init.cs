using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsletterAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsletterStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personnel",
                table: "Personnel");

            migrationBuilder.RenameTable(
                name: "Personnel",
                newName: "Personnels");

            migrationBuilder.RenameColumn(
                name: "NewsletterID",
                table: "Newsletter",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonnelID",
                table: "Personnels",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Newsletter",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Newsletter",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personnels",
                table: "Personnels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SendNewsletterLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(type: "int", nullable: true),
                    NewsletterId = table.Column<int>(type: "int", nullable: true),
                    NewsTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendNewsletterLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personnels",
                table: "Personnels");

            migrationBuilder.RenameTable(
                name: "Personnels",
                newName: "Personnel");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Newsletter",
                newName: "NewsletterID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Personnel",
                newName: "PersonnelID");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Newsletter",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Newsletter",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Personnel",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Personnel",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personnel",
                table: "Personnel",
                column: "PersonnelID");

            migrationBuilder.CreateTable(
                name: "NewsletterStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsletterID = table.Column<int>(type: "int", nullable: true),
                    PersonnelID = table.Column<int>(type: "int", nullable: true),
                    ReceiveTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    SendStatus = table.Column<int>(type: "int", nullable: true),
                    SendTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ViewStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Newslett__C8EE2043E96496D3", x => x.StatusID);
                    table.ForeignKey(
                        name: "FK__Newslette__Newsl__38996AB5",
                        column: x => x.NewsletterID,
                        principalTable: "Newsletter",
                        principalColumn: "NewsletterID");
                    table.ForeignKey(
                        name: "FK__Newslette__Perso__37A5467C",
                        column: x => x.PersonnelID,
                        principalTable: "Personnel",
                        principalColumn: "PersonnelID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterStatus_NewsletterID",
                table: "NewsletterStatus",
                column: "NewsletterID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterStatus_PersonnelID",
                table: "NewsletterStatus",
                column: "PersonnelID");
        }
    }
}
