using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ICardDeckIdDao
    {
        public List<CardDeckId> GetCardDeckIdArray(); // could this be used for modifying decks & creating cards? needs route
        public bool AddCardToDeck(int deckId, int cardId);
        public bool DeleteCardFromDeck(int deckId, int cardId);
    }
}
