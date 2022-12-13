using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardDeckIdController : ControllerBase
    {
        private ICardDeckIdDao cardDeckIdDao;
        private ICardDao cardDao;
        private IDeckDao deckDao;
        public CardDeckIdController(ICardDeckIdDao cardDeckIdDao, ICardDao cardDao, IDeckDao deckDao)
        {
            this.cardDeckIdDao = cardDeckIdDao;
            this.cardDao = cardDao;
            this.deckDao = deckDao;
        }

            [HttpGet("/deck/card")]
            public ActionResult<List<CardDeckId>> GetCardDeckIdArray()
            {
                List<CardDeckId> cardDeckIdList = cardDeckIdDao.GetCardDeckIdArray();

                if (cardDeckIdList != null)
                {
                    return cardDeckIdList;
                }
                else
                {
                    return NotFound();
                }
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
                return cardDeckIdDao.AddCardToDeck(deckId, cardId);
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

                bool result = cardDeckIdDao.DeleteCardFromDeck(deckId, cardId);

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
