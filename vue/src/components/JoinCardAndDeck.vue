<template>
  <div>
    <div>
      <span>
        <button
          class="btn"
          @click="showAddCardToDeckForm = !showAddCardToDeckForm"
        >
          Add Card to Deck
        </button>
        &nbsp; &nbsp;
        <button
          class="btn"
          @click="showDeleteCardFromDeckForm = !showDeleteCardFromDeckForm"
        >
          Delete Card From Deck
        </button>
      </span>
    </div>
    <div>
      <form
        v-show="showAddCardToDeckForm"
        @submit.prevent="addCardDeck"
        class="cardForm"
      >
        <div class="form-group" v-bind:key="card.cardId">
          <label for="cardToDeck">Enter card ID: </label>
          <input
            id="cardToDeck"
            type="text"
            name="cardToDeck"
            v-model="card.cardId"
          />
          <br />
        </div>
        <div class="form-group" v-bind:key="deck.deckId">
          <label for="deckToCard">Enter deck name: </label>
          <input
            id="deckToCard"
            type="text"
            name="deckToCard"
            v-model="deck.name"
          />
          <br />
        </div>
        <br />
        <button class="actions btn btn-submit">Save</button>
      </form>
      <form
        v-show="showDeleteCardFromDeckForm"
        @submit.prevent="deleteCardDeck"
        class="cardForm"
      >
        <div class="form-group" v-bind:key="card.cardId">
          <label for="cardToDeck">Enter Card ID: </label>
          <input
            id="cardToDeck"
            type="text"
            name="cardToDeck"
            v-model="card.cardId"
          />
          <br />
        </div>
        <div class="form-group" v-bind:key="deck.deckId">
          <label for="deckToCard">Enter deck name: </label>
          <input
            id="deckToCard"
            type="text"
            name="deckToCard"
            v-model="deck.name"
          />
          <br />
        </div>
        <br />
        <button class="actions btn btn-submit">Save</button>
      </form>
    </div>
  </div>
</template>

<script>
import cardService from "../services/CardService";

export default {
  name: "join-card-and-deck",
  props: ["decks"],
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
  card: {
    cardId: 0, //this.$route.params.cardId,
    front: "",
    back: "",
    keywords: "",
    creator: "",
    creatorId: 0,
    cardDate: "",
  },
  data() {
    return {
      showAddCardToDeckForm: false,
      showDeleteCardFromDeckForm: false,
      //deckId: this.$store.state.deck.deckId,
      //cardId: this.$store.state.card.cardId // this route params?
    };
  },
  methods: {
    addCardDeck(deckId, cardId) {
      cardService
        .addCardToDeck(deckId, cardId)
        .then((response) => {
          response.data;
        })
        .catch((error) => {
          alert(error);
        });
    },
    deleteCardDeck(deckId, cardId) {
      cardService.deleteCardFromDeck(deckId, cardId)
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
</style>