<template>
  <div id="PublicDecksPage">
    <div
      class="decks"
      v-for="deck in $store.state.decks"
      v-bind:key="deck.deckId"
      v-bind:style="{ 'background-color': deck.backgroundColor }"
    >
      <p class="eachDeck">
        {{ deck.name }}<br /><br />
        {{ deck.description }}<br />
        {{ deck.creator }}
      </p>
    </div>
  </div>
</template>

<script>
import deckService from "../services/DeckService";
export default {
  name: "publicDecks",
  data() {
    return {
      showAddDeck: false,
      newDeck: {
        name: "",
        description: "",
        deckId: 0,
        deckKeywords: "",
        creator: "",
        deckDate: "",
        isPublic: false,
        creatorId: this.$store.state.user.userId,
      },
      errorMsg: "",
    };
  },
  created() {
    this.retrievePublicDecks();
  },
  methods: {
    retrievePublicDecks() {
      //we need to look at this one!
      this.$store.commit("SET_DECKS", []); //reset before pulling decks
      deckService
        .getPublicDecks()
        .then((response) => {
          console.log(response.data);
          this.$store.commit("SET_DECKS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>

<style>
</style>