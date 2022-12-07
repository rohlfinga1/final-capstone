<template>
  <div>
      <table>
          <tr>
              <th>Front</th>
              <th>Back</th>
              <th>Keywords</th>
              <th>Edit</th>
          </tr>
          <tr class="card-row" v-for="(value, key) in $store.state.cards"
              v-bind:key="key" :card="value">
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
export default {
    name: 'edit-deck',
    data() {
        return {
            card: {}
        }
    },
    methods: {
    
    }
}

</script>

<style>

</style>