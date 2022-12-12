<template>
  <div>
    <h2>Search Results</h2>
    <search-cards/>
  </div>
</template>

<script>
import SearchCards from '../components/SearchCards.vue'

export default {
  components: { SearchCards },

}
</script>

<style>
h2 {
  text-align: center;
  font-family: 'Courier New', Courier, monospace;
}
</style>