<template>
  <section class="card-overview">
    <div v-for="card in selectedCards" v-bind:key="card.cardId">
      <section>
        <card-details v-bind:card="card" v-bind:key="card.cardId" />
      </section>
      <div class="card-details" v-bind:card="card" v-bind:key="card.cardId">
        <button
          class="addbtn"
          @click="selectThisCardForAdding(card.cardId)"
        >
          Add to A Deck
        </button>

        <p>Card {{ card.cardId }}</p>
        <p>Creator ID: {{ card.creatorId }}</p>
        <p>Tags: {{ card.cardKeywords }}</p>
      </div>
    </div>
  </section>
</template>
<!-- Add to deck button from https://uiverse.io/cssbuttons-io/stale-rattlesnake-87-->
<script>
import CardDetails from "./CardDetails.vue";
// import cardDeckIdService from "../services/CardDeckIdService"

export default {
  name: "cards-display",
  props: {
    cards: [],
    userDecks: [],
    decks: [],
    cardID: {},
    deckID: {},
    user: Object,
  },
  data() {
    return {
      hideForm: false,
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
      return this.$store.state.userDecks.filter((deck) => {
        return deck.creatorId === this.userId;
      });
    },
    setCurrentCard(cardId) {
      return this.$store.commit('SET_CURRENT_CARD_ID', cardId);
    }
  },
  components: {
    CardDetails,

  },
  methods: {
    selectThisCardForAdding(cardId) {
      console.log(`${cardId}`);
      this.$store.commit("SET_CARD_ID", cardId);
      if (this.userId !== 0 || this.userId !== null) {
        this.$router.push({path: `/card/${cardId}/deck/`});
      }
      else {
        this.$router.push({name: 'Home'});
      }
    },
  },
};
</script>
<!--  Single card CSS & HTML from: https://codepen.io/edeesims/pen/wvpYWW -->
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

.addbtn {
  padding: 1.3em 3em;
  font-size: 12px;
  text-transform: uppercase;
  letter-spacing: 2.5px;
  font-weight: 500;
  color: #000;
  background-color: #fff;
  border: none;
  border-radius: 45px;
  box-shadow: 0px 8px 15px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease 0s;
  cursor: pointer;
  outline: none;
  margin-left: 85px;
}

.addbtn:hover {
  background-color: #23c483;
  box-shadow: 0px 15px 20px rgba(46, 229, 157, 0.4);
  color: #fff;
  transform: translateY(-7px);
}

.addbtn:active {
  transform: translateY(-1px);
}
  /* 
  background-color: #FFFDEB;
  border: none;
} */

/* .card-details {
  display: flex;
  justify-content: center;
} */
</style>