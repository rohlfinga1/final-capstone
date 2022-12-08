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
        Deck CreateDeck(string name, bool isPublic, string description, string deckKeywords);
        Deck UpdateDeck(int deckId, Deck deck);
        bool DeleteDeck(int deckId);
        List<Deck> GetAllDecks();

        List<Deck> GetAllPublicDecks();

        //List<Deck> GetAllDecksByCreatorId(int creatorId);
    }
}
