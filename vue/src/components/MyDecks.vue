<template>
  <div id="MyDecksPage">
    <div class="Nav">
      <h1>Account</h1>
    </div>
    <my-cards-search-bar />

    <h2>My Decks</h2>
    <div class="displayDecks">
      <div>
        <div class="deck">
          <div class="details">
            <button class="addDeck" v-on:click="showAddDeck = !showAddDeck">
              Add New Deck
            </button>
          </div>
        </div>
      </div>

      <form v-if="showAddDeck" @submit.prevent="submitForm">
        Deck Name:
        <input type="text" class="form-control" v-model="newDeck.name" />
        Description:
        <input type="text" class="form-control" v-model="newDeck.description" />
        Deck Keywords:
        <input
          type="text"
          class="form-control"
          v-model="newDeck.deckKeywords"
        />
        Is this Public:
        <input
          type="checkbox"
          class="form-control"
          v-model="newDeck.isPublic"
        />
        <button class="btn btn-submit" @click="submitForm">Save</button>
        <button class="btn btn-cancel" v-on:click="showAddDeck = !showAddDeck">
          Cancel
        </button>
      </form>
      <!--<div v-bind:key="deck.deckId" v-bind:deck="deck">-->
      
        <router-link
          :to="{ name: 'DeckEditor', params: { deckId: deck.deckId } }"
          class="decks"
          v-for="deck in filterMyDecksOnly"
          v-bind:key="deck.deckId"
        >
          <deck-display v-bind:deck="deck" v-bind:key="deck.deckId" />
        </router-link>
      </div>
      <div>
        <h2>Public Decks</h2>

        <div class="displayDecks">
          <router-link
            :to="{ name: 'DeckEditor', params: { deckId: deck.deckId } }"
            id="publicDecks"
            class="decks"
            v-for="deck in filterPublicOnly"
            v-bind:key="deck.id"
          >
            <deck-display v-bind:deck="deck" v-bind:key="deck.deckId" />
          </router-link>
        </div>
      </div>
    </div>
</template>

<script>
import deckService from "../services/DeckService";
import cardService from "../services/CardService.js";
import DeckDisplay from "./DeckDisplay.vue";
import MyCardsSearchBar from "./MyCardsSearchBar.vue";

