import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    userDecks:[],
    publicDecks: [],
    decks:[],
    deck:{
      name: '',
      description:'',
      deckId: 0,
      deckKeywords:'',
      //creator: '',
      creatorId: 0,
      dateMade: Date,
      isPublic: false
    },
    userCards: [],
    publicCards: [],
    searchCards: [],
    cards: [],
    card: {
      cardId: 0,
      front: '',
      back: '',
      keywords: '',
      creator: '',
      creatorId: 0,
      dateMade: Date,
      correct: false,
      wrong: false,
      scored: false
    },
    cardDeckIds: [],   // could this be used for modifying decks & creating cards? needs route
    cardDeckId: {
      cardId: 0,
      deckId: 0
    },
  },

  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },

    SET_DECKS(state, data) {
      state.decks = data;
    },

    SET_USER_DECKS(state, data) {
      state.userDecks = data;
    },

    SET_PUBLIC_DECKS(state, data) {
      state.userDecks = data;
    },

    SET_CURRENT_DECK(state, data) {
      state.deck = data;
    },

    // SET_DECK_ID(state, data) {
    //   state.deckID = data;
    // },

    SET_CARDS(state, data){
      state.cards = data;
    },

    SET_USER_CARDS(state, data) {
      state.userCards = data;
    },

    SET_PUBLIC_CARDS(state, data) {
      state.publicCards = data;
    },

    SET_SEARCH_CARDS(state, data) {
      state.searchCards = data;
    },
    
    SET_CURRENT_CARD(state, data) {
      state.card = data;
    },
    MARK_CORRECT(state, data) {
      state.card = data;
    },
    MARK_WRONG(state, data) {
      state.card = data;
    },
    MARK_SCORED(state, data) {
      state.card = data;
    }

    // SET_CARD_ID(state, data) {
    //   state.cardID = data;
    //   console.log(`${this.cardID}`)
    // },

    
  }
})
