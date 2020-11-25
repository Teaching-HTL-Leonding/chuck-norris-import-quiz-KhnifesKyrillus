using Microsoft.EntityFrameworkCore.Migrations;

namespace ChuckNorrisImport.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Facts",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuckNorrisId = table.Column<string>("nvarchar(40)", maxLength: 40, nullable: true),
                    Url = table.Column<string>("nvarchar(1024)", maxLength: 1024, nullable: true),
                    Joke = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Facts", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Facts");
        }
    }
}