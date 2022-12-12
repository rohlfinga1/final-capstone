<template>
  <div>
    <button class="add-btn" @click="() => TogglePopup('buttonTrigger')">
      Add Card
    </button>
    <button class="edit-btn" @click="ShowEditForm = !ShowEditForm"> Edit Deck</button>  
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
        <td v-if="ShowEditForm == false">{{ card.front }}</td>
        <td v-if="ShowEditForm == false">{{ card.back }}</td>
        <td v-if="ShowEditForm == false">{{ card.cardKeywords }}</td>
        <td v-if="ShowEditForm">
          <form id="editForm" v-if="ShowEditForm" @submit.prevent="editForm">
            <input type="text" class="form-control" v-model="card.front" />
          </form>
        </td>
        <td v-if="ShowEditForm">
          <form id="editForm" v-if="ShowEditForm" @submit.prevent="editForm">
            <input type="text" class="form-control" v-model="card.back" />
          </form>
        </td>
        <td v-if="ShowEditForm">
          <form id="editForm" v-if="ShowEditForm" @submit.prevent="editForm">
            <input type="text" class="form-control" v-model="card.cardKeywords" />
          </form>
        </td>   
      </tr>
    </table>
    <button type="submit" form:id="editForm" class="edit-btn" v-if="ShowEditForm" @click="">Submit</button>
    <div class="popup">
      <popup
        v-if="popupTriggers.buttonTrigger"
        :TogglePopup="() => TogglePopup('buttonTrigger')"
      >
        <h3>Card Creator and Editor</h3>
        <card-form />
      </popup>
    </div>
  </div>
</template>

<script>
import DeckCardService from "../services/DeckCardService.js";
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
      ShowEditForm: false,
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
    this.GetCards();
  },
  methods: {
    GetCards() {
      DeckCardService.getCards(this.card.deckId)
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
      DeckCardService.updateCard(updateCard)
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
