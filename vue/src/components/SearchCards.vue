<template>
  <section class="search">
    <div v-for="card in searchCards" v-bind:key="card.cardId">
      <section>
        <single-card-display
          v-bind:card="card"
          v-bind:key="card.cardId"
        />
      </section>
      <p>Card {{card.cardId}} &nbsp; Deck {{card.deckId}}</p>
      <p>Tags:  {{card.cardKeywords}}</p>
    </div>
  </section>
</template>

<script>
import cardService from "../services/CardService.js";
import SingleCardDisplay from "./SingleCardDisplay.vue";

export default {
  name: "SearchCards",
  props: ["cards"],
  computed: {
    searchCards() {
      return this.$store.state.cards.filter((card) => {
        return card;
      });
    }
  },
  created() {
    this.retrieveResults();
    console.log('line 33');
  },
  methods: {
    retrieveResults() {
      //we need to look at this one!
      cardService
        .getSearchResults(this.$route.query.searchInput)
        .then((response) => {
          this.$store.commit("SET_SEARCH_RESULT", response.data);
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
  components: {
    SingleCardDisplay,
  },
};
</script>

<style>
.search {
    display: flex;
    justify-content: space-evenly;
    flex-wrap: wrap;
}

p {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}
</style>