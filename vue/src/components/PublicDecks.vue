<template>
  <div id="PublicDecksPage">
    <div class="search-bar">
      <form @submit.prevent="retrievePublicResults">
        <input
          class="form-item"
          id="front"
          type="text"
          name="keyword"
          placeholder="Search Public Cards"
          v-model="searchInput"
        />
        <button class="form-item btn btn-submit" @click="retrievePublicResults">
          Search
        </button>
      </form>
    </div>
    <div
      class="decks"
      v-for="deck in $store.state.decks"
      v-bind:key="deck.deckId"
      v-bind:style="{ 'background-color': deck.backgroundColor }"
    >
      <p class="eachDeck">
        {{ deck.name }}<br /><br />
        {{ deck.description }}<br />
        Creator ID: {{ deck.creatorId }}
      </p>
    </div>
  </div>
</template>

<script>
import deckService from "../services/DeckService";
import cardService from "../services/CardService.js";

export default {
  name: "publicDecks",
  data() {
    return {
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
.search-bar {
  display: flex;
  align-items: center;
  float: right;
}

.form-item {
  margin: 10px;
}
</style>