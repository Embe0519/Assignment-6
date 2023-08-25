using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Alias { get; set; }
        [MaxLength(50)]
        public string? Gender { get; set; }
        [MaxLength(150)]
        public string? Picture { get; set; } //Just URL
       
        public ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}
