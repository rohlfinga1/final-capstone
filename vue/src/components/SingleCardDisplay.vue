<template>
  <div class="card" v-bind:key="this.$route.params.cardId" v-bind:card="card">
    <div class="content">
      <div class="front">
        <h2>{{ card.front }}</h2>
        <p><em>{{ card.keywords }}</em></p>
      </div>
      <div class="back">
        <h2>{{ card.back }}</h2>
      </div>
    </div>
  </div>

  <!-- <router-link
      :to="{ name: 'EditCard', params: {cardID: $route.params.cardID} }"
      class="btn editCard"
    >Edit Card</router-link>
    <div class="status-message error" v-show="errorMsg !== ''">{{errorMsg}}</div>
    <comments-list :comments="card.comments" /> -->
</template>

<script>
import deckCardService from "../services/DeckCardService.js";

export default {
  name: "single-card-display",
  data() {
    return {
      showFront: true,
      errorMsg: "",
      card: {
        cardId: 0,
        deckId: 0,
        front: '',
        back: '',
        keywords: ''
      }
    };
  },
  methods: {
    retrieveCard() {
      deckCardService
        .getCard(this.$route.params.deckId, this.$route.params.cardId)
        .then((response) => {
          this.$store.commit("SET_CURRENT_CARD", response.data);
          //this.showFront = true;
        })
        .catch((error) => {
          if (error.response && error.response.status === 404) {
            alert(
              "Card not available. This card may have been deleted or you have entered an invalid card ID."
            );
            this.$router.push({ name: "Home" });
          }
        });
    },
  },
};
</script>


<style>
@import 'https://fonts.googleapis.com/css?family=Lily+Script+One';

body {
  background: #eee;
  font-family: 'Lily Script One', 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif
}

.card {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 300px;
  height: 300px;
  margin: -150px;
  float: left;
  perspective: 500px;
}

.content {
  position: absolute;
  width: 100%;
  height: 100%;
  box-shadow: 0 0 15px rgba(0,0,0,0.1);

  transition: transform 1s;
  transform-style: preserve-3d;
}

.card:hover .content {
  transform: rotateY( 180deg ) ;
  transition: transform 0.5s;
}

.front,
.back {
  position: absolute;
  height: 100%;
  width: 100%;
  background: white;
  line-height: 300px;
  color: #03446A;
  text-align: center;
  font-size: 60px;
  border-radius: 5px;
  backface-visibility: hidden;
}

.back {
  background: #03446A;
  color: white;
  transform: rotateY( 180deg );
}
</style>