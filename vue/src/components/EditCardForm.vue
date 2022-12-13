<template>
 <form v-on:submit.prevent="submitForm" class="editCardForm">
    <div class="status-message error" v-show="errorMsg !== ''">{{errorMsg}}</div>
    <div class="form-group">
      <label for="front">Question: </label>
      <input id="front" type="text" class="form-control" v-model="newCard.front" autocomplete="off" />
      <label for="back">Answer:  </label>
      <input id="back" type="text" class="form-control" v-model="newCard.back"/>
      <label for="cardKeywords">Keywords:  </label>
      <input id="cardKeywords" type="text" class="form-control" v-model="newCard.cardKeywords" />
    </div>
    <button class="btn btn-submit">Submit</button>
    <button class="btn btn-cancel" v-on:click="cancelForm" type="button">Cancel</button>
  </form>
</template>

<script>
import cardService from "../services/CardService.js";

export default {
  name: "edit-card-form",
  props:{
    cardId: {
      type: Number,
      }
  },
  data() {
    return {
      //newCard: this.props.card,
       newCard: {
         front: "",
         back: "",
         cardKeywords: "",
         deckId: "",
         cardId: Number("")
       },
      errorMsg: ""
    };
  },
  created() {
    if (this.cardID != 0) {
      cardService
        .getCard(this.cardId)
        .then(response => {
          this.card = response.data;
        })
        .catch(error => {
          if (error.response && error.response.status === 404) {
            alert(
              "Card not available. This card may have been deleted or you have entered an invalid card ID."
            );
            this.$router.push({ name: 'Home' });
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
        deckId: Number(this.$route.params.deckId),
        cardId: this.newCard.cardId
      };
        cardService
          .updateCard(tempCard)
          .then(response => {
            if (response.status === 201) {
              
              this.$router.go();
            }
          })
          .catch(error => {
            
            this.handleErrorResponse(error, "adding");
          });
    },
    cancelForm() {
      this.$router.push(`/deck/${this.$route.params.deckId}/card`);
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
