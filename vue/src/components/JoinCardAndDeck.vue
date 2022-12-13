<template>
  <div>
    <single-card-display />
    <div
      class="decks"
      v-for="deck in userDecks"
      v-bind:key="deck.deckId"
      v-bind:style="{ 'background-color': deck.backgroundColor }"
    >
      <p class="eachDeck">
        {{ deck.name }}<br />
        Deck: {{ deck.deckId }}<br /><br />
        {{ deck.description }}<br />

        Creator ID: {{ deck.creatorId }}
      </p>
      <button></button>
    </div>
  </div>
</template>

<script>
import cardDeckIdService from "../services/CardDeckIdService";
import SingleCardDisplay from "./SingleCardDisplay.vue";

export default {
  name: "join-card-and-deck",
  props: {
    userDecks: [],
    deck: {
      name: "",
      description: "",
      deckId: 0, //this.$route.params.deckId,
      deckKeywords: "",
      creator: "",
      creatorId: 0,
      deckDate: Date,
      isPublic: false,
    },
    cardID: this.$store.state.cardID,
  },
  data() {
    return {
      showAddCardToDeckForm: false,
      showDeleteCardFromDeckForm: false,
      hideForm: false,
    };
  },
  components: { SingleCardDisplay },
  methods: {
    selectThisDeckForAdding(deckId) {
      this.$store.commit("SET_DECK_ID", deckId);
      this.addCardToDeck(this.cardID, deckId);
    },

    addCardDeck(deckID, cardID) {
      //this.$router.push({ path: `/deck/${deckID}/card/${cardID}`});
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