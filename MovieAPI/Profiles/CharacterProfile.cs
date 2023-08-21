using AutoMapper;
using MovieAPI.DTOs.CharactersDtos;
using MovieAPI.DTOs.MoviesDtos;
using MovieAPI.Models;

namespace MovieAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile() 
        {
            CreateMap<Character, ReadCharacterDto>().ReverseMap(); 
            CreateMap<CreateCharacterDto, Character>().ReverseMap();
            CreateMap<UpdateCharacterDto, Character>().ReverseMap();
        }
    }
}
