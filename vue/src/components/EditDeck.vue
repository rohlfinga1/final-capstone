<template>
  <div>
      <button class="add-btn" @click="() => TogglePopup('buttonTrigger')">Add Card</button>
      <popup
          v-if="popupTriggers.buttonTrigger"
          :TogglePopup="() => TogglePopup('buttonTrigger')">
            <h2>epic popup</h2>
      </popup>
      <table>  
          <tr>
              <th>Front</th>
              <th>Back</th>
              <th>Keywords</th>
              <th>Edit</th>
          </tr>
          <tr class="card-row" v-for="card in this.$store.state.cards"
              v-bind:key="card.cardId">
              <!-- For every card in the textcard table by deck_id, assign each data point to the appropriate column -->
              <td>{{card.front}}</td>
              <td>{{card.back}}</td>
              <td>{{card.keywords}}</td>
              <td><button class="edit-btn">Edit Card</button></td>
          </tr>
      </table>
  </div>
</template>

<script>
import DeckCardService from '../services/DeckCardService.js'
import { ref } from 'vue';
import Popup from '../components/Popup.vue'

export default {
    name: 'edit-deck',
    props: ['cards'],
    setup() {
        const popupTriggers = ref({
                buttonTrigger: false
        });
        const TogglePopup = (trigger) => {
            popupTriggers.value[trigger] = !popupTriggers.value[trigger]
        }
        return {
            Popup, popupTriggers, TogglePopup
        }
    },
    data() {
        return {
            card: {
                cardId: 0,
                deckId: 0, // right now, GetCards() is taking this number literally,
                            // but we want it to auto-increment
                front: '',
                back: '',
                keywords: ''
            }
        }
    },
    methods: {
        GetCards() {
            DeckCardService.getCards(this.card.deckId).then((response) => {
                 this.$store.commit("SET_CARDS", response.data);
            })
            .catch((error) => {
                alert(error);
            })
        }
    },
    components: {
        Popup
    },
    created() {
        this.GetCards();
        // hard coded deck #2 for testing purposes

        // THIS IS NOT WORKING >:(

        // DeckCardService.getCards(2).then((response) => {
        //     this.$store.commit("SET_DECKS", response.data);
        // })
        // .catch(error => {
        //     alert(error);
        // });
    }
}

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