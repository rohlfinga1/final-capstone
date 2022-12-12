<template>
  <div>
    <h2>Cards in Deck "{{ deck.name }}"</h2>
    <p>{{ deck.creator }} created on {{ deck.deckDate }}</p>
    <br />
    <p>{{ deck.description }}</p>
    <cards-display />
  </div>
</template>

<script>
import cardService from "../services/CardService.js";
import deckService from "../services/DeckService.js";
import CardsDisplay from "../components/CardsDisplay.vue";

export default {
  name: "deck-of-cards",
  components: { CardsDisplay },
  data() {
    return {
      searchInput: this.$route.params.searchInput,
      tempDeck: {
        name: "",
        description: "",
        deckId: this.$route.params.deckId,
        deckKeywords: "",
        creator: "",
        creatorId: 0,
        deckDate: "",
        isPublic: false,
      },
    };
  },
  props: ["cards"],
  computed: {
    selectedCards() {
      return this.$store.state.cards.filter((card) => {
        return card;
      });
    },
  },
  created() {
    this.getDeckDetailsById(this.selectedDeckId);
    this.getCardsByDeck(this.selectedDeckId);
  },
  methods: {
    getCardsByDeck(selectedDeckId) {
      cardService
        .getCardsByDeckId(selectedDeckId)
        .then((response) => {
          this.$store.commit("SET_CARDS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
    getDeckDetailsById(selectedDeckId) {
      deckService
        .getDeck(selectedDeckId)
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