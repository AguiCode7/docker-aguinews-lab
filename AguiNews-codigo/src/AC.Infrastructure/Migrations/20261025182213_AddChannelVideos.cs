using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "channel_videos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    youtube_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    kind = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    news_url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    published_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channel_videos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_channel_videos_published_at",
                table: "channel_videos",
                column: "published_at");

            migrationBuilder.CreateIndex(
                name: "IX_channel_videos_youtube_id",
                table: "channel_videos",
                column: "youtube_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channel_videos");
        }
    }
}
