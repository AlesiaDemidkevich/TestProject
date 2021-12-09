using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class idtb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTest",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdTest",
                table: "Results");
        }
    }
}
