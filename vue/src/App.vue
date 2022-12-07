<template>
  <div id="app">
    <menu-bar/>
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <menu-bar></menu-bar>
    </div>
    <router-view />
  </div>
</template>

<script>
import MenuBar from './components/MenuBar.vue';

export default {
  components: {
    MenuBar
  }
};
</script>

<style>

</style>