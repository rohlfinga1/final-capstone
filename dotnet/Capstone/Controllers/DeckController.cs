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
    public class DeckController : ControllerBase
    {
        private IDeckDao deckDao;
        public DeckController(IDeckDao deckDao)
        {
            this.deckDao = deckDao;
        }

        [HttpGet()]
        public ActionResult<List<Deck>> GetAllDecks()
        {
            List<Deck> allDecks = deckDao.GetAllDecks();

            // null, empty list, or full list
            if (allDecks == null)
            {
                return StatusCode(500);
            }
            else if (allDecks.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return allDecks;
            }
        }

        [HttpGet()]
        public ActionResult<List<Deck>> GetAllPublicDecks()
        {
            List<Deck> allPublicDecks = deckDao.GetAllDecks();

            // null, empty list, or full list
            if (allPublicDecks == null)
            {
                return StatusCode(500);
            }
            else if (allPublicDecks.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return allPublicDecks;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Deck> RetrieveDeck(int id)
        {
            Deck deck = deckDao.GetDeck(id);

            if (deck != null)
            {
                return deck;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost()]
        public ActionResult<Deck> AddCar(Deck deck)
        {
            Deck added = deckDao.CreateDeck(deck);
            return Created($"/{added.DeckId}", added);
        }

        [HttpPut("{id}")]
        public ActionResult<Deck> UpdateExistingDeck(int id, Deck deck)
        {
            Deck existingDeck = deckDao.GetDeck(id);
            if (existingDeck == null) // if the car does not exist
            {
                return NotFound();
            }

            Deck updatedDeck = deckDao.UpdateDeck(id, deck);

            return updatedDeck;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDeckFromDB(int id)
        {
            Deck existingDeck = deckDao.GetDeck(id);

            if (existingDeck == null)
            {
                return NotFound();
            }

            bool result = deckDao.DeleteDeck(id);

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
