using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpPlayground.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ChatSessions_ChatSessionId",
                schema: "QuestioningModule",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatSessionId",
                schema: "QuestioningModule",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatSessionId",
                schema: "QuestioningModule",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ChatSessionUser",
                schema: "QuestioningModule",
                columns: table => new
                {
                    ChatSessionsId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipantsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSessionUser", x => new { x.ChatSessionsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ChatSessionUser_ChatSessions_ChatSessionsId",
                        column: x => x.ChatSessionsId,
                        principalSchema: "QuestioningModule",
                        principalTable: "ChatSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatSessionUser_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalSchema: "QuestioningModule",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessionUser_ParticipantsId",
                schema: "QuestioningModule",
                table: "ChatSessionUser",
                column: "ParticipantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatSessionUser",
                schema: "QuestioningModule");

            migrationBuilder.AddColumn<long>(
                name: "ChatSessionId",
                schema: "QuestioningModule",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatSessionId",
                schema: "QuestioningModule",
                table: "Users",
                column: "ChatSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ChatSessions_ChatSessionId",
                schema: "QuestioningModule",
                table: "Users",
                column: "ChatSessionId",
                principalSchema: "QuestioningModule",
                principalTable: "ChatSessions",
                principalColumn: "Id");
        }
    }
}
