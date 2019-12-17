using Microsoft.EntityFrameworkCore.Migrations;

namespace MailService.Migrations
{
    public partial class MailService4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BodySummary",
                table: "Mails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodySummary",
                table: "Mails");
        }
    }
}
