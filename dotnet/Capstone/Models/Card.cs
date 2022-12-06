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

        public int DeckId { get; set; }

       


    }
}
