using Microsoft.EntityFrameworkCore.Migrations;

namespace MailService.Migrations
{
    public partial class Mailservice3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State_Id",
                table: "Mails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Flowchart",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowchart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Flowchart_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Flowchart_Flowchart_Id",
                        column: x => x.Flowchart_Id,
                        principalTable: "Flowchart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    State_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reader_States_State_Id",
                        column: x => x.State_Id,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mails_State_Id",
                table: "Mails",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_State_Id",
                table: "Reader",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_States_Flowchart_Id",
                table: "States",
                column: "Flowchart_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_States_State_Id",
                table: "Mails",
                column: "State_Id",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mails_States_State_Id",
                table: "Mails");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Flowchart");

            migrationBuilder.DropIndex(
                name: "IX_Mails_State_Id",
                table: "Mails");

            migrationBuilder.DropColumn(
                name: "State_Id",
                table: "Mails");
        }
    }
}
