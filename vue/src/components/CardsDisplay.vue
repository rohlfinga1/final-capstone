<template>
  <section class="card-overview">
    <div v-for="card in selectedCards" v-bind:key="card.cardId">
      <section>
        <card-details v-bind:card="card" v-bind:key="card.cardId" />
      </section>
      <button @click="selectCardToAdd(card.cardId)">Add to A Deck</button>
      <button @click="deleteCardFromDeck(card.cardId)">Delete From A Deck</button>
      <p>Card {{ card.cardId }} &nbsp; Date Created: {{ card.cardDate }}</p>
      <p>Creator: {{ card.creator }}</p>
      <p>Tags: {{ card.cardKeywords }}</p>
    </div>
  </section>
</template>

<script>
import cardService from "../services/CardService.js";
import CardDetails from "./CardDetails.vue";
//import Popup from './Popup.vue'

export default {
  name: "cards-display",
  props: ["cards"],
  data() {
    return {
      editingCardId: 0,
    }
  },
  computed: {
    selectedCards() {
      return this.$store.state.cards.filter((card) => {
        return card;
      });
    },
  },
  components: {
    CardDetails,
    //Popup
  },
  methods: {
    selectCardToAdd(card.cardId) {

    },
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
    deleteCardFromDeck(deckId, cardId) {
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
.card-overview {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}

p {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}
</style>