<template>
  <div>
    <h2>{{ deck.name }}<br /></h2>
    <p>{{ deck.description }}<br /></p>

    <button class="add-btn" @click="ShowForm = !ShowForm">
      Edit This Deck
    </button>

    <form v-if="ShowForm" @submit.prevent="UpdateDeck">
      Deck Name:
      <input type="text" class="form-control" v-model="deck.name" />
      Description:
      <input type="text" class="form-control" v-model="deck.description" />
      Deck Keywords:
      <input type="text" class="form-control" v-model="deck.deckKeywords" />
      Is this Public:
      <input type="checkbox" class="form-control" v-model="deck.isPublic" />
      <button class="btn btn-submit">Save</button>
      <button class="btn btn-cancel" v-on:click="showAddDeck = !showAddDeck">
        Cancel
      </button>
    </form>

    <button class="add-btn" @click="() => TogglePopup('buttonTrigger')">
      Add Card
    </button>
    <div class="popup">
      <popup
        v-if="popupTriggers.buttonTrigger"
        :TogglePopup="() => TogglePopup('buttonTrigger')"
      >
        <h3>Add Card</h3>
        <!--<card-form v-bind:cardID="parseInt($route.params.cardID)" />-->
      </popup>
    </div>
    <table>
      <tr>
        <th>Front</th>
        <th>Back</th>
        <th>Keywords</th>
      </tr>
      <tr
        class="card-row"
        v-for="card in this.$store.state.cards"
        v-bind:key="card.cardId"
      >
        <!-- For every card in the textcard table by deck_id, assign each data point to the appropriate column -->
        <td>{{ card.front }}</td>
        <td>{{ card.back }}</td>
        <td>{{ card.cardKeywords }}</td>
        <td>
          <button v-on:click="viewEditCard(card.cardId)" class="btn editCard" v-if="viewEditButton(card.creatorId)">
            Edit Card
          </button>
          <button v-on:click="deleteCard(card.cardId)" class="btn editCard">
            Delete Card From Deck
          </button>
        </td>
      </tr>
    </table>
    <div class="popup">
      <popup
        v-if="popupTriggers.buttonTrigger"
        :TogglePopup="() => TogglePopup('buttonTrigger')"
      >
        <h3>Card Creator</h3>
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
      viewButton: false,
      deck: {
        name: "",
        description: "",
        deckId: this.$route.params.deckId, // right now, GetCards() is taking this number literally,
        // but we want it to auto-increment
        deckKeywords: "",
        creator: "",
        creatorId: 0,
        dateMade: "",
        isPublic: false,
      },
      card: {
        cardId: 0,
        deckId: this.$route.params.deckId, // right now, GetCards() is taking this number literally,
        // but we want it to auto-increment
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
    viewEditCard(cardId) {
      this.$router.push(`/editcard/${cardId}`);
    },
    viewEditButton(creatorId){
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
table {
  align-self: center;
  justify-self: center;
  table-layout: fixed;
  width: 80%;
  border-collapse: collapse;
  border: 3px solid gray;
  text-align: center;
}
tbody tr:nth-child(odd) {
  background-color: #e9e9e9;
}
tbody tr:nth-child(even) {
  background-color: #979797;
}
.edit-btn {
  background-color: rgb(129, 230, 129);
  border-style: solid, black;
}
.edit-btn:hover {
  background-color: whitesmoke;
}

.add-btn {
  background-color: rgb(160, 190, 245);
  border-style: solid, black;
  height: 50px;
  width: 200px;
  font-size: 20px;
  border-radius: 10px;
  margin-bottom: 10px;
}
.add-btn:hover {
  background-color: whitesmoke;
}
</style>