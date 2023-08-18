using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string? Genre { get; set; }
        public int? Year { get; set; }
        [MaxLength(50)]
        public string? Director { get; set; }
        [MaxLength(50)]
        public string? Picture { get; set; }
        [MaxLength(450)]
        public string? Trailer { get; set; }
        //Foreign Key
        public int? FranchiseId { get; set; }
        public Franchise? Franchise { get; set; }
        public ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}
