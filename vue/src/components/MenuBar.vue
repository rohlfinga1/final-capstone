<template>
  <header id="menubar">
      <div class="title">
          Page Title
      </div>
    <div class= "dropdown_menu">
       <b-dropdown id="nav_dropdown" text="menu" class="nav_dropdown">
            <b-dropdown-item href="#">Login</b-dropdown-item>
            <b-dropdown-item href="#">My Decks</b-dropdown-item>
            <b-dropdown-item href="#">Public Decks</b-dropdown-item>
       </b-dropdown>
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
}
.dropdown_menu {
    align-self: right;
}

.title {
    align-self: center;
}
</style>