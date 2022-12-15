<template>
  <div>
    <h1>My Cards Results</h1>
    <div class="layout">
      <my-cards-search-bar/>
      <cards-display />
    </div>
  </div>
</template>

<script>
import cardService from '../services/CardService.js'
import CardsDisplay from '../components/CardsDisplay.vue'
import MyCardsSearchBar from '../components/MyCardsSearchBar.vue'

export default {
  name: "auth-card-search",
  data() {
    return {
      userId: this.$store.state.user.userId,
      searchInput: this.$route.params.input
    }
  },
  props: ["cards"],
  components: { CardsDisplay, MyCardsSearchBar },
  created() {
    this.privateCardSearch(this.userId, this.searchInput);
    console.log(`${this.userId}`);
    console.log(`${this.searchInput}`);
  },
  methods: {
    privateCardSearch(userId, searchInput) {
        cardService.getAuthenticatedCardSearchResults(userId, searchInput)
        .then((response) => {
          this.$store.commit("SET_CARDS", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>

<style>
h1 {
  text-align: center;
  font-family: "Courier New", Courier, monospace;
}
.layout {
  display: flex;
  flex-direction: column;
}
</style>