import axios from 'axios';

export default 
{
  getAllPublicCards() {
    return axios.get(`/card`);
  },
  getCardsByUserId(userId) {
    return axios.get(`${userId}/card`); 
  },
  getAuthenticatedCardSearchResults(userId, searchInput) {
    return axios.get(`/${userId}/cardsearch/${searchInput}`); 
  },
  getPublicCardSearchResults(searchInput) {
    return axios.get(`/cardsearch/${searchInput}`); 
  },
  getCardsByDeckId(deckId) {
    return axios.get(`/deck/${deckId}/card`);
  },

  getCard(cardId) {
    return axios.get(`/card/${cardId}`)
  },

  addCard(card) {
    return axios.post(`/card`, card);
  },

  updateCard(cardId, card) {
    return axios.put(`/card/${cardId}`, card);
  },

  deleteCard(cardId) {
    return axios.delete(`/card/${cardId}`);
  },
}