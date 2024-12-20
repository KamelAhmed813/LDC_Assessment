using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userQueries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userQueries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chatBotResponses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    queryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatBotResponses", x => x.id);
                    table.ForeignKey(
                        name: "FK_chatBotResponses_userQueries_queryID",
                        column: x => x.queryID,
                        principalTable: "userQueries",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chatBotResponses_queryID",
                table: "chatBotResponses",
                column: "queryID",
                unique: true,
                filter: "[queryID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chatBotResponses");

            migrationBuilder.DropTable(
                name: "userQueries");
        }
    }
}
