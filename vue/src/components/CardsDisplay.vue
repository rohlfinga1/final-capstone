<template>
  <section class="card-overview">
    <div v-for="card in selectedCards" v-bind:key="card.cardId">
      <section>
        <card-details v-bind:card="card" v-bind:key="card.cardId" />
      </section>
      <p>Card {{ card.cardId }} &nbsp; Date Created: {{ card.cardDate }}</p>
      <p>Creator: {{ card.creator }}</p>
      <p>Tags: {{ card.cardKeywords }}</p>
    </div>
  </section>
</template>

<script>
//import cardService from "../services/CardService.js";
import CardDetails from "./CardDetails.vue";

export default {
  name: "cards-display",
  props: ["cards"],
  computed: {
    selectedCards() {
      return this.$store.state.cards.filter((card) => {
        return card;
      });
    },
  },

// else if (this.activeRoute == 'authCardSearch') {
//         console.log(`${this.$route.params.userId}, ${this.$route.params.searchInput}`);
//         cardService.getAuthenticatedCardSearchResults(this.$route.params.userId, this.$route.params.searchInput)
//         .then((response) => {
//           this.$store.commit("SET_CARDS", response.data);
//         })
//         .catch((error) => {
//           alert(error);
//         });
//       } else if (this.activeRoute == 'viewSingleDeckOfCards') {
//           console.log(`${this.route.params.deckId}`);
//         
//       } else if (this.activeRoute == 'myCards') {
//           console.log(`${this.$route.params.userId}`);
//         cardService.getCardsByUserId(this.$route.params.userId)
//         .then((response) => {
//           this.$store.commit("SET_CARDS", response.data);
//         })
//         .catch((error) => {
//           alert(error);
//         });
//       } else { // no route? 
//       console.log(`${this.currentRouteName}`);
//         cardService.getAllPublicCards()
//         .then((response) => {
//           this.$store.commit("SET_CARDS", response.data);
//         })
//         .catch((error) => {
//           alert(error);
//         });
//       }
      
//     },
//  },
  components: {
    CardDetails,
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