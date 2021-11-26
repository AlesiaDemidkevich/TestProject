using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class AddTestAndQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuestion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "Questions",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   IdQuestionCoefficient = table.Column<int>(type: "int", nullable: false),
                   ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   IdSubject = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Questions", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
