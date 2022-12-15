<template>
  <div>
    <!-- <li>
          <router-link v-bind:to="{ name: 'Home' }" class="link"
            >HOME</router-link
          >
        </li> make route to go to all public cards-->
        <!-- <li>
          <router-link v-bind:to="{ name: 'Home' }" class="link"
            >HOME</router-link
          >
        </li> make route to go to all of my cards-->
    <myDecks />
    <!--<decks-display/>-->
  </div>
</template>

<script>
//import DeckDisplay from '../components/DeckDisplay.vue';
import MyDecks from "../components/MyDecks.vue";

export default {
  name: "account-page",
  components: { MyDecks },
  // data() {
  //   return {DeckDisplay 
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
/* div {
  display: flex;
  flex-direction: column;
} */
</style>