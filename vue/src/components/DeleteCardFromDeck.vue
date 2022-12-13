<template>
  <div v-show="hideForm == true">
    <form v-on:submit="hideForm">
      <div v-for="deck in selectedDecks" v-bind:key="deck.deckId">
        <button @click="selectThisDeckForDeleting(deck.deckId)">
          {{ deck.name }}
        </button>
      </div>
      <button class="btn btn-submit">Done</button>
    </form>
  </div>
</template>

<script>
// import cardService from "../services/CardService.js";

export default {
  name: "delete-card-from-deck",
  computed: {
    selectedDecks() {
      return this.$store.state.decks.filter((deck) => {
        return deck;
      });
    },
  },
  methods: {
    
  },
};
</script>

<style>
</style>