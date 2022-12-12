<template>
  <div id="MyDecksPage">
    <div class="Nav">
      <h1>My Decks</h1>
      </div>
      <button class="addDeck" v-on:click="showAddDeck = !showAddDeck">
        Add New Deck
      </button>
      
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
      <h2>My Decks</h2>
      <div id="myCreated"
        class="decks"
        v-for="deck in filterMyDecksOnly"
        v-bind:key="deck.id"
        v-bind:style="{ 'background-color': deck.backgroundColor }"
      >
        <p class="eachDeck">
          {{ deck.name }}<br /><br />
          {{ deck.description }}<br />
          {{ deck.creatorId }}
        </p>
      
      </div >
      <h2>Public Decks</h2>
            <div id="publicDecks"
        class="decks"
         
        v-for="deck in filterPublicOnly"
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
    this.retrieveDecks();
  },
  methods: {
    retrieveDecks() {
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
    
    submitForm() {
      const tempDeck = {
        name: this.newDeck.name,
        description: this.newDeck.description,
        deckKeywords: this.newDeck.deckKeywords,
        isPublic: this.newDeck.isPublic,
        creatorId: this.$store.state.user.userId,
      };
      console.log(tempDeck);

      this.showAddDeck = false; // take away the form so the user can't click the 'save' button 823,492 times while waiting for the Promise to resolve
      deckCardService
        .AddDeck(tempDeck)
        .then(() => {
          this.retrieveDecks();
          //reset new deck object
          this.newDeck = {
            name: "",
            description: "",
            deckKeywords: "",
            deckId: "0",
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
    randomBackgroundColor() {
      return "#" + this.generateHexCode();
    },
    generateHexCode() {
      var bg = Math.floor(Math.random() * 16777215).toString(16);
      if (bg.length !== 6) bg = this.generateHexCode();
      return bg;
    },
  },
  computed:{
    filterPublicOnly() {
      const publicDecks = this.$store.state.decks.filter(d => {
        return (d.isPublic && (d.creatorId != this.$store.state.user.userId))
      });
      
      return publicDecks;
    },
    filterMyDecksOnly() {
      const myDecksOnly = this.$store.state.decks.filter(d => {
        return d.creatorId == this.$store.state.user.userId
      });
      
      return myDecksOnly;
    },
  }
  // filteredDecks() {
  // let tempDecks = this.$store.state.decks

  // tempDecks = tempDecks.filter((deck) => {
  // return (deck.creatorId == this.deck.creatorId)
  // })

  // return tempDecks;
  // }
  // }
};
</script>

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
</style>
