<template>
  <div class="home">
    <menu-bar/>
    <h1>Home</h1>
    <p>You must be authenticated to see this</p>
  </div>
</template>

<script>
import MenuBar from '../components/MenuBar.vue';
export default {
  components: { MenuBar },
  name: "home"
};
</script>
