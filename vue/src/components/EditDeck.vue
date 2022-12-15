<template>
  <div>
    <h2 id="deck-name">{{ deck.name }}<br /></h2>
    <p>{{ deck.description }}<br /></p>

     <form id="form" v-if="ShowForm" @submit.prevent="UpdateDeck">
      <label class="labels" for="deckName">Deck Name:</label><br/>
      <input id="deckName" type="text" class="form-control, input-field" v-model="deck.name" /><br/><br/>
      <label class="labels" for="description">Description:</label><br/>
      <input id="description" type="text" class="form-control, input-field" v-model="deck.description" /><br/><br/>
      <label class="labels" for="deckKeywords">Deck Keywords:</label><br/>
      <input id="deckKeywords" type="text" class="form-control, input-field" v-model="deck.deckKeywords" /><br/><br/>
      <label class="labels" for="isPublic">Is this Public:</label>
      <input id="isPublic" type="checkbox" class="form-control" v-model="deck.isPublic" /><br/><br/>
      <button class="btn-btn">Save</button>
      <button class="btn-btn" v-on:click="showAddDeck = !showAddDeck"> Cancel </button>
    </form>

    <button class="add-btn" id="study-button" @click="study(deck.deckId)">
      Study Now
    </button>

    <button class="add-btn" id="edit-deck-button" @click="ShowForm = !ShowForm" v-if="viewDeleteButton(deck.creatorId)">
      Edit Deck Details
    </button>


    <button class="add-btn" id="add-card-button" @click="() => TogglePopup('buttonTrigger')" v-if="viewDeleteButton(deck.creatorId)">
      Add Card
    </button>
    <div class="popup">
      <popup
        v-if="popupTriggers.buttonTrigger"
        :TogglePopup="() => TogglePopup('buttonTrigger')"
      >
        <h3>Add Card</h3>
      </popup>
    </div>
    <div id="table-div">
    <table>
      <tr class="table-head">
        <th>Front</th>
        <th>Back</th>
        <th>Keywords</th>
        <th>Edit/Delete Card</th>
      </tr>
      <tr
        class="card-row"
        v-for="card in this.$store.state.cards"
        v-bind:key="card.cardId"
      >
        <td>{{ card.front }}</td>
        <td>{{ card.back }}</td>
        <td>{{ card.cardKeywords }}</td>
        <td>
          <button class="add-btn" v-on:click="viewEditCard(card.cardId)" v-if="viewEditButton(card.creatorId)">
            Edit Card
          </button>
          <br/>
          <button class="add-btn" v-on:click="deleteCard(card.cardId)"  v-if="viewDeleteButton(deck.creatorId)">
            Delete From Deck
          </button>
           <br/>
        </td>
      </tr>
    </table>
    </div>
    <div class="popup">
      <popup
        v-if="popupTriggers.buttonTrigger"
        :TogglePopup="() => TogglePopup('buttonTrigger')"
      >
        <h3 class="cardform-title">Card Creator</h3>
        <card-form />
      </popup>
    </div>
  </div>
</template>

<script>
import deckService from "../services/DeckService.js";
import cardService from "../services/CardService.js";
import { ref } from "vue";
import Popup from "./Popup.vue";
import CardForm from "./CardForm.vue";
import CardDeckIdService from '../services/CardDeckIdService.js';

