using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class CrawlNewsTab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrawlNews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddTime = table.Column<string>(maxLength: 50, nullable: false),
                    Content = table.Column<string>(maxLength: 512, nullable: false),
                    From = table.Column<string>(maxLength: 50, nullable: false),
                    FromUrl = table.Column<string>(maxLength: 512, nullable: false),
                    ImportantLevel = table.Column<int>(nullable: false),
                    PushLevel = table.Column<int>(nullable: false),
                    PushTime = table.Column<string>(maxLength: 50, nullable: false),
                    Tag = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlNews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrawlNews");
        }
    }
}
