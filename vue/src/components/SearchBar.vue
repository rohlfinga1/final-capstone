<template>
  <div class="search-bar">
    <form @submit.prevent="retrieveResults">
      <select class="form-item" id="select-search" name="select-search">
        <option value="">--Please choose an option--</option>
        <option value="my-cards" v-bind:key="userId">my cards</option>
        <option value="public-cards">public cards</option>
      </select>
      <input
        class="form-item"
        id="front"
        type="text"
        name="keyword"
        placeholder="Search"
        v-model="searchInput"
      />
      <button class="form-item btn btn-submit" @click="retrieveResults">
        Search
      </button>
    </form>
  </div>
</template>

<script>
import cardService from "../services/CardService.js";

export default {
  name: "search-bar",
  data() {
    return {
      searchInput: "",
      userId: this.$store.state.user.userId
    };
  },
  created() {
    this.retrieveResults();
  },
  methods: {
    retrieveResults() {
      //we need to look at this one!
      if (this.userId != 0) {
        cardService
          .getAuthenticatedCardSearchResults(this.userId, this.searchInput)
          .then((response) => {
            if (response.status == 200) {
              this.$store.commit("SET_SEARCH_CARDS", response.data);
              this.$router.push({path: `/${this.userId}/cardsearch/${this.searchInput}`});
            }
          })
          .catch((error) => {
            alert(error);
          });
      } else {
        cardService
          .getPublicCardSearchResults(this.searchInput)
          .then((response) => {
            if (response.status == 200) {
              this.$store.commit("SET_SEARCH_CARDS", response.data);
              this.$router.push({path: `/cardsearch/${this.searchInput}`});
            }
          })
          .catch((error) => {
            alert(error);
          });
      }
    },
  },
};
</script>

<style>
form {
  display: flex;
  align-items: center;
  float: right;
}

.form-item {
  margin: 10px;
}
</style>