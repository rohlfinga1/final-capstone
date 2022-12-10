<template>
  <div>
    <h2>Search Results</h2>
    <div
        class="search"
        v-for="card in this.$store.state.searchResults"
        v-bind:key="card.cardId"
      >
      <single-card-display v-for="card in cards" v-bind:card="card" v-bind:key="card.cardId"/>
    </div>
  </div>
</template>

<script>
import deckCardService from '../services/DeckCardService.js';
import SingleCardDisplay from './SingleCardDisplay.vue';

export default {
  name: "SearchCards",
  props: ["searchResults"],
    data() {
    return {
      card: {
        cardId: 0,
        deckId: 0,
        front: '',
        back: '',
        keywords: ''
      },
      errorMsg: "",
    };
  },
  created() {
    this.retrieveResults();
  },
  methods: {
    retrieveResults() {
      //we need to look at this one!
      this.$store.commit("SET_SEARCH_RESULT", []);//reset before pulling decks
      deckCardService.getSearchResults(this.$route.params.card.keywords).then(response => {
        console.log(response.data);
        this.$store.commit("SET_SEARCH_RESULT", response.data);
                       
      }).catch((error) => {
        alert(error);
      });
    },
  },
  components: {
    SingleCardDisplay
  },
};
</script>

<style>
</style>