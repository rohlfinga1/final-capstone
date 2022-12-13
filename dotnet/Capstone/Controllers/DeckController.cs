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
    //[Authorize]
    public class DeckController : ControllerBase
    {
        private IDeckDao deckDao;
        public DeckController(IDeckDao deckDao)
        {
            this.deckDao = deckDao;
        }

        [HttpGet("{userId}/deck")]  //new GetMethod for mydecks+public
        public ActionResult<List<Deck>> GetUserDecks(int userId)
        {
            List<Deck> userDecks = deckDao.GetDecksForUser(userId);
          
            if (userDecks == null)
            {
                userDecks = new List<Deck>();
            }

            //  empty list, or full list
            if (userDecks.Count == 0)
            {
                return NotFound();
            }

            return userDecks;
        }

        [HttpGet("{userId}/decksearch/{searchInput}")]
        public ActionResult<List<Deck>> SearchMyDecks(int userId, string searchInput)
        {
            List<Deck> decks;

            if (searchInput == "")
            {
                decks = deckDao.GetDecksForUser(userId);
            }
            else
            {
                decks = deckDao.GetMyDecksByKeywords(userId, searchInput);
            }

            if (decks == null)
            {
                decks = new List<Deck>();
            }

            //  empty list, or full list
            if (decks.Count == 0)
            {
                return NotFound();
            }

            return decks;
        }

        [AllowAnonymous]
        [HttpGet("/decksearch/{searchInput}")]
        public ActionResult<List<Deck>> SearchPublicCards(string searchInput)
        {
            List<Deck> decks;
            if (searchInput == "")
            {
                decks = deckDao.GetAllPublicDecks();
            }
            else
            {
                decks = deckDao.GetPublicDecksByKeywords(searchInput);
            }

            if (decks == null)
            {
                decks = new List<Deck>();
            }

            //  empty list, or full list
            if (decks.Count == 0)
            {
                return NotFound();
            }

            return decks;
        }

        [AllowAnonymous]
        [HttpGet("public")]
        public ActionResult<List<Deck>> GetAllPublicDecks()
        {
            List<Deck> allPublicDecks = deckDao.GetAllPublicDecks();

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

        [HttpPost("{deck.creatorId}/deck")]
        public ActionResult<Deck> AddDeck(Deck deck)
        {
            if (deck.CreatorId != 0)
            {
                Deck added = deckDao.CreateDeck(deck);
                return Created($"/{added.DeckId}", added);
            }
            else
            {
                return Unauthorized();
            }
            
        }

        [HttpPut("/editdeck/{id}")]
        public ActionResult<Deck> UpdateExistingDeck(int id, Deck deck)
        {
            Deck existingDeck = deckDao.GetDeck(id);
            if (existingDeck == null) // if the deck does not exist
            {
                return NotFound();
            }

            Deck updatedDeck = deckDao.UpdateDeck(id, deck);

            return updatedDeck;
        }

        // do we need this to authorize role admin?
        [HttpPut("{id}")]
        public ActionResult<Deck> UpdateDeckAsAdmin(int deckId, Deck deck)
        {
            Deck existingDeck = deckDao.GetDeck(deckId);
            if (existingDeck == null) // if the deck does not exist
            {
                return NotFound();
            }

            Deck updatedDeck = deckDao.UpdateDeckAsAdmin(deckId, deck);

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
