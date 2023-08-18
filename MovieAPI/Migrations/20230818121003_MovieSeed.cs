using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class MovieSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "Title", "Trailer", "Year" },
                values: new object[] { 1, "Anthony Russo", 1, "Action", "x", "Avengers: Endgame", "https://www.youtube.com/watch?v=hA6hldpSTF8", 2019 });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "Title", "Trailer", "Year" },
                values: new object[] { 2, "David Ayer", 2, "Fantasy", "x", "Suicide Squad", "https://www.youtube.com/watch?v=CmRih_VtVAs", 2016 });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "Title", "Trailer", "Year" },
                values: new object[] { 3, "James Gunn", 1, "Action", "x", "Guardians of the Galaxy", "https://www.youtube.com/watch?v=0t2bmsXeFYE", 2014 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
