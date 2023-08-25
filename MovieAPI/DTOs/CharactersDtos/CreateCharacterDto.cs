using MovieAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.DTOs.CharactersDtos
{
    public class CreateCharacterDto
    {
        public string? Name { get; set; }
       
        public string? Alias { get; set; }
        
        public string? Gender { get; set; }
       
        public string? Picture { get; set; }
       

    }
}
