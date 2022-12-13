using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ICardDao
    {
        List<Card> GetMyCards(int userId);
        List<Card> GetPublicCards();
        List<Card> GetAllCardsByDeckId(int deckId);
        List<Card> GetPublicCardsByKeywords(string cardKeyword);
        List<Card> GetMyCardsByKeywords(int userId, string cardKeyword);
        Card GetCard(int cardId);
        Card CreateCard(Card card);
        Card UpdateCard(int cardId, Card card);
        bool DeleteCard(int cardId);

    }
}
