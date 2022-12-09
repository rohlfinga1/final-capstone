<template>
  <div class="home">

    <h1>Home</h1>
    <p>Study APP</p>
    <public-decks/>
    
    <router-link to="Login"> Let's Log In! </router-link>
  </div>
</template>

<script>
import PublicDecks from '../components/PublicDecks.vue';

export default {
  components: { PublicDecks },

  name: "home"
};
</script>
