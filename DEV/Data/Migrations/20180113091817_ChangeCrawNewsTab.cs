using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class ChangeCrawNewsTab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "CrawlNews",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FromUrl",
                table: "CrawlNews",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CrawlNews",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "CrawlNews",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromUrl",
                table: "CrawlNews",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CrawlNews",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);
        }
    }
}
