using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdTest",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "IdUserAnswer",
                table: "Results");

            migrationBuilder.AlterColumn<double>(
                name: "Mark",
                table: "Results",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "IdUser",
                table: "Results",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Mark",
                table: "Results",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTest",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUserAnswer",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
