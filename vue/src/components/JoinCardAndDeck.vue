<template>
  <div class="joinLayout">
    <section>
    <div class="card" v-bind:key="this.$route.params.cardId">
      <div class="content">
        <div class="front">
          <p class="text">{{ selectCard.front }}</p>
        </div>
        <div class="back">
          <p class="text">{{ selectCard.back }}</p>
        </div>
      </div>
    </div>
  </section>

    <div
      class="decks"
      v-for="deck in myDecks"
      v-bind:key="deck.deckId"
    >
      <deck-display v-bind:deck="deck" v-bind:key="deck.deckId" />
      <br/>
      <button class="addcardbtn" @click="selectThisDeck(deck.deckId)">Add To This Deck</button>
    </div>
  </div>
</template>

<script>
// import CardDetails from './CardDetails.vue';
import cardDeckIdService from "../services/CardDeckIdService";
import DeckDisplay from "./DeckDisplay.vue";

export default {
  name: "join-card-and-deck",
  props: ["userDecks", "card"],
  components: { DeckDisplay },
  data() {
    return {
      showFront: true,
      userId: this.$store.state.user.userId,
      deck: {
        name: "",
        description: "",
        deckId: 0, //this.$route.params.deckId,
        deckKeywords: "",
        //creator: "",
        creatorId: this.userId,
        deckDate: Date,
        isPublic: false,
      },
      selectCard: {
        cardId: this.$route.params.cardId,
        front: '',
        back: '',
        keywords: '',
        //creator: '',
        creatorId: 0,
        dateMade: Date,
      },
      //selectedCardId: this.$route.params.cardId,
    };
  },
  computed: {
    myDecks() {
      return this.$store.state.decks.filter((deck) => {
        return deck.creatorId == this.userId;
      });
    },
    // selectedCard(selectedCardId) {
    //   console.log(this.selectedCardId);
    //   let arrayCard = this.$store.state.cards.filter((card) => {
    //     return card.cardId == selectedCardId;
    //   });
    //   const myCard = arrayCard[0];
    //   console.log(myCard.cardId);
    //   return myCard;
    // }
  },
  created() {
    // Do NOT refresh this page!!!
    this.selectCard = this.addSelectCardToActualCard(this.$route.params.cardId);
  },
  methods: {
    addSelectCardToActualCard(selectedCardId) {
      let arrayCard = this.$store.state.cards.filter((card) => {
        return card.cardId == selectedCardId;
      });
      const myCard = arrayCard[0];
      console.log(`${myCard.cardId}`);

      return myCard;
    },
    selectThisDeck(deckId) {
      this.$route.query.deckId = deckId
      this.$store.commit("SET_DECK_ID", deckId);
      this.addCardDeck(this.selectCard.cardId, deckId);
    },
    addCardDeck(cardID, deckID) {
      cardDeckIdService
        .addCardToDeck(cardID, deckID)
        .then((response) => {
          if (response.status === 201 || response.status === 200) {
            this.$router.push({name: 'add-card-to-deck', params: {cardID, deckID}}); 
            alert("Your card was successfully added!");
          }
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>
<!-- Add to deck button from https://uiverse.io/cssbuttons-io/stale-rattlesnake-87-->
<style>
@import "https://fonts.googleapis.com/css?family=Lily+Script+One";

body {
  background: #eee;
  font-family: "Lily Script One", "Trebuchet MS", "Lucida Sans Unicode",
    "Lucida Grande", "Lucida Sans", Arial, sans-serif;
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

.content {
  /* position: absolute; */
  width: 100%;
  height: 100%;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);

  transition: transform 1s;
  transform-style: preserve-3d;
}

.card:hover .content {
  transform: rotateY(180deg);
  transition: transform 0.5s;
}

.front,
.back {
  position: absolute;
  height: 100%;
  width: 100%;
  background: white;
  line-height: normal;
  color: #203159; /* #03446a;*/
  text-align: center;
  font-size: 30px;
  border-radius: 5px;
  backface-visibility: hidden;
}

.back {
  background: white;
  color: #203159;/* #03446a;*/
  transform: rotateY(180deg);
}

.text {
  margin-left: 10%;
  margin-right: 10%;
}
.decks {
  /* display: flex;
  flex-direction: column; */
  justify-content: space-between;
  border-width: 3px;
  border-color: black;
  border: black;
  align-items: center;
  font-size: 16px;
  width: 300px;
  margin: 10px;
  padding: 20px;
  cursor: pointer;
  font-weight: bold;
}
/* .eachDeck {
  text-align: center;
  align-items: center;
  color: #f7fafc;
  border-style: solid;
  border-radius: 10px;
  border-width: 2px;
  border-color: black;

  background-color: rgb(79, 189, 240);
  font-size: 16px;
  width: 90%;
  margin: 10px;
  padding: 20px;
  margin-bottom: 35px;
  cursor: pointer;
} */
.addcardbtn {
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
  margin-left: 45px;
}

.addcardbtn:hover {
  background-color: #23c483;
  box-shadow: 0px 15px 20px rgba(46, 229, 157, 0.4);
  color: #fff;
  transform: translateY(-7px);
}

.addcardbtn:active {
  transform: translateY(-1px);
}
.joinLayout {
  display: flex;
}
</style>