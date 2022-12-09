<template>
<div id="MyDecksPage">


    <div
  class="decks"
  v-for="deck in $store.state.decks"
  v-bind:key="deck.id"
  v-bind:style="{ 'background-color': deck.backgroundColor }"
    >
  <p class="eachDeck">
    {{ deck.name }}<br /><br />
    {{ deck.description }}<br />
    {{ deck.creatorId }}
  </p>
</div>
</div>
</template>

<script>
import deckCardService from "../services/DeckCardService";
export default {
name: "myDecks",
data() {
  return {
    showAddDeck: false,
    newDeck: {
      name: "",
      description: "",
      deckKeywords: "",
      deckId: "0",
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
    const userId = this.$store.state.user.userId;
    //console.log(this.$store.state.user.userId);
    this.$store.commit("SET_DECKS", []);//reset before pulling decks
    deckCardService.getUserDecks(userId).then(response => {
      console.log(response.data);
      this.$store.commit("SET_DECKS", response.data);
                     
  }).catch((error) => {
    alert(error);
  });
  },
}

}        
    

</script>

<style>

</style>