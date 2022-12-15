<template>
  <div id="PublicDecksPage">
    <div id="layout ">
      <public-cards-search-bar class="color" />
    </div>
    <span id="layout" class="displayDecks">
      <span
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
import PublicCardsSearchBar from "./PublicCardsSearchBar.vue";
import DeckDisplay from "./DeckDisplay.vue";

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
      if (userId > 0) {
        return true;
      } else {
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
            this.$router.push({ path: `/search/${this.searchInput}` });
          }
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>
<!-- Search bar HTML & CSS from https://uiverse.io/alexruix/slippery-frog-10-->
<style>
/* #layout {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  flex-wrap: wrap;
} */
/* #layout {
  margin: auto;
} */

.displayDecks {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  /* width: 350px; */
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

span.decks > div {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  width: 300px;
  margin: 0;
}

span.decks {
  width: 300px;
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
  transition: 0.3s ease;
}

.input::placeholder {
  color: #000;
}

.input:focus,
input:hover {
  outline: none;
  border-color: rgb(32 49 89 / 40%);
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