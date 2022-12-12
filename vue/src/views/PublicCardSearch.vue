<template>
  <div>
    <h2>Cards Search Results</h2>
    <cards-display />
  </div>
</template>

<script>
import cardService from '../services/CardService.js'
import CardsDisplay from '../components/CardsDisplay.vue'

export default {
  name: "public-card-search",
  data() {
    return {
      searchInput: this.$route.params.searchInput,
    }
  },
  props: ["cards"],
  components: { CardsDisplay },
  computed: {
    selectedDeckId() {
      return this.$store.state.deck.find((deck) => {
        return deck.deckId === this.$route.params.deckId;
      });
    },
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
h2 {
  text-align: center;
  font-family: "Courier New", Courier, monospace;
}
</style>