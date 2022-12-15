<template>
  <div>
    <h1>Cards Search Results</h1>
    <div class=layout>
      <public-cards-search-bar/>
      <cards-display />
    </div>
  </div>
</template>

<script>
import cardService from '../services/CardService.js'
import CardsDisplay from '../components/CardsDisplay.vue'
import PublicCardsSearchBar from '../components/PublicCardsSearchBar.vue'

export default {
  name: "public-card-search",
  data() {
    return {
      searchInput: this.$route.params.searchInput,
    }
  },
  props: ["cards", "user"],
  components: { CardsDisplay, PublicCardsSearchBar },
  computed: {
    selectedDeckId() {
      return this.$store.state.deck.find((deck) => {
        return deck.deckId === this.$route.params.deckId;
      });
    },
    // getUserId() {
    //   return this.$store.state.user.userId
    // }
  },
  created() {
    this.publicCardSearch(this.searchInput);
  },
  methods: {
    publicCardSearch(searchInput) {
        cardService.getPublicCardSearchResults(searchInput)
        .then((response) => {
          this.$store.commit("SET_CARDS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>

<style>
h1 {
  text-align: center;
  font-family: "Courier New", Courier, monospace;
}
.layout {
  display: flex;
  flex-direction: column;
}
</style>