import axios from 'axios';

export default 
{
  GetAllDecks(){
    return axios.get('/deck');
  },
  getUserDecks(userId){
    return axios.get(`/deck/${userId}/public`);//new method for my decks and public
  },
  AddDeck(newDeck){
      return axios.post('/deck', newDeck);

  },

   //about cards
  getCards(deckId) {
      return axios.get(`/deck/${deckId}/card`);
  },

  getSearchResults(keyword) {
    return axios.get(`/card/keyword/${keyword}`);
  },


  getCard(deckId, cardId) {
    return axios.get(`/deck/${deckId}/card/${cardId}`)
  },

  addCard(card) {
    return axios.post(`/deck/${card.deckId}/card`, card);
  },

  updateCard(deckId, card) {
    return axios.put(`/deck/${deckId}/card/${card.id}`, card);
  },

  deleteCard(deckId, cardId) {
    return axios.delete(`/deck/${deckId}/card/${cardId}`);
  }

}