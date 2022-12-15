<template>
  <div id="app">
    <menu-bar></menu-bar>
    <!--<div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>-->
    <router-view />
    <the-footer/>
  </div>
</template>

<script>
import MenuBar from './components/MenuBar.vue';
import TheFooter from './components/TheFooter.vue';

export default {
  components: {
    MenuBar,
    TheFooter
  }
};
</script>

<style>
body {
  background-color: #eee;
}

</style>