using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class AddSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Subject",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Subject", x => x.Id);
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
