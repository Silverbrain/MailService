using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailService.Migrations
{
    public partial class MailService3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReadStatus",
                table: "Mails",
                newName: "IsRead");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadDate",
                table: "Mails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "Mails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadDate",
                table: "Mails");

            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "Mails");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Mails",
                newName: "ReadStatus");
        }
    }
}
