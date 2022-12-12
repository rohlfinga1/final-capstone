using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private ICardDao cardDao;
        private IDeckDao deckDao;
        public CardController(ICardDao cardDao, IDeckDao deckDao)
        {
            this.cardDao = cardDao;
            this.deckDao = deckDao;
        }

        [HttpGet("/{userId}/card")]
        public ActionResult<List<Card>> GetUserCards(int userId)
        {
            List<Card> cards = cardDao.GetMyCards(userId);

            if (cards == null)
            {
                cards = new List<Card>();
            }

            // null, empty list, or full list
            if (cards.Count == 0)
            {
                return NotFound();
            }
            return cards;
        }

        [HttpGet("/{userId}/cardsearch/{searchInput}")]
        public ActionResult<List<Card>> SearchMyCards(int userId, string searchInput)
        {
            List<Card> cards;

            if (searchInput == "")
            {
                cards = cardDao.GetMyCards(userId);
            }
            else
            {
                cards = cardDao.GetMyCardsByKeywords(userId, searchInput);
            }

            // null, empty list, or full list
            if (cards == null)
            {
                cards = new List<Card>();
            }

            if (cards.Count == 0)
            {
                return NotFound();
            }
            return cards;

        }

        [AllowAnonymous]
        [HttpGet("/cardsearch/{searchInput}")]
        public ActionResult<List<Card>> SearchPublicCards(string searchInput)
        {
            List<Card> cards;
            if (searchInput == "")
            {
                cards = cardDao.GetPublicCards();
            }
            else
            {
                cards = cardDao.GetPublicCardsByKeywords(searchInput);
            }

            // null, empty list, or full list
            if (cards == null)
            {
                cards = new List<Card>();
            }

            if (cards.Count == 0)
            {
                return NotFound();
            }
            return cards;
        }

        [HttpGet("/deck/{deckId}/card")]
        public ActionResult<List<Card>> GetAllCardsByDeckId(int deckId)
        {
            List<Card> allCardsByDeckId = cardDao.GetAllCardsByDeckId(deckId);

            if (allCardsByDeckId == null)
            {
                return StatusCode(500);
            }
            else if (allCardsByDeckId.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return allCardsByDeckId;
            }
        }

        [AllowAnonymous]
        [HttpGet()]
        public ActionResult<List<Card>> GetAllPublicCards()
        {
            List<Card> allPublicCards = cardDao.GetPublicCards();

            // null, empty list, or full list
            if (allPublicCards == null)
            {
                return StatusCode(500);
            }
            else if (allPublicCards.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return allPublicCards;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Card> RetrieveCard(int id)
        {
            Card card = cardDao.GetCard(id);

            if (card != null)
            {
                return card;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost()]
        public ActionResult<Card> AddCard(Card card)
        {
            Card added = cardDao.CreateCard(card);
            return Created($"/{added.CardId}", added);
        }

        [HttpPost("/deck/{deckId}/card/{cardId}")]
        public ActionResult<bool> AddCardToDeck(int deckId, int cardId)
        {
            Card card = cardDao.GetCard(cardId);
            Deck deck = deckDao.GetDeck(deckId);
            if (card == null || deck == null)
            {
                return NotFound();
            }
            return cardDao.AddCardToDeck(deckId, cardId);
        }

        [HttpPut("{id}")]
        public ActionResult<Card> UpdateExistingCard(int id, Card card)
        {
            Card existingCard = cardDao.GetCard(id);
            if (existingCard == null)
            {
                return NotFound();
            }

            Card updatedCard = cardDao.UpdateCard(id, card);

            return updatedCard;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCardFromDB(int id)
        {
            Card existingCard = cardDao.GetCard(id);

            if (existingCard == null)
            {
                return NotFound();
            }

            bool result = cardDao.DeleteCard(id);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("/deck/{deckId}/card/{cardId}")]
        public ActionResult DeleteCardFromDeck(int deckId, int cardId)
        {
            Card card = cardDao.GetCard(cardId);
            Deck deck = deckDao.GetDeck(deckId);
            if (card == null || deck == null)
            {
                return NotFound();
            }

            bool result = cardDao.DeleteCardFromDeck(deckId, cardId);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
