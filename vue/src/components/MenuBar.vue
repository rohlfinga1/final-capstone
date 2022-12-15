<template>
  <header id="menubar">
    <div class="title">
      <h2>Crash Cards</h2>
    </div>
    <nav>
      <ul>
        <!--<li><router-link v-bind:to="{ name: 'login' }" v-if="token==''">LOGIN</router-link></li> possibly switch v-bind for v-model   @click="processResults"-->
        <li>
          <router-link v-bind:to="{ name: 'home' }" class="link"
            >HOME</router-link
          >
        </li>
        <li>
          <router-link v-bind:to="{ name: 'account-page' }" class="link"
            >ACCOUNT</router-link
          >
        </li>
        <!--<li>
          <router-link v-bind:to="{ name: 'StudySession' }" class="link"
            >STUDY</router-link
          >
        </li>-->
        <li>
          <router-link v-bind:to="{ name: 'logout' }" class="link"
            >LOG OUT</router-link
          >
        </li>
      </ul>
    </nav>
  </header>
</template>

<script>
export default {
  name: "menu-bar",
};
</script>

<style>
header {
  font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto,
    "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji",
    "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
  background-color: #495579;
  display: flex;
  align-items: center;
  justify-content: space-between;
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  flex-grow: 1;
}
.title {
  align-items: center;
  color: #FFFDEB;
  margin-left: 20px;
}

header div {
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
}
header nav {
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  flex-grow: 1;
  justify-content: flex-end;
}
header nav ul {
  justify-content: space-evenly;
  flex-grow: 0.5;
  justify-items: center;
  align-items: center;
}
/* #header nav ul li a 
{
    display: flex;
    flex-direction: row;
    justify-content: space-evenly;
    text-decoration:  none;

} */

nav ul {
  display: inline;
  margin: 0;
  padding: 20px 0 0 0;
  display: flex;
  flex-direction: row;
}

nav ul li {
  list-style: none;
  width: 30%;
  border-radius: 5px;
  text-align: center;
  line-height: 50px;
  text-decoration: none;
  font-size: 1.2rem;
  color: #FFFDEB;
}

.link {
  text-decoration: none;
  font-size: 1.2rem;
  color: #FFFDEB;
}

.btn {
  text-emphasis: none;
  background: white;
  color: #03446a;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}
h2 {
  font-family: "Arial Narrow Bold", sans-serif;
  text-align: center;
  font-style: italic;
  color: #FFFDEB;
}
</style>