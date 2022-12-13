<template>
  <div v-show="hideForm == true">
    <form v-on:submit="hideForm">
      <button
        v-for="deck in selectedDecks"
        v-bind:key="deck.deckId"
        @click="selectThisDeckForAdding(deck.deckId)"
      >
        {{ deck.name }}
      </button>
      <br />
      <button class="btn btn-submit">Done</button>
    </form>
  </div>
</template>

<script>
import cardDeckIdService from "../services/CardDeckIdService";

export default {
  name: "join-card-and-deck",
  props: {
    decks: [],
    deck: {
      name: "",
      description: "",
      deckId: 0, //this.$route.params.deckId,
      deckKeywords: "",
      creator: "",
      creatorId: 0,
      deckDate: "",
      isPublic: false,
    },
    cardID: this.$store.state.cardID,
  },
  data() {
    return {
      showAddCardToDeckForm: false,
      showDeleteCardFromDeckForm: false,
      hideForm: false
    };
  },
  methods: {
    selectThisDeckForAdding(deckId) {
      this.$store.commit("SET_DECK_ID", deckId);
      this.addCardToDeck(this.cardID, deckId);
    },

    addCardDeck(deckID, cardID) {
      this.$router.push({ path: `/deck/${deckID}/card/${cardID}`});
      cardDeckIdService
        .addCardToDeck(deckID, cardID)
        .then((response) => {
          if (response.status === 201) {
            this.$router.back();
          }
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>

<style>
</style>