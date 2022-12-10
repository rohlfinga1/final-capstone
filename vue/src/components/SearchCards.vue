<template>
  <div>
    <div class="search" v-for="card in this.searchResults" v-bind:key="card.cardId">
      <single-card-display
        v-for="card in this.cards"
        v-bind:card="card"
        v-bind:key="card.cardId"
      />
    </div>
  </div>
</template>

<script>
import deckCardService from "../services/DeckCardService.js";
import SingleCardDisplay from "./SingleCardDisplay.vue";

export default {
  name: "SearchCards",
  data() {
    return {
      searchResults: [],
      card: {
        cardId: 0,
        deckId: 0,
        front: "",
        back: "",
        keywords: "",
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
      console.log(this.$route.query.searchInput);
      deckCardService
        .getSearchResults(this.$route.query.searchInput)
        .then((response) => {
          console.log(response.data);
          this.$store.commit("SET_SEARCH_RESULT", response.data);
          this.searchResults = response.data;
          console.log(this.searchResults);
          alert(this.searchResults.forEach((result) => {
            `${result.cardId}, ${result.deckId}, ${result.front}, ${result.back}, ${result.keywords}`;
            }));
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
  components: {
    SingleCardDisplay,
  },
};
</script>

<style>
</style>