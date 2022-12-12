<template>
  <div class="deckEditor">

    <h1>Deck Editor</h1>
    <edit-deck />
    <!--<edit-other />-->
  </div>
</template>

<script>
import EditDeck from '../components/EditDeckBootstrapTest.vue';
//import EditDeck from '../components/EditCardOverview.vue';

export default {
  components: { EditDeck},

};
</script>
