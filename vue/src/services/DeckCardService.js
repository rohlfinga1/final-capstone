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
  getCards(deckId) {
      return axios.get(`/deck/${deckId}/card`);
  },

  //about cards


  getCard(deckId, cardId) {
    return axios.get(`/deck/${deckId}/card/${cardId}`)
  },

  addCard(deckId, card) {
    return axios.post(`/deck/${deckId}/card`, card);
  },

  updateCard(deckId, card) {
    return axios.put(`/deck/${deckId}/card/${card.id}`, card);
  },

  deleteCard(deckId, cardId) {
    return axios.delete(`/deck/${deckId}/card/${cardId}`);
  }

}