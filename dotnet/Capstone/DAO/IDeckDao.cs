using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IDeckDao
    {
        Deck GetDeck(int deckId);
        Deck CreateDeck(Deck deck);
        Deck UpdateDeck(int deckId, Deck deck);
        bool DeleteDeck(int deckId);
        List<Deck> GetAllDecks();
    }
}
