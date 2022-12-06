<template>
 <div id= "MyDecksPage">
  <div class="Nav">
    <h1>My Decks</h1>
    
    
    <button class="btn addDeck"   v-on:click="showAddDeck = !showAddDeck">Add New Deck</button> 
    
  <div class="decks" v-for="deck in this.$store.state.decks"
        v-bind:key="deck.id"
        v-bind:style="{ 'background-color': board.
        backgroundColor }"
      
        {{ deck.name }}>
      
      <form v-if="showAddDeck">
        Deck Name:
        <input type="text" class="form-control" 
v-model="newDeck.name" />
        Description:
        <input type="text" class="form-control" 
v-model="newDeck.description" />
        Deck Keywords:
        <input type="text" class="form-control" v-model="newDeck.deckKeywords" />
        <button class="btn btn-submit" v-on:click.
prevent="saveNewDeck">Save</button>
        <button class="btn btn-cancel" v-on:click="showAddDeck 
= !showAddDeck">Cancel</button>
      </form>
  </div>
 </div>
</template>

<script>
import deckCardService from '../services/DeckCardService';

export default {
  data() {
    return {
      
      showAddDeck: false,
      newDeck: {
        name: '',
        description: '',
        deckKeywords: ''
      },
      errorMsg: ''
    };
  },
  created() {
    this.retrieveDecks();
  },
  
  methods: {
    retrieveDecks() { //we need to look at this one!
      deckCardService.getDecks().then(response => {
        this.$store.commit("SET_DECKS", response.data);
                            
    });
    },

    saveNewBoard() 
    {
        
        this.showAddDeck = false; // take away the form so the user can't click the 'save' button 823,492 times while waiting for the Promise to resolve
        deckCardService.addDeck(this.newDeck).then(() => {
            this.retrieveDecks();
            //reset new board object
            this.newDeck = {
              name: '',
              description:'',
              deckKeywords:''
            }

        }).catch((error) => {
          if(error.response){
            //if the error object has a response, I know I made it to the server
            this.errorMsg = 'Error adding a new deck, response received from the server was ' + error.response.statusText + '.';
          }


          else if(error.request) {
            //we never got a response, so there was a problem reaching the server
            this.errorMsg = 'Error adding a new deck, could not reach the server.';
          }
          else {
            //no request, no response, something has gone terribly wrong in the application
            this.errorMsg = 'Error adding a new deck, request could not be created.';
          }
          
        });
        
    },
    randomBackgroundColor() {
      return "#" + this.generateHexCode();
    },
    generateHexCode() {
      var bg = Math.floor(Math.random()*16777215).toString(16);
      if (bg.length !== 6) bg = this.generateHexCode();
      return bg;
    }
  }
};
</script>

<style scoped>
div#Nav {
  height: 100%;
  width: 20%;
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  padding-top: 20px;
  padding-bottom: 20px;
  overflow-x: hidden;
  border-right: solid lightgrey 1px;
}

h1 {
  text-align: center;
}

.decks {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}
.deck {
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
.addDeck {
  color: #f7fafc;
  border-radius: 10px;
  background-color: #28a745;
  font-size: 16px;
  width: 60%;
  margin: 10px;
  padding: 20px;
  cursor: pointer;
}
.form-control {
  margin-bottom: 10px;
}
.btn {margin-bottom: 35px;}
.loading {
  flex: 3;
}
.board:hover:not(.router-link-active), .addBoard:hover {
  font-weight: bold;
}
.board-active {
  font-weight: bold;
  border: solid blue 5px;
}
</style>
