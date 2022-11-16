using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiPlayground.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatSessions_ChatSessionId",
                schema: "QuestioningModule",
                table: "Messages");

            migrationBuilder.AlterColumn<long>(
                name: "ChatSessionId",
                schema: "QuestioningModule",
                table: "Messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatSessions_ChatSessionId",
                schema: "QuestioningModule",
                table: "Messages",
                column: "ChatSessionId",
                principalSchema: "QuestioningModule",
                principalTable: "ChatSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatSessions_ChatSessionId",
                schema: "QuestioningModule",
                table: "Messages");

            migrationBuilder.AlterColumn<long>(
                name: "ChatSessionId",
                schema: "QuestioningModule",
                table: "Messages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatSessions_ChatSessionId",
                schema: "QuestioningModule",
                table: "Messages",
                column: "ChatSessionId",
                principalSchema: "QuestioningModule",
                principalTable: "ChatSessions",
                principalColumn: "Id");
        }
    }
}
