<template>
<div>
<my-decks/>
</div>
</template>

<script>
import MyDecks from '../components/MyDecks.vue';
import DeckCardService from '../services/DeckCardService';

export default {
  components: { MyDecks },
 
 created() {
     DeckCardService.GetDecks().then((response) => {this.$store.commit('SET_DECKS', response.data);
     })
     .catch(error => {
         alert("Error Obtaining List of Decks" + error);
     });
     }
}
</script>

<style>

</style>