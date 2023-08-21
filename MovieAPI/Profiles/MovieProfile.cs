using AutoMapper;
using MovieAPI.DTOs.MoviesDtos;
using MovieAPI.Models;

namespace MovieAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() 
        {
            CreateMap<Movie, ReadMoviesDto>().ReverseMap(); //Makes it possible to map from either side. It won't ever be the wrong way around... 
            CreateMap<CreateMoviesDto, Movie>().ReverseMap();
            CreateMap<UpdateMoviesDto, Movie>().ReverseMap();
        }
    }
}
