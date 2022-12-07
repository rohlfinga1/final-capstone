using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ICardDao cardDao;
        public CardController(ICardDao cardDao)
        {
            this.cardDao = cardDao;
        }

        [HttpGet()]
        public ActionResult<List<Card>> GetAllCards()
        {
            List<Card> allCards = cardDao.GetAllCards();

            // null, empty list, or full list
            if (allCards == null)
            {
                return StatusCode(500);
            }
            else if (allCards.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return allCards;
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
    }
}
