import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import store from '../store/index'
import AccountPage from '../views/AccountPage.vue'
import DeckEditor from '../views/DeckEditor.vue'
import StudySession from '../views/StudySession.vue'
import AuthCardSearch from '../views/AuthCardSearch.vue'
import PublicCardSearch from '../views/PublicCardSearch.vue'
import ViewCardsInDeck from '../views/ViewCardsInDeck.vue'
import EditCard from '../views/EditCard.vue'
//import JoinCardAndDeck from '../components/JoinCardAndDeck.vue'

Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/', // changed this to false does not require auth
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/deck/public", // view public deck?
      name: "publicDecks", //"DeckEditor",
      component: Home, //DeckEditor,
      meta:{
        requiresAuth: false
      }
    },

    {
      path: "/:userId/deck", // authenticated home
      name: "my-decks", // "myDecks",
      component: AccountPage, // AccountPage,
      meta:{
        requiresAuth: true
      }
    },
    {
      path: "/editdeck/:deckId", //view 1 deck
      name: "DeckEditor",
      component: DeckEditor,
      meta:{
        requiresAuth: true
      }
    },
    {
      path: '/decksearch/:input', // search public decks
      name: 'publicDeckSearch',
      component: Home,
      meta:{
        requiresAuth: false
      }
    },
    {
      path: '/:userId/decksearch/:input', // search user decks
      name: 'authDeckSearch',
      component: AccountPage,
      meta:{
        requiresAuth: true
      }
    },
    {
      path: '/deck/:deckId', //view 1 deck
      name: 'deck-of-cards',
      component: ViewCardsInDeck,
      meta:{
        requiresAuth: true
      }
    },
    // {
    //   path: "/:userId/card", // get my cards
    //   name: "myCards",
    //   component: AuthCardSearch,
    //   meta:{
    //     requiresAuth: true
    //   }
    // },
    {
      path: '/cardsearch/:searchInput', // search public cards
      name: 'public-card-search',
      component: PublicCardSearch,
      meta:{
        requiresAuth: false
      }
    },
    
    {
      path: '/:userId/cardsearch/:input', // search user cards
      name: 'auth-card-search',
      component: AuthCardSearch,
      meta:{
        requiresAuth: false//true
      }
    },
    
    {
      path: '/card', // this route was '/study'; get public cards, post card
      name: 'StudySession',
      component: StudySession,
      meta:{
        requiresAuth: true
      }
    },
    {
      path: '/card/:cardId', // this route was '/study'; get, post, put, delete card
      name: 'ViewCard',
      component: StudySession,
      meta:{
        requiresAuth: true
      }
    },
    {
      path: '/editcard/:cardId', // this route was '/study'; get, post, put, delete card
      name: 'EditCard',
      component: EditCard,
      meta:{
        requiresAuth: true
      }
    },
    // {
    //   path: '/deck/:deckId/card/create',
    //   name: 'CardForm',
    //   component: CardForm,
    //   meta:{
    //     requiresAuth: true
    //   }
    // },
    // {
    //   path: '/deck/:deckId/card/:cardId',
    //   name: 'join-card-and-deck',
    //   component: JoinCardAndDeck,
    //   meta:{
    //      requiresAuth: true
    //   }
    // }
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
