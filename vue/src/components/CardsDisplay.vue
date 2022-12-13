<template>
  <section class="card-overview">
    <!--<join-card-and-deck v-bind:key="card.cardId" v-show="showSelectAddingDeckForm = !showSelectAddingDeckForm"/>v-on:submit="!showAddForm"
    <delete-card-from-deck v-bind:key="card.cardId" v-show="showSelectDeletingDeckForm = !showSelectDeletingDeckForm"/>v-show="showAddForm == true"-->
    <div class="card">
      <div class="form-content">
        <br/>
        <ul v-for="deck in selectedDecks" v-bind:key="deck.deckId">
          <li>Deck {{ deck.deckId }}: {{ deck.name }}</li>
        </ul>
        <form v-on:submit="selectForAdding(deckID, cardID)">
          <label for="card">Enter card ID to select card: </label>
          <input
            id="deck"
            type="text"
            name="deck"
            value="cardID"
            v-model="this.$store.state.cardID"
          />
          <br />
          <label for="deck">Enter deck ID to select deck: </label>
          <input
            id="deck"
            type="text"
            name="deck"
            value="deckID"
            v-model="this.$store.state.deckID"
          />
          <br />
          <br />
          <button
            class="btn btn-submit"
            @click="parseInt(selectForAdding(deckID, cardID))"
            v-bind:key="deck.deckId"
          >
            Select
          </button>
        </form>
      </div>
    </div>
    <!-- <div class="form-block" v-show="showDeleteForm == true">
      <form v-on:submit="!showDeleteForm">
        <section v-for="deck in selectedDecks" v-bind:key="deck.deckId">
          <button @click="selectThisDeckForDeleting(deck.deckId)">
            {{ deck.name }}
          </button>
          <br />
        </section>
        <button class="btn btn-submit">Done</button>
      </form>
    </div> -->
    <div v-for="card in selectedCards" v-bind:key="card.cardId">
      <section>
        <card-details v-bind:card="card" v-bind:key="card.cardId" />
      </section>
      <div class="card-details" v-bind:card="card" v-bind:key="card.cardId">
        <button
          class="addbtn"
          v-if="showAddForm == false"
          @click="selectThisCardForAdding(card.cardId)"
        >
          Add to A Deck
        </button>

        <p>Card {{ card.cardId }} &nbsp; Date Created: {{ card.cardDate }}</p>
        <p>Creator ID: {{ card.creatorId }}</p>
        <p>Tags: {{ card.cardKeywords }}</p>
      </div>
    </div>
  </section>
  <!-- <button
        v-if="showDeleteForm == false"
        @click="selectThisCardForDeleting(card.cardId)"
      >
        Delete From A Deck
      </button> -->
</template>

<script>
import CardDetails from "./CardDetails.vue";
//import cardService from "../services/CardService";
import cardDeckIdService from "../services/CardDeckIdService"
//import JoinCardAndDeck from "./JoinCardAndDeck.vue";
//import DeleteCardFromDeck from "./DeleteCardFromDeck.vue"
//import Popup from './Popup.vue'

export default {
  name: "cards-display",
  props: {
    cards: [],
    decks: [],
    cardID: {},
    deckID: {},
    user: Object,
  },
  data() {
    return {
      hideForm: false,
      //showAddForm: false,
      showDeleteForm: false,
      userId: this.$store.state.user.userId,
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
        deckId: 0, //this.$route.params.deckId,
        deckKeywords: "",
        creator: "",
        creatorId: 0,
        deckDate: "",
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
        return deck.creatorId === this.userId;
      });
    },
  },
  components: {
    CardDetails,
    //JoinCardAndDeck,
    //DeleteCardFromDeck
    //Popup
  },
  // created() {
  //   this.selectThisCardForAdding(cardId);
  //   this.selectThisCardForDeleting(cardId);
  //   this.selectThisDeckForAdding(deckId);
  //   this.selectThisDeckForDeleting(deckId);
  //   this.addCardDeck(deckID, cardID);
  //   this.deleteCardDeck(deckID, cardID)
  // },
  methods: {
    selectThisCardForAdding(cardId) {
      //this.showAddForm = true;
      console.log(`${cardId}`);
      this.$store.commit("SET_CARD_ID", cardId);
    },
    // selectThisCardForDeleting(cardId) {
    //   this.showSelectDeletingDeckForm = true;
    //   this.$store.commit("SET_CARD_ID", cardId);
    // },

    selectThisDeckForAdding(deckId) {
      //this.showAddForm = false;
      console.log(`${deckId}`);
      this.$store.commit("SET_DECK_ID", deckId);
      this.addCardDeck(this.deckID, this.cardID);
    },
    // selectThisDeckForDeleting(deckId) {
    //   this.showDeleteForm = false;
    //   this.$store.commit("SET_DECK_ID", deckId);
    //   this.deleteCardDeck(this.deckID, this.cardID);
    // },

    addCardDeck(deckID, cardID) {
      console.log(`${deckID}, ${cardID}`);
      this.$router.replace({ path: `/deck/${deckID}/card/${cardID}` });
      console.log(`${deckID}, ${cardID}`);
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
    //   deleteCardDeck(deckID, cardID) {
    //     this.$router.push({ path: `/deck/${deckID}/card/${cardID}` });
    //     cardService
    //       .deleteCardFromDeck(deckID, cardID)
    //       .then((response) => {
    //         if (response.status === 204) {
    //           this.$router.back();
    //         }
    //       })
    //       .catch((error) => {
    //         alert(error);
    //       });
    //   },
  },
};
</script>

<style>
.card-overview {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}

.card {
  /* position: absolute;
  top: 50%;
  left: 50%; */
  width: 300px;
  height: 300px;
  margin: 30px;
  /* float: left; */
  perspective: 500px;
}
.form-content {
  /* position: absolute; */
  width: 100%;
  height: 100%;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
  text-align: center;
  background-color: white;
}

p {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}

label {
  font-family: 'Courier New', Courier, monospace;
  text-align: center;
}

ul li {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: start;
  font-size: 10px;
  margin-top: 2px;
  margin-bottom: 2px;
}

/* .card-details {
  display: flex;
  justify-content: center;
} */
</style>