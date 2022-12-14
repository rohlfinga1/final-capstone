import axios from 'axios';

export default 
{
  getCardDeckIdArray() {
      return axios.get(`deck/card/`);
  },

  addCardToDeck(cardId, deckId) {
    return axios.post(`/card/${cardId}/deck/${deckId}`, {cardId, deckId});
  },

  deleteCardFromDeck(deckId, cardId) {
    return axios.delete(`/deck/${deckId}/card/${cardId}`);
  },
}