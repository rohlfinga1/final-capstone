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
    return axios.get(`/${userId}/search/${searchInput}`); 
  },
  getPublicCardSearchResults(searchInput) {
    return axios.get(`/search/${searchInput}`); 
  },
  getCardsByDeckId(deckId) {
    return axios.get(`/deck/${deckId}/card`);
  },

  getCard(cardId) {
    return axios.get(`/card/${cardId}`)
  },

  addCard(card, deckId) {
    return axios.post(`/deck/${deckId}`, card);
  },

  updateCard(cardId, card) {
    return axios.put(`/card/${cardId}`, card);
  },

  deleteCard(cardId) {
    return axios.delete(`/card/${cardId}`);
  },
}