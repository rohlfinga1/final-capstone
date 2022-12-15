<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" style="font-family: Arial" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <label for="username" class="sr-only"></label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      <label for="password" class="sr-only"></label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      <input
        type="password"
        id="confirmPassword"
        class="form-control sr-only"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"
        required
      />
      <router-link :to="{ name: 'login' }" style="font-family: Arial">Need an account?</router-link>
      <button id="create-btn" class="btn btn-lg btn-primary btn-block" type="submit" @click="register" > <!-- Add Register functionality using @click to call register method-->
        Create Account
      </button>
      <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
      <br/><br/><br/><br/>
    </form>
  </div>
</template>

<script scoped>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      //call clearError function defined below will set current errors back to default
      this.clearErrors();

      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style>
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
  .form-register {
    display: flex;
    flex-direction: column;
    width: 400px;
  }
  .sr-only {
    margin-bottom: 30px;
  }
  #create-btn {
    margin-top: 30px;
    background-color: #203159;
    color: white;
    width: 175px;
    height: 40px;
    border-radius: 8px;
  }
  #create-btn:hover {
    cursor: pointer;
    background-color: #495579;
  }
  #confirmPassword {
    margin-top: 30px;
  }
  #register {
    padding-left: 37%;
    border-style: ridge;
  }
</style>
