<template>
  <section class="card-overview">
    <form v-show="showSelectAddingDeckForm == true">
      <button
        v-for="deck in selectedDecks"
        v-bind:key="deck.deckId"
        @click="selectThisDeckForAdding(deck.deckId)"
      >
        {{ deck.name }}
      </button>
      <br />
      <button @click="showSelectAddingDeckForm == false">Done</button>
    </form>
    <form v-show="showSelectDeletingDeckForm == true">
      <div v-for="deck in selectedDecks" v-bind:key="deck.deckId">
        <button @click="selectThisDeckForDeleting(deck.deckId)">
          {{ deck.name }}
        </button>
      </div>
      <button @click="showSelectDeletingDeckForm == false">Done</button>
    </form>
    <div v-for="card in selectedCards" v-bind:key="card.cardId">
      <section>
        <card-details v-bind:card="card" v-bind:key="card.cardId" />
      </section>
      <button @click="selectThisCardForAdding(card.cardId)">
        Add to A Deck
      </button>
      <button @click="selectThisCardForDeleting(card.cardId)">
        Delete From A Deck
      </button>
      <p>Card {{ card.cardId }} &nbsp; Date Created: {{ card.cardDate }}</p>
      <p>Creator ID: {{ card.creatorId }}</p>
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
  props: {
    cards: [],
    decks: [],
  },
  data() {
    return {
      showSelectAddingDeckForm: false,
      showSelectDeletingDeckForm: false,
      addCardId: 0,
      addDeckId: 0,
      deleteCardId: 0,
      deleteDeckId: 0,
      card: {
        cardId: 0,
        front: "",
        back: "",
        keywords: "",
        creator: "",
        creatorId: 0,
        dateMade: Date,
      },
      deck: {
        name: "",
        description: "",
        deckId: 0,
        deckKeywords: "",
        creator: "",
        creatorId: 0,
        dateMade: Date,
        isPublic: false,
      },
    };
  },
  computed: {
    selectedCards() {
      return this.$store.state.cards.filter((card) => {
        return card;
      });
    },
    selectedDecks() {
      return this.$store.state.decks.filter((deck) => {
        return deck;
      });
    },
  },
  components: {
    CardDetails,
    //Popup
  },
  methods: {
    selectThisCardForAdding(cardId) {
      this.showSelectAddingDeckForm = true;
      this.addCardId = cardId;
    },
    selectThisCardForDeleting(cardId) {
      this.showSelectDeletingDeckForm = true;
      this.deleteCardId = cardId;
    },
    selectThisDeckForAdding(deckId) {
      this.addDeckId = deckId;
      this.addCardToDeck(this.addDeckId, this.addCardId);
    },
    selectThisDeckForDeleting(deckId) {
      this.deleteDeckId = deckId;
      this.deleteCardFromDeck(this.deleteDeckId, this.deleteCardId);
    },
    addCardToDeck(deckId, cardId) {
      cardService
        .addCardToDeck(deckId, cardId)
        .then((response) => {
          this.$store.commit("SET_CARDS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
    deleteCardFromDeck(deckId, cardId) {
      cardService
        .deleteCardFromDeck(deckId, cardId)
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