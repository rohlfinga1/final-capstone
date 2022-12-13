<template>
  <form @submit.prevent="submitForm" class="cardForm">
    <div class="status-message error" v-show="errorMsg !== ''">{{errorMsg}}</div>
    <div class="form-group">
      <label for="front">Question:  </label>
      
      <input id="front" type="text" name="front" v-model="newCard.front" />
    </div>
    <br/>
     <div class="form-group">
      <label for="back">Answer:  </label>
      <input type="text" name="back" v-model="newCard.back" value="test"/>
    </div>
    <br/>
    <div class="form-group">
      <label for="cardKeywords">Keywords:  </label>
      <input id="cardKeywords" type="text" name="cardKeywords" v-model="newCard.cardKeywords" />
    </div>
    <br/>
    <div class="actions">
      <button class="btn btn-submit" >Submit</button>
    </div>
  </form>
</template>

<script>
//import CardDeckIdService from '../services/CardDeckIdService.js';
import cardService from "../services/CardService.js";

export default {
  name: "card-form",
  data() {
    return {
      newCard: {
        front: "",
        back: "",
        cardKeywords: "",
        //deckId: this.$route.params.deckId,
        cardId: null,
        creator: '',
        creatorId: 0,
        cardDate: {}
      },
      errorMsg: ""
    };
  },
  created() {},
  methods: {
    submitForm() { //currently does not add card to deck
      // const tempDeckCard = {
      //   deckId: this.$route.params.deckId,
      //   cardId: this.newCard.cardId
      //   };
      const tempCard = {
        front: this.newCard.front,
        back: this.newCard.back,
        cardKeywords: this.newCard.cardKeywords,
        cardId: this.newCard.cardId,
        creator: this.newCard.creator,
        creatorId: this.newCard.creatorId,
        cardDate: this.newCard.cardDate
      };
        cardService
          .addCard(tempCard, this.$route.params.deckId)
          .then(response => {
            if (response.status === 201) {
              this.$router.go();
            }
          })
          .catch(error => {
            
            this.handleErrorResponse(error, "adding");
          });
        // CardDeckIdService
        // .addCardToDeck(tempDeckCard.deckId, tempDeckCard.cardId)
        // .then(response => {
        //     if (response.status === 201) {
        //       this.$router.go();
        //     }
        //   })
        //   .catch(error => {
            
        //     this.handleErrorResponse(error, "adding");
        //   });
    },
    cancelForm() {
      this.$router.push(`/editdeck/${this.$route.params.deckId}`);
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " + verb + " card. Response received was '" +
          error +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " card. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " card. Request could not be created.";
      }
    }
  },
  
};
</script>

<style>
</style>
