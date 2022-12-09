<template>
  <div>
    <h2>Search Results</h2>
    <table>
      <tr>
        <th>Front</th>
        <th>Back</th>
        <th>Keywords</th>
        <th>Edit</th>
      </tr>
      <tr
        class="card-row"
        v-for="card in this.$store.state.searchResults"
        v-bind:key="card.cardId"
      >
        <!-- For every card in the textcard table by deck_id, assign each data point to the appropriate column -->
        <td>{{ card.front }}</td>
        <td>{{ card.back }}</td>
        <td>{{ card.cardKeywords }}</td>
      </tr>
    </table>

  </div>
</template>

<script>
export default {
  name: "SearchCards",
  props: ["searchResults"],
  components: {},
};
</script>

<style>
</style>
SingleCardDisplay