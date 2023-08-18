using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_API.Models
{
    public class CharacterMovie
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
