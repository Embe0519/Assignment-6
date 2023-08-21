using System.ComponentModel.DataAnnotations;

namespace MovieAPI.DTOs.FranchisesDtos
{
    public class ReadFranchisesDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
