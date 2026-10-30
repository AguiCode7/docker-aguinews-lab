using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    summary_id = table.Column<Guid>(type: "uuid", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    source_name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    topic = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    headline = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    lead = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    reading = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    key_points = table.Column<List<string>>(type: "text[]", nullable: false),
                    reading_minutes = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                    table.ForeignKey(
                        name: "FK_articles_summaries_summary_id",
                        column: x => x.summary_id,
                        principalTable: "summaries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articles_summary_id_position",
                table: "articles",
                columns: new[] { "summary_id", "position" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
