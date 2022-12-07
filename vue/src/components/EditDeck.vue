<template>
  <div>
      <table>
          <tr>
              <th>Front</th>
              <th>Back</th>
              <th>Keywords</th>
              <th>Edit</th>
          </tr>
          <tr class="card-row" v-on:created="CreateCard" v-for="(value, key) in $store.state.cards"
              v-bind:key="key" :card="value">
              <!-- For every card in the textcard table by deck_id, assign each data point to the appropriate column -->
              <td>{{newCard.front}}</td>
              <td>{{newCard.back}}</td>
              <td>{{newCard.keywords}}</td>
              <td><button class="edit-btn">Edit Card</button></td>
          </tr>
      </table>
  </div>
</template>

<script>
import DeckCardService from '../services/DeckCardService'
export default {
    name: 'edit-deck',
    props: ['card'],
    data() {
        return {
            newCard: {}
        }
    },
    methods: {
        GetCards() {
            DeckCardService.getCards(this.newCard.deck_id).then((response) => {
                 this.newCard.card_id = response.data;
            })
            .catch((error) => {
                alert(error);
            })
        }
    },
    components: {

    },
    created() {
        // hard coded deck #2 for testing purposes
        DeckCardService.getCards(2).then((response) => {
            this.$store.commit("SET_DECKS", response.data);
        })
        .catch(error => {
            alert(error);
        });
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
        background-color: green;
        border-style: solid, black;
    }
</style>