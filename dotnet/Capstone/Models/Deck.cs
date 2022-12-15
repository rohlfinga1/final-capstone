using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Deck
    {
        public int DeckId { get; set; }
        public int CreatorId { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        public DateTime DateMade { get; set; } 
        public string DeckKeywords { get; set; }
    }
}
