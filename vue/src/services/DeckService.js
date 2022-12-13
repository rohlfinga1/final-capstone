import axios from 'axios';

export default 
{
  getUserDecks(userId){
    return axios.get(`/${userId}/deck`);//new method for my decks and public
  },
  getPublicDecks(){
    return axios.get('/deck/public');//check /deckendpnt
    //return axios.get('/deck');//check /deckendpnt

  },
  
  getAuthenticatedDeckSearchResults(userId, searchInput) {
    return axios.get(`/${userId}/decksearch/${searchInput}`); 
  },
  getPublicDeckSearchResults(searchInput) {
    return axios.get(`/decksearch/${searchInput}`);
  },

  getDeck(deckId) {
    return axios.get(`deck/${deckId}`);
  },
  AddDeck(newDeck){
    return axios.post(`${newDeck.creatorId}/deck`, newDeck);
  },
  updateDeck(deckId, deck) {
    return axios.put(`/deck/${deckId}`, deck);
  },

  deleteDeck(deckId) {
    return axios.delete(`/deck/${deckId}`);
  },

  

}