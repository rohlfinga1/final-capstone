<template>
  <div>
    <h1 id="title">Edit Card</h1>
    <edit-card-form v-bind:cardID="parseInt($route.params.cardID)" />
  </div>
</template>

<script>
import EditCardForm from "../components/EditCardForm";

export default {
  components: {
    EditCardForm
  }
};
</script>
<style>
#title{
  font-family: sans-serif;
}
</style>