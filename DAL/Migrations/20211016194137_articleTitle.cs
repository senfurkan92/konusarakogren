using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class articleTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleTitle",
                table: "Quizzes",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleTitle",
                table: "Quizzes");
        }
    }
}
