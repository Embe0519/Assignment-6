using MovieAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.DTOs.MoviesDtos
{
    public class ReadMoviesDto
    {
        public string Title { get; set; }
        public string? Genre { get; set; }
        public int? Year { get; set; }
        public string? Director { get; set; }
        public string? Picture { get; set; }
        public string? Trailer { get; set; }
    }
}