export default {
  name: "edit-deck",
  props: ["cards"],
  setup() {
    const popupTriggers = ref({
      buttonTrigger: false,
    });
    const TogglePopup = (trigger) => {
      popupTriggers.value[trigger] = !popupTriggers.value[trigger];
    };
    return {
      Popup,
      popupTriggers,
      TogglePopup,
    };
  },
  data() {
    return {
      ShowForm: false,
      deck: {
        name: "",
        description: "",
        deckId: this.$route.params.deckId,
        deckKeywords: "",
        creatorId: "",
        dateMade: "",
        isPublic: false,
      },
      card: {
        cardId: 0,
        deckId: this.$route.params.deckId, 
        front: "",
        back: "",
        cardKeywords: "",
      },
    };
  },
  created() {
    this.getSingleDeck(this.deck.deckId);
    this.GetCards(this.deck.deckId);
  },
  methods: {
    study(deckId) {
      this.$router.push({name: 'StudySession', params: {deckId}});
    },
    viewEditCard(cardId) {
      this.$router.push(`/card/${cardId}`);
    },
    viewEditButton(creatorId){
      if(creatorId == this.$store.state.user.userId){
        return true;
      }
      else{
        return false;
      }
    },
    viewDeleteButton(creatorId){
      if(creatorId == this.$store.state.user.userId){
        return true;
      }
      else{
        return false;
      }
    },
    getSingleDeck(deckId) {
      deckService
        .getDeck(deckId)
        .then((response) => {
          this.$store.commit("SET_CURRENT_DECK", response.data);
          this.deck = this.$store.state.deck;
        })
        .catch((error) => {
          alert(error);
        });
    },
    GetCards(deckId) {
      cardService
        .getCardsByDeckId(deckId)
        .then((response) => {
          this.$store.commit("SET_CARDS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
    UpdateCards() {
      const updateCard = {
        front: this.card.front,
        back: this.card.back,
        cardKeywords: this.card.cardKeywords,
        deckId: this.card.deckId,
        cardId: this.card.cardId,
      };
      cardService
        .updateCard(updateCard)
        .then((response) => {
          if (response.status == 201) {
            this.$router.go();
            this.ShowForm = false;
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "editing");
        });
    },
    UpdateDeck() {
      const updatedDeck = {
        name: this.deck.name,
        description: this.deck.description,
        deckId: this.deck.deckId,
        deckKeywords: this.deck.deckKeywords,
        dateMade: this.deck.dateMade,
        creatorId: this.deck.creatorId,
        isPublic: this.deck.isPublic,
      };
      deckService
        .updateDeck(updatedDeck.deckId, updatedDeck)
        .then((response) => {
          if (response.status === 200 ||
              response.status === 201 ||
              response.status === 202) {
            this.$router.go();
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "updating");
        });
    },
    deleteCard(cardId) {
      CardDeckIdService
      .deleteCardFromDeck(this.$route.params.deckId, cardId).then((response) => {
        if (response.status === 200 ||
            response.status === 201 ||
            response.status === 202 ||
            response.status === 203 ||
            response.status === 204 ||
            response.status === 205) {
          this.$router.go();
        }
      })
      .catch((error) => {
          this.handleErrorResponse(error, "deleting");
        });
    },

    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " card. Response received was '" +
          error.response.statusText;
        ("'.");
      } else if (error.request) {
        this.errorMsg = "Error " + verb + " card. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " card. Request could not be created.";
      }
    },
  },

  components: {
    Popup,
    CardForm,
  },
};
</script>

<style>
#deck-name {
  color: black;
}
table {
  table-layout: auto;
  width: 100%;
  border-collapse: collapse;
  border: 3px solid gray;
  text-align: center;
  background-color: white;
  column-rule-width: 3px;
  column-rule-color: gray;
  column-rule-style: solid;
}
tr:nth-child(even) {
  background-color: #e9e9e9;
  border: 3px solid gray;
}

.edit-btn {
  background-color: #495579;
  margin: 10px;
  /* border-style: solid, black; */
}
.edit-btn:hover {
  background-color: whitesmoke;
}
#form {
font-family: sans-serif;
align-content: center;
width: 20%;
margin-left: 40%;
margin-right:40%;
text-align: center;
background-color: white;
padding: 5px;
border-radius: 20px;
box-shadow: rgba(73,85,121,0.5) 5px 3px,
 rgba(73,85,121,0.4) 10px 6px,
 rgba(73,85,121,0.3) 15px 9px;
}
.input-field{
  width:90%;
}
.labels{
  font-family: sans-serif;
  font-weight: bold; 
}
.add-btn {
  background-color: #495579;
  border-style: solid, black;
  height: 50px;
  width: 200px;
  font-size: 20px;
  border-radius: 10px;
  margin-bottom: 10px;
  margin-right:15px;
  margin-left:15px;
  color:#FFFDEB;
}
.add-btn:hover {
  background-color: rgb(190, 209, 233);
  color: black;
}
.btn-btn {
   background-color: #495579;
   border-style: solid, black;
   height: 30px;
   width: 100px;
   font-size: 20px;
   border-radius: 10px;
   margin-bottom: 10px;
   margin-right:15px;
   margin-left:15px;
   color:#FFFDEB;
}
.btn-btn:hover {
  background-color: rgb(190, 209, 233);
  color: black;
}
td {
  font-family: sans-serif;
  font-size: 120%;
  word-wrap: break-word;
  border: 3px solid gray;
  padding:15px;
}
th{
  font-family: sans-serif;
  border: 3px solid gray;
}
.table-head{
  background-color: #495579;
  color: #FFFDEB;
  height: 50px;
  font-size: 170%;
}
.cardform-title{
  font-family: sans-serif;
}
</style>