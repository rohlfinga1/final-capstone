<template>
  <div>
    <h2>Public Cards Search Results</h2>
    <cards-display/>
  </div>
</template>

<script>
import CardsDisplay from '../components/CardsDisplay.vue'
import cardService from '../services/CardService'

export default {
  components: { CardsDisplay },
  methods: {
      searchPublicCards() {
        cardService.getPublicCardSearchResults(this.$route.params.searchInput)
        .then((response) => {
          this.$store.commit("SET_CARDS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
      }
    }
}
</script>

<style>
h2 {
  text-align: center;
  font-family: 'Courier New', Courier, monospace;
}
</style>