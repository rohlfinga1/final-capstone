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
        CreateCard() {
            DeckCardService.getCard(this.newCard.deck_id, this.newCard.card_id).then((response) => {
                 this.newCard.card_id = response.data;
            })
            .catch((error) => {
                alert(error);
            })
        }
    }
}

</script>

<style>

</style>