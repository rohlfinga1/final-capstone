<template>
  <div>
      <table>
          <tr>
              <th>Front</th>
              <th>Back</th>
              <th>Keywords</th>
              <th>Edit</th>
          </tr>
          <tr v-for="(value, key) in textcard" v-bind:key="key">
              <!-- For every card in the textcard table by deck_id, assign each data point to the appropriate column -->
              <td>{{textcard.front}}</td>
              <td>{{textcard.front}}</td>
              <td>{{textcard.front}}</td>
              <td><button class="edit-btn">Edit Card</button></td>
          </tr>
      </table>
  </div>
</template>

<script>
export default {

}
</script>

<style>

</style>