using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Card
    {
        public string Front { get; set; }
        public string Back { get; set; }
        public string CardKeywords { get; set; }
        public int CardId { get; set; }
        public DateTime DateMade { get; set; }
        public string Creator { get; set; } = "";
        public int CreatorId { get; set; } = 0;
    }
}
