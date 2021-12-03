using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class VariantTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.RenameColumn(
                name: "Variant",
                table: "Tests",
                newName: "IdVariant");

            migrationBuilder.CreateTable(
              name: "Variant",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Variant", x => x.Id);
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
