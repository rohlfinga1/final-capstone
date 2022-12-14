<template>
  <div>
      <h3>Select a deck to which you can add this card</h3>
      <join-card-and-deck/>
  </div>
</template>

<script>
import JoinCardAndDeck from '../components/JoinCardAndDeck.vue'
export default {
  name: 'add-card-to-deck',
  components: { JoinCardAndDeck },
}
</script>

<style>

</style>