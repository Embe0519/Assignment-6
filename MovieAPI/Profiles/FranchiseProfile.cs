using AutoMapper;
using MovieAPI.DTOs.CharactersDtos;
using MovieAPI.DTOs.FranchisesDtos;
using MovieAPI.Models;

namespace MovieAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
       public FranchiseProfile() 
       {
            CreateMap<Franchise, ReadFranchisesDto>().ReverseMap();
            CreateMap<CreateFranchisesDto, Franchise>().ReverseMap();
            CreateMap<UpdateFranchisesDto, Franchise>().ReverseMap();
        }
    }
}
