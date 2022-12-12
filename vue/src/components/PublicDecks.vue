<template>
<div id="PublicDecksPage">
  <h1>Public Decks</h1>

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
    //console.log(this.$store.state.user.userId);
    this.$store.commit("SET_DECKS", []);//reset before pulling decks
    deckCardService.getPublicDecks().then(response => {
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
 .decks {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  border-width: 3px;
  border-color: black;
  border: black;
  align-items: center;
  font-size: 16px;
  width: 60%;
  margin: 10px;
  padding: 20px;
  cursor: pointer;
  font-weight: bold;
}
#decks {
  color: #f7fafc;
  border-radius: 10px;
  padding: 40px;
  flex: 1;
  margin: 10px;
  text-align: center;
  text-decoration: none;
  cursor: pointer;
  width: 60%;
}
.eachDeck {
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
}
.addDeck {
  align-items: center;
  color: #f7fafc;
  border-radius: 10px;
  background-color: mediumaquamarine;
  font-size: 16px;
  width: 90%;
  margin: 10px;
  padding: 20px;
  margin-bottom: 35px;
  cursor: pointer;
}
.form-control {
  margin-bottom: 10px;
}

.deck:hover:not(.router-link-active),
.addDeck:hover {
  font-weight: bold;
}
h1 {
  font-weight: bold;
  text-align: center;
}
</style>