using MovieAPI.Models;

namespace MovieAPI.DTOs.MoviesDtos
{
    public class CreateMoviesDto
    {
        public string Title { get; set; }
        public string? Genre { get; set; }
        public int? Year { get; set; }
        public string? Director { get; set; }
        public string? Picture { get; set; }
        public string? Trailer { get; set; }
    }
}