export default {
  components: { DeckDisplay, MyCardsSearchBar },
  name: "myDecks",
  data() {
    return {
      showEditDeck: false,
      searchInput: "",
      userId: this.$store.state.user.userId,
      showAddDeck: false,
      newDeck: {
        name: "",
        description: "",
        deckId: 0,
        deckKeywords: "",
        //creator: "",
        dateMade: Date,
        isPublic: false,
        creatorId: this.$store.state.user.userId,
      },
      errorMsg: "",
    };
  },
  props: ["decks"],
  created() {
    this.retrieveDecks();
  },
  methods: {
    retrieveDecks() {
      //we need to look at this one!
      const userId = this.$store.state.user.userId;
      //console.log(this.$store.state.user.userId);
      this.$store.commit("SET_DECKS", []); //reset before pulling decks
      deckService
        .getUserDecks(userId)
        .then((response) => {
          console.log(response.data);
          this.$store.commit("SET_DECKS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
    retrieveResults() {
      //we need to look at this one!
      cardService
        .getAuthenticatedCardSearchResults(this.userId, this.searchInput)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_CARDS", response.data);
            this.$router.push({
              path: `/${this.userId}/search/${this.searchInput}`,
            });
          }
        })
        .catch((error) => {
          alert(error);
        });
    },
    submitForm() {
      const tempDeck = {
        name: this.newDeck.name,
        description: this.newDeck.description,
        deckKeywords: this.newDeck.deckKeywords,
        isPublic: this.newDeck.isPublic,
        dateMade: this.newDeck.dateMade,
        // creator: this.newDeck.creator,
        creatorId: this.$store.state.user.userId,
      };
      console.log(tempDeck);

      this.showAddDeck = false; // take away the form so the user can't click the 'save' button 823,492 times while waiting for the Promise to resolve
      deckService
        .AddDeck(tempDeck)
        .then(() => {
          console.log(tempDeck.name);
          console.log(tempDeck.description);
          console.log(tempDeck.isPublic);
          console.log(tempDeck.deckKeywords);
          console.log(tempDeck.dateMade);
          console.log(tempDeck.creatorId);
          this.retrieveDecks();
          //reset new deck object
          this.newDeck = {
            name: "",
            description: "",
            deckId: 0,
            deckKeywords: "",
            creator: "",
            dateMade: Date,
            isPublic: false,
            creatorId: this.$store.state.user.userId,
          };
        })
        .catch((error) => {
          if (error.response) {
            //if the error object has a response, I know I made it to the server
            this.errorMsg =
              "Error adding a new deck, response received from the server was " +
              error.response.statusText +
              ".";
          } else if (error.request) {
            //we never got a response, so there was a problem reaching the server
            this.errorMsg =
              "Error adding a new deck, could not reach the server.";
          } else {
            //no request, no response, something has gone terribly wrong in the application
            this.errorMsg =
              "Error adding a new deck, request could not be created.";
          }
        });
    },

    BackgroundColor() {
      return "#203159";
    },
    // generateHexCode() {
    //   var bg = Math.floor(Math.random() * 16777215).toString(16);
    //   if (bg.length !== 6) bg = this.generateHexCode();
    //   return bg;
    // },
  },
  computed: {
    filterMyDecksOnly() {
      let myDecksOnly = this.$store.state.decks.filter((deck) => {
        return deck.creatorId == this.userId; // this.$store.state.user.userId;
      });
      myDecksOnly.forEach((myDeck) => {
        console.log(`mydecksonly ${myDeck.deckId}`);
      });

      return myDecksOnly;
    },
    filterPublicOnly() {
      const publicDecks = this.$store.state.decks.filter((d) => {
        return d.isPublic && d.creatorId != this.userId; // this.$store.state.user.userId;
      });
      publicDecks.forEach((publicDeck) => {
        console.log(`public ${publicDeck.deckId}`);
      });
      return publicDecks;
    },
  },
};
</script>
<!-- Search bar HTML & CSS from https://uiverse.io/alexruix/slippery-frog-10-->
<style scoped>
#Nav {
  align-items: center;
  height: 100%;
  width: 20%;
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  padding-top: 20px;
  padding-bottom: 20px;
  overflow-x: hidden;
  border-right: solid rgb(224, 42, 42) 1px;
}

h1 {
  text-align: center;
}
.displayDecks {
  display: flex;
  margin: 20px;
}
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
/* .search-bar {
  display: flex;
  align-items: center;
  float: right;
} */

.eachDeck {
  text-align: center;
  align-items: center;
  color: rgb(32, 49, 89);
  border-style: solid;
  border-radius: 10px;
  border-width: 2px;
  border-color: black;

  background-color: #fff;
  font-size: 16px;
  width: 90%;
  margin: 10px;
  padding: 20px;
  margin-bottom: 35px;
  cursor: pointer;
}
.addDeck {
  align-items: center;
  color: rgb(32, 49, 89);
  border-radius: 10px;
  background-color: #fff;
  font-size: 16px;
  width: 90%;
  margin: 10px;
  padding: 20px;
  margin-bottom: 35px;
  margin-top: 50px;
  cursor: pointer;
}
.form-control {
  margin-bottom: 10px;
}

.deck:hover:not(.router-link-active),
.addDeck:hover {
  font-weight: bold;
}
.decks:hover {
  font-weight: bold;
}
h2 {
  color: black;
}
div.displayDecks > a {
  width: 300px;
}
div.displayDecks > div {
  width: 320px;
}
div.displayDecks {
  align-items: center;
}
</style>
