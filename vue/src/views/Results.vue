<template>
  <div>
    <h2>Cards</h2>
    <cards-display/>
  </div>
</template>

<script>
import CardsDisplay from '../components/CardsDisplay.vue'

export default {
  components: { CardsDisplay },

}
</script>

<style>
h2 {
  text-align: center;
  font-family: 'Courier New', Courier, monospace;
}
</style>