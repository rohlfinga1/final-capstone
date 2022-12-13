<template>
  <div class="home">

    <h1>Home</h1>
    <p>Study APP</p>
        <!-- <li>
          <router-link v-bind:to="{ name: 'Home' }" class="link"
            >HOME</router-link
          >
        </li> make route to go to all public cards-->
    <public-decks/>
    
    
    <router-link to="Login"> Let's Log In! </router-link>
  </div>
</template>

<script>
import PublicDecks from '../components/PublicDecks.vue';


export default {
  components: { PublicDecks},

  name: "home"
};
</script>

<style>
</style>