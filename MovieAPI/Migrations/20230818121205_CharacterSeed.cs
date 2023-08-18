using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    public partial class CharacterSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "Gender", "Name", "Picture" },
                values: new object[,]
                {
                    { 1, "Iron Man", "Male", "Robert Downey Jr", "https://www.imdb.com/name/nm0000375/?ref_=tt_cl_t_1" },
                    { 2, "Black Widow", "Female", "Scarlett Johansson", "https://www.imdb.com/name/nm0424060/?ref_=tt_cl_t_5" },
                    { 3, "The Joker", "Male", "Jared Leto", "https://www.imdb.com/name/nm0001467/?ref_=tt_cl_t_2" },
                    { 4, "Harley Quinn", "Female", "Margot Robbie", "https://www.imdb.com/name/nm3053338/?ref_=tt_cl_t_3" },
                    { 5, "Star Lord", "Male", "Chris Pratt", "https://www.imdb.com/name/nm0695435/?ref_=tt_cl_i_1" },
                    { 6, "Mantis", "Female", "Pom Klementieff", "https://www.imdb.com/name/nm2962353/?ref_=tt_cl_t_4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
