using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CSharpPlayground.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_ChatSessions_SessionId",
                schema: "QuestioningModule",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_SolverId",
                schema: "QuestioningModule",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "SolverId",
                schema: "QuestioningModule",
                table: "Questions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "SessionId",
                schema: "QuestioningModule",
                table: "Questions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "Metadata",
                schema: "QuestioningModule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_ChatSessions_SessionId",
                schema: "QuestioningModule",
                table: "Questions",
                column: "SessionId",
                principalSchema: "QuestioningModule",
                principalTable: "ChatSessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_SolverId",
                schema: "QuestioningModule",
                table: "Questions",
                column: "SolverId",
                principalSchema: "QuestioningModule",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_ChatSessions_SessionId",
                schema: "QuestioningModule",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_SolverId",
                schema: "QuestioningModule",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Metadata",
                schema: "QuestioningModule");

            migrationBuilder.AlterColumn<int>(
                name: "SolverId",
                schema: "QuestioningModule",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SessionId",
                schema: "QuestioningModule",
                table: "Questions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_ChatSessions_SessionId",
                schema: "QuestioningModule",
                table: "Questions",
                column: "SessionId",
                principalSchema: "QuestioningModule",
                principalTable: "ChatSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_SolverId",
                schema: "QuestioningModule",
                table: "Questions",
                column: "SolverId",
                principalSchema: "QuestioningModule",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
