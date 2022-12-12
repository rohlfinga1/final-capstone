using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IDeckDao
    {
        List<Deck> GetDecksForUser(int userId);
        List<Deck> GetAllPublicDecks();
        List<Deck> GetMyDecksByKeywords(int userId, string deckKeyword);
        List<Deck> GetPublicDecksByKeywords(string deckKeyword);
        Deck GetDeck(int deckId);
        Deck CreateDeck(Deck deck);
        Deck UpdateDeck(int deckId, Deck deck);
        Deck UpdateDeckAsAdmin(int deckId, Deck deck);
        bool DeleteDeck(int deckId);
    }
}
