<template>
  <header id="menubar">
      <div class="title">
          <h2>FlashCard Study App</h2>
      </div>
    <div class= "dropdown_menu">
        <select name="nav_dropdown" id="nav_dropdown" class="nav_dropdown">
            <option value="login"><router-link v-bind:to="{ name: 'login' }">Login</router-link></option>
            <option value="login"><router-link v-bind:to="{ name: 'myDecks' }">My Decks</router-link></option>
            <option value="login"><router-link v-bind:to="{ name: 'public-decks' }">Public Decks</router-link></option>
        </select>
    </div>
  </header>
</template>

<script>

export default {
    name: "menu-bar"
}
</script>

<style>

header {
    font-family: system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto,
    'Helvetica Neue', Arial, 'Noto Sans', sans-serif, 'Apple Color Emoji',
    'Segoe UI Emoji', 'Segoe UI Symbol', 'Noto Color Emoji';
    background-color: #508ca8;
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.dropdown_menu {
    align-items: right;
}

.title {
    align-items: center;
}
</style>