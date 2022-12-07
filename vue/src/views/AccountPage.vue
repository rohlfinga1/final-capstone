<template>
<div>
<myDecks/>
</div>
</template>

<script>
import MyDecks from '../components/MyDecks.vue';
import DeckCardService from '../services/DeckCardService.js';

export default {
  components: { MyDecks },
 data() {
  return {
    
    showAddDeck: false,
    newDeck: {
      name: '',
      description: '',
      deckKeywords: '',
      deckId: '0'
    },
    errorMsg: ''
  };
},
 created() {
     DeckCardService.GetAllDecks().then((response) => {this.$store.commit('SET_DECKS', response.data);
     })
     .catch(error => {
         alert("Error Obtaining List of Decks" + error);
     });
     }
}
</script>

<style>

</style>