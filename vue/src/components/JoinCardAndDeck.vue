<template>
  <div>
    <section>
      <card-details v-bind:card="card" v-bind:key="card.cardId" />
    </section>
    <!-- <div
      class="decks"
      v-for="deck in myDecks"
      v-bind:key="deck.deckId"
      v-bind:style="{ 'background-color': deck.backgroundColor }"
    >
      <p class="eachDeck">
        {{ deck.name }}<br />
        Deck: {{ deck.deckId }}<br /><br />
        {{ deck.description }}<br />

        Creator ID: {{ deck.creatorId }}
      </p>
      <button @click="selectThisDeck(deck.deckId)">Add To This Deck</button>
    </div> -->
  </div>
</template>

<script>
import CardDetails from './CardDetails.vue';
// import cardDeckIdService from "../services/CardDeckIdService";


export default {
  name: "join-card-and-deck",
//  props: {
//     userDecks: [],
//     deck: {
//       name: "",
//       description: "",
//       deckId: 0, //this.$route.params.deckId,
//       deckKeywords: "",
//       creator: "",
//       creatorId: 0,
//       deckDate: Date,
//       isPublic: false,
//     },
//    cardID: this.$store.state.cardID,
//    card: Object,
//  },
  data() {
    return {
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
//       showAddCardToDeckForm: false,
//       showDeleteCardFromDeckForm: false,
//       hideForm: false,
//       eachDeck: {
//         name: "",
//         description: "",
//         deckId: 0, //this.$route.params.deckId,
//         deckKeywords: "",
//         creator: "",
//         creatorId: 0,
//         deckDate: Date,
//         isPublic: false,
//     },
//     singleCardID: this.$store.state.cardID,
//     };
//   },
//   computed: {
//     myDecks() {
//       return this.$store.state.userDecks.filter((deck) => {
//         return deck;
//       });
    }
  },
  components: { CardDetails },
  methods: {
    selectThisDeck(deckId) {
      this.$store.commit("SET_DECK_ID", deckId);
      this.addCardToDeck(deckId, this.cardID);
    },

//     addCardDeck(cardID, deckID) {
//       cardDeckIdService
//         .addCardToDeck(cardID, deckID)
//         .then((response) => {
//           if (response.status === 201) {
//             this.$router.back();
//           }
//         })
//         .catch((error) => {
//           alert(error);
//         });
//     },
  },
};
</script>

<style>
</style>