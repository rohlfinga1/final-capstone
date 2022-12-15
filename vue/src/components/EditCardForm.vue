<template>
  <form v-on:submit.prevent="submitForm" class="editCardForm">
    <div class="status-message error" v-show="errorMsg !== ''">
      {{ errorMsg }}
    </div>
    <div class="form-group">
      <label for="front">Question: </label>
      &nbsp;
      <input
        id="front"
        type="text"
        class="form-control"
        v-model="newCard.front"
        autocomplete="off"
      />
      &nbsp;
      &nbsp;
      <label for="back">Answer: </label>
      &nbsp;
      <input
        id="back"
        type="text"
        class="form-control"
        v-model="newCard.back"
      />
      &nbsp;
      &nbsp;
      <label for="cardKeywords">Keywords: </label>
      &nbsp;
      <input
        id="cardKeywords"
        type="text"
        class="form-control"
        v-model="newCard.cardKeywords"
      />
    </div>
    <button class="btn btn-submit">Submit</button>
    <button class="btn btn-cancel" v-on:click="cancelForm" type="button">
      Cancel
    </button>
    <!-- <button class="btn btn-delete" v-on:click="deleteCard">Delete Card From Deck</button> -->
  </form>
</template>

<script>
import cardService from "../services/CardService.js";
//import CardDeckIdService from '../services/CardDeckIdService.js';

export default {
  name: "edit-card-form",
  data() {
    return {
      //newCard: this.props.card,
      // deckId: this.$route.params.
      newCard: {
        front: "",
        back: "",
        cardKeywords: "",
        cardId: 0,
        //creator: '',
        creatorId: this.$store.state.user.userId,
        cardDate: "",
      },
      errorMsg: "",
    };
  },
  created() {
    if (this.cardId != 0) {
      cardService
        .getCard(this.$route.params.cardId)
        .then((response) => {
          this.newCard = response.data;
        })
        .catch((error) => {
          if (error.response && error.response.status === 404) {
            alert(
              "Card not available. This card may have been deleted or you have entered an invalid card ID."
            );
            this.$router.push({ name: "Home" });
          }
        });
    }
  },
  methods: {
    submitForm() {
      const tempCard = {
        front: this.newCard.front,
        back: this.newCard.back,
        cardKeywords: this.newCard.cardKeywords,
        cardId: this.newCard.cardId,
        //creator: this.newCard.creator,
        creatorId: this.newCard.creatorId,
        cardDate: this.newCard.cardDate,
      };
      cardService
        .updateCard(tempCard.cardId, tempCard)
        .then((response) => {
          if (response.status === 200) {
            this.$router.back();
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "updating");
        });
    },
    cancelForm() {
      this.$router.back();
    },

    // deleteCard(){
    //   const tempDeckCard = {
    //     deckId: this.$route.params.deckId,
    //     cardId: this.newCard.cardId
    //     };
    //     CardDeckIdService
    //     .deleteCardFromDeck(tempDeckCard.deckId, tempDeckCard.cardId).then((response) => {
    //       if (response.status === 201) {
    //         this.$router.back();
    //       }
    //     });
    // },

    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " + verb + " card. Response received was '" + error + "'.";
      } else if (error.request) {
        this.errorMsg = "Error " + verb + " card. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " card. Request could not be created.";
      }
      
    },
  },
};
</script>

<style>
</style>
