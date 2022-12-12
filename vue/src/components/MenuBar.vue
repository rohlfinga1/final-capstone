<template>
  <header id="menubar">
    <div class="title">
      <h2>FlashCard Study App</h2>
    </div>
    <nav>
      <ul>
        <li>
          <div>
            <form @submit.prevent="processResults">
              <input
                id="front"
                type="text"
                name="keyword"
                placeholder="Search"
                v-model="searchInput"
              />
              <router-link
                v-bind:to="{
                  name: 'results',
                  query: { searchInput: searchInput },
                }"
                class="link"
                ><button class="btn btn-submit">Search</button></router-link
              >
            </form>
          </div>
        </li>
        <!--<li><router-link v-bind:to="{ name: 'login' }" v-if="token==''">LOGIN</router-link></li> possibly switch v-bind for v-model   @click="processResults"-->
        <li>
          <router-link v-bind:to="{ name: 'myDecks' }" class="link"
            >MY DECKS</router-link
          >
        </li>
        <li>
          <router-link v-bind:to="{ name: 'public-decks' }" class="link"
            >PUBLIC DECKS</router-link
          >
        </li>
        <li>
          <router-link v-bind:to="{ name: 'logout' }" class="link"
            >LOG OUT</router-link
          >
        </li>
        <!--v-else="token"-->
      </ul>
    </nav>
  </header>
</template>

<script>
//import deckCardService from "../services/DeckCardService.js";

export default {
  name: "menu-bar",
  data() {
    return {
      searchInput: "",
    };
  },
//   methods: {
//     processResults() {
//       const searchResult = this.searchInput;
//       this.$store.commit("SET_SEARCH_RESULTS", []);
//       deckCardService
//         .getSearchResults(searchResult)
//         .then((response) => {
//           console.log(response.data);
//           this.$store.commit("SET_SEARCH_RESULTS", response.data);
//         })
//         .catch((error) => {
//           alert(error);
//         });
//       this.route.replace({ name: "SearchCards", params: { searchResult } });
//     },
//   },
};
</script>

<style>
header {
  font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto,
    "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji",
    "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
  background-color: #508ca8;
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
}
</style>