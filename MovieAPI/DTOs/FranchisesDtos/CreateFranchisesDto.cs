using System.ComponentModel.DataAnnotations;

namespace MovieAPI.DTOs.FranchisesDtos
{
    public class CreateFranchisesDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
