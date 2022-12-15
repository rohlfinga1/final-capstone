<template>
  
  <div id="login" class="text-center">
     <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
        style="font-family: Arial"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <label for="username" class="sr-only"></label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />&nbsp;
      <label for="password" class="sr-only"></label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />&nbsp;&nbsp;
      <router-link :to="{ name: 'register' }" style="font-family: Arial">Need an account? </router-link>&nbsp;&nbsp;
      <!-- <button class="cssbuttons-io-button" type="submit" @click="login">Sign in</button>
        <div class="icon">
          <svg height="24" width="24" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path d="M0 0h24v24H0z" fill="none"></path><path d="M16.172 11l-5.364-5.364 1.414-1.414L20 12l-7.778 7.778-1.414-1.414L16.172 13H4v-2z" fill="currentColor"></path></svg>
        </div> -->
        <button class="signin-btn">Sign In</button>
      <!--  @click triggers login function on a click-->
      <!-- testable using default username = "user" password = "password" -->
      <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
      <br/><br/><br/><br/><br/><br/><br/><br/><br/>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200 || response.status == 201) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push({name: "account-page", params: {userId: response.data.user.userId}});//changed from "/mydecks" so our decks are visible once we log in on the first page
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style scoped>
/* From uiverse.io by @alexruix */
/* vvvvvvvvvvvvvvvvvvvvvvvvvvvv */
  .form-control {
  line-height: 28px;
  border: 2px solid transparent;
  border-bottom-color: #777;
  padding: .2rem 0;
  outline: none;
  background-color: transparent;
  color: #0d0c22;
  transition: .3s cubic-bezier(0.645, 0.045, 0.355, 1);
  }

  .input:focus, input:hover {
  outline: none;
  padding: .2rem 1rem;
  border-radius: 1rem;
  border-color: #7a9cc6;
  }

  .input::placeholder {
  color: #777;
  }

  .input:focus::placeholder {
  opacity: 0;
  transition: opacity .3s;
  }
/* ^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */

  .form-signin {
    display: flex;
    flex-direction: column;
    width: 400px;
  }
  .sr-only {
    margin-bottom: 30px;
  }
  .signin-btn {
    background-color: #203159;
    color: white;
    width: 175px;
    height: 40px;
    border-radius: 8px;
  }
  .signin-btn:hover {
    cursor: pointer;
    background-color: #495579;
  }

  #login {
    padding-left: 37%;
    border-style: ridge;
  }
</style>

<!-- #FFFDEB, #495579, #203159 -->