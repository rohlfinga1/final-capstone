<template>
  <form @submit.prevent="submitForm" class="editCardForm">
    <div class="status-message error" v-show="errorMsg !== ''">
      { {errorMsg}}
    </div>
    <div class="form-group">
      <label for="front">Question:  </label>
      
      <input id="front" type="text" name="front" v-model="newCard.front" value="Test"/>
    </div>
    <br />
    <div class="form-group">
      <label for="back">Answer: </label>
      <input type="text" name="back" v-model="newCard.back" />
    </div>
    <br />
    <div class="form-group">
      <label for="cardKeywords">Tags:  </label>
      <input id="cardKeywords" type="text" name="cardKeywords" v-model="newCard.cardKeywords" />
    </div>
    <br />
    <div class="actions">
      <button class="btn btn-submit">Submit</button>
    </div>
  </form>
</template>

<script>
import cardService from "../services/CardService.js";

export default {
  name: "edit-card-form",
  props:['card'],
  data() {
    return {
      newCard: this.props.card,
      // newCard: {
      //   front: this.$route.params.front,
      //   back: this.$route.params.back,
      //   cardKeywords: this.$route.params.cardKeywords,
      //   deckId: this.$route.params.deckId,
      //   cardId: this.$route.params.cardId
      // },
      errorMsg: ""
    };
  },
  created() {
      // cardService
      //   .getCard(this.$route.params.deckId, this.card.cardId)
      //   .then(response => {
      //     this.card = response.data;
      //   })
      //   .catch(error => {
      //     if (error.response && error.response.status === 404) {
      //       alert(
      //         "Card not available. This card may have been deleted or you have entered an invalid card ID."
      //       );
      //       this.$router.push({ name: 'Home' });
      //     }
      //   });
    },
  
  methods: {
    submitForm() {
      const tempCard = {
        front: this.newCard.front,
        back: this.newCard.back,
        cardKeywords: this.newCard.cardKeywords,
        deckId: Number(this.$route.params.deckId),
        cardId: this.newCard.cardId,
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
