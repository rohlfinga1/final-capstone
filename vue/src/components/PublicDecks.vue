<template>
  <div id="PublicDecksPage">
    <div class="layout ">
      <public-cards-search-bar class="color"/>
    </div>
    <span class="layout displayDecks">
      <span
      id="deck"
      class="decks"
      v-for="deck in $store.state.decks"
      v-bind:key="deck.deckId"
    >
      <deck-display v-bind:deck="deck" v-bind:key="deck.deckId" />
    </span>
    </span>
  </div>
</template>

<script>
import deckService from "../services/DeckService";
import cardService from "../services/CardService.js";
import PublicCardsSearchBar from './PublicCardsSearchBar.vue';
import DeckDisplay from './DeckDisplay.vue'

export default {
  components: { PublicCardsSearchBar, DeckDisplay },
  name: "publicDecks",
  data() {
    return {
      userId: this.$store.state.user.userId,
      searchInput: "",
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
    loggedIn(userId) {
      if(userId > 0){
        return true;
      }
      else{
        return false;
      }
    },
    retrievePublicDecks() {
      //we need to look at this one!
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
    retrievePublicResults() {
        cardService
          .getPublicCardSearchResults(this.searchInput)
          .then((response) => {
            if (response.status == 200) {
              this.$store.commit("SET_CARDS", response.data);
              this.$router.push({path: `/cardsearch/${this.searchInput}`});
            }
          })
          .catch((error) => {
            alert(error);
          });
      }
  },
};
</script>
<!-- Search bar HTML & CSS from https://uiverse.io/alexruix/slippery-frog-10-->
<style>
.layout {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.displayDecks {
  display: flex;
  flex-direction: row;
  width: 250px;
  margin-inline: 5% 10%;
  writing-mode: horizontal-tb;
}
 .decks {
  /* display: flex;
  flex-direction: column;
  justify-content: space-between; */
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
/* #decks {
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
/* .search-bar {
  display: flex;
  align-items: center;
  float: right;
} */

.form-item {
  margin: 10px;
}

.group {
 display: flex;
 line-height: 28px;
 align-items: center;
 position: relative;
 max-width: 250px;
}

.input {
 width: 100%;
 height: 40px;
 line-height: 28px;
 padding: 0 1rem;
 padding-left: 2.5rem;
 border: 2px solid transparent;
 border-radius: 8px;
 outline: none;
 background-color: #fff;
 color: #0d0c22;
 transition: .3s ease;
}

.input::placeholder {
 color: #000;
}

.input:focus, input:hover {
 outline: none;
 border-color:  rgb(32 49 89 / 40%);
 background-color: #fff;
 box-shadow: 0 0 0 4px rgb(32 49 89 / 10%);
}

.icon {
 position: absolute;
 left: 1rem;
 fill: #9e9ea7;
 width: 1rem;
 height: 1rem;
} 
</style>