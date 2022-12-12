<template>
  <div>
    <myDecks />
  </div>
</template>

<script>
import MyDecks from "../components/MyDecks.vue";
//import deckService from "../services/DeckService.js";

export default {
  name: "my-decks",
  components: { MyDecks },
  // data() {
  //   return {
  //     userId: this.$route.params.userId,
  //     showAddDeck: false,
  //     newDeck: {
  //       name: "",
  //       description: "",
  //       deckId: 0,
  //       deckKeywords: "",
  //       creator: "",
  //       creatorId: 0,
  //       deckDate: "",
  //       isPublic: false,
  //     },
  //     errorMsg: "",
  //   };
  // },
  // created() {
  //   this.retrieveMyDecks(this.userId);
  // },
  // methods: {
  //   retrieveMyDecks(userId) {
  //     //we need to look at this one!
  //     deckService
  //       .getUserDecks(userId)
  //       .then((response) => {
  //         this.$store.commit("SET_DECKS", response.data);
  //       })
  //       .catch((error) => {
  //         alert(error);
  //       });
  //   },
  // },
};
</script>

<style>
</style>