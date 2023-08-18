using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class RelationshipMovieFranchise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FranchiseId",
                table: "Movies",
                column: "FranchiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Franchises_FranchiseId",
                table: "Movies",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Franchises_FranchiseId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Franchises");

            migrationBuilder.DropIndex(
                name: "IX_Movies_FranchiseId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "Movies");
        }
    }
}
