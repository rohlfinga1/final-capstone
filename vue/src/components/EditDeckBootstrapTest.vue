<template>
  <div>
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
          <button
            v-on:click="viewEditCard(card.cardId)"
            class="btn editCard">
            Edit Card
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
      deck: {
        name: "",
        description: "",
        deckId: this.$route.params.deckId, // right now, GetCards() is taking this number literally,
        // but we want it to auto-increment
        deckKeywords: "",
        creator: "",
        creatorId: 0,
        deckDate: "",
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
    getSingleDeck(deckId) {
      deckService
        .getDeck(deckId)
        .then((response) => {
          this.$store.commit("SET_DECK", response.data);
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
        cardId: this.card.cardId
      }
      cardService.updateCard(updateCard)
        .then((response)=> {
          if (response.status == 201){
            this.$router.go();
          }
        })
         .catch(error => {
            
            this.handleErrorResponse(error, "editing");
          });
    }
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