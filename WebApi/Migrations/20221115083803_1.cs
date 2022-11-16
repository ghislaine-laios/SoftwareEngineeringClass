using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiPlayground.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "QuestioningModule");

            migrationBuilder.CreateTable(
                name: "ChatSessions",
                schema: "QuestioningModule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSessions", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "QuestioningModule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSessionUser",
                schema: "QuestioningModule",
                columns: table => new
                {
                    ChatSessionsId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipantsId = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "QuestioningModule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatSessionId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdPerChat = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_ChatSessions_ChatSessionId",
                        column: x => x.ChatSessionId,
                        principalSchema: "QuestioningModule",
                        principalTable: "ChatSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "QuestioningModule",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "QuestioningModule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    SolverId = table.Column<long>(type: "bigint", nullable: true),
                    SessionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_ChatSessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "QuestioningModule",
                        principalTable: "ChatSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "QuestioningModule",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Users_SolverId",
                        column: x => x.SolverId,
                        principalSchema: "QuestioningModule",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessionUser_ParticipantsId",
                schema: "QuestioningModule",
                table: "ChatSessionUser",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatSessionId",
                schema: "QuestioningModule",
                table: "Messages",
                column: "ChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                schema: "QuestioningModule",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SenderId",
                schema: "QuestioningModule",
                table: "Questions",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SessionId",
                schema: "QuestioningModule",
                table: "Questions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SolverId",
                schema: "QuestioningModule",
                table: "Questions",
                column: "SolverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatSessionUser",
                schema: "QuestioningModule");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "QuestioningModule");

            migrationBuilder.DropTable(
                name: "Metadata",
                schema: "QuestioningModule");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "QuestioningModule");

            migrationBuilder.DropTable(
                name: "ChatSessions",
                schema: "QuestioningModule");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "QuestioningModule");
        }
    }
}
