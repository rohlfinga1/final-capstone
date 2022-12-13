import axios from 'axios';

export default 
{
  getCardDekIdArray() {
      return axios.get(`deck/card/`);
  },

  addCardToDeck(deckId, cardId) {
    return axios.post(`/deck/${deckId}/card/${cardId}`, {deckId, cardId});
  },

  deleteCardFromDeck(deckId, cardId) {
    return axios.delete(`/deck/${deckId}/card/${cardId}`);
  },
}