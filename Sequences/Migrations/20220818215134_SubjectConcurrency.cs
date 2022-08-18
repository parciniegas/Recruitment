using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sequences.Migrations
{
    public partial class SubjectConcurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Subjects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Subjects");
        }
    }
}
