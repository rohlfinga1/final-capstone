using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ICardDao
    {
        Card GetCard(int cardId);

        Card CreateCard(Card card);

        Card UpdateCard(int cardId, Card card);

        bool DeleteCard(int cardId);

        List<Card> GetAllCards();
        List<Card> GetAllCardsByDeckId();



    }
}
