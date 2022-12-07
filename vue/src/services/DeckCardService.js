import axios from 'axios';

export default 
{
GetDecks(){
    return axios.get('/myDecks');
},

AddDeck(newDeck){
    return axios.post('/myDecks', newDeck)

},

//about cards
getCards(deckId) {
    return axios.get(`/deck/${deckId}`)
},

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