import axios from 'axios';

export default 
{
  getCardDeckIdArray() {
      return axios.get(`deck/card/`);
  },

  async addCardToDeck(cardId, deckId) {
    let response = axios.post(`/card/${cardId}/deck/${deckId}`);
    return response;
  },

  deleteCardFromDeck(deckId, cardId) {
    return axios.delete(`/deck/${deckId}/card/${cardId}`);
  },
}