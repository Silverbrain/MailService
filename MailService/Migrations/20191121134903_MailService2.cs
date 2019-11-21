using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailService.Migrations
{
    public partial class MailService2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mails_AspNetUsers_Reciever_id",
                table: "Mails");

            migrationBuilder.DropForeignKey(
                name: "FK_Mails_AspNetUsers_Sender_id",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_Reciever_id",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_Sender_id",
                table: "Mails");

            migrationBuilder.DropColumn(
                name: "Reciever_id",
                table: "Mails");

            migrationBuilder.DropColumn(
                name: "Sender_id",
                table: "Mails");

            migrationBuilder.AddColumn<bool>(
                name: "ReadStatus",
                table: "Mails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RecievedMail",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mail_id = table.Column<int>(nullable: false),
                    Reciever_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecievedMail", x => x.id);
                    table.ForeignKey(
                        name: "FK_RecievedMail_Mails_Mail_id",
                        column: x => x.Mail_id,
                        principalTable: "Mails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecievedMail_AspNetUsers_Reciever_id",
                        column: x => x.Reciever_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SentMail",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mail_id = table.Column<int>(nullable: false),
                    Sender_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentMail", x => x.id);
                    table.ForeignKey(
                        name: "FK_SentMail_Mails_Mail_id",
                        column: x => x.Mail_id,
                        principalTable: "Mails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SentMail_AspNetUsers_Sender_id",
                        column: x => x.Sender_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecievedMail_Mail_id",
                table: "RecievedMail",
                column: "Mail_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecievedMail_Reciever_id",
                table: "RecievedMail",
                column: "Reciever_id");

            migrationBuilder.CreateIndex(
                name: "IX_SentMail_Mail_id",
                table: "SentMail",
                column: "Mail_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentMail_Sender_id",
                table: "SentMail",
                column: "Sender_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecievedMail");

            migrationBuilder.DropTable(
                name: "SentMail");

            migrationBuilder.DropColumn(
                name: "ReadStatus",
                table: "Mails");

            migrationBuilder.AddColumn<string>(
                name: "Reciever_id",
                table: "Mails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sender_id",
                table: "Mails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mails_Reciever_id",
                table: "Mails",
                column: "Reciever_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_Sender_id",
                table: "Mails",
                column: "Sender_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_AspNetUsers_Reciever_id",
                table: "Mails",
                column: "Reciever_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_AspNetUsers_Sender_id",
                table: "Mails",
                column: "Sender_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
