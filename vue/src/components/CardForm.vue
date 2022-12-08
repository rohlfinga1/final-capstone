<template>
  <form v-on:submit.prevent="submitForm" class="cardForm">
    <div class="status-message error" v-show="errorMsg !== ''">{{errorMsg}}</div>
    <div class="form-group">
      <label for="front">Question:  </label>
      <input id="front" type="text" name="front" v-model="card.front" />
    </div>
    <br/>
     <div class="form-group">
      <label for="back">Answer:  </label>
      <input type="text" name="back" v-model="card.back" />
    </div>
    <br/>
    <div class="form-group">
      <label for="cardKeywords">Tags:  </label>
      <input id="cardKeywords" type="text" name="cardKeywords" v-model="card.cardKeywords" />
    </div>
    <br/>
    <div class="actions">
      <button class="btn btn-submit">Submit</button>
    </div>
  </form>
</template>

<script>
import deckCardService from "../services/DeckCardService.js";

export default {
  name: "card-form",
  data() {
    return {
      card: {
        front: "",
        back: "",
        cardKeywords: "",
        deckId: this.$route.params.deckId,
        cardId: 1
      },
      errorMsg: ""
    };
  },
  created() {
    if (this.card.cardId != 0) {
      deckCardService
        .getCard(this.$route.params.deckId, this.card.cardId)
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
      const newCard = {
        front: this.card.front,
        back: this.card.back,
        cardKeywords: this.card.cardKeywords,
        deckId: Number(this.$route.params.deckId),
        cardId: this.card.cardId
      };
      if (this.card.cardId === 0) {
        // add
        deckCardService
          .addCard(newCard)
          .then(response => {
            if (response.status === 201) {
              alert("line 74")
              this.$router.push(`/deck/${newCard.deckId}/card`);
            }
          })
          .catch(error => {
            alert("line 80")
            this.handleErrorResponse(error, "adding");
          });
      } else {
        // update
        newCard.front = this.card.front;
        newCard.back = this.card.back;
        newCard.cardKeywords = this.card.cardKeywords;
        newCard.deckId = Number(this.$route.params.deckId);
        newCard.cardId = this.card.cardId;
        deckCardService
          .updateCard(newCard)
          .then(response => {
            if (response.status === 200) {
              alert("line 75")
              this.$router.push(`/deck/${newCard.deckId}/card`);
            }
          })
          .catch(error => {
            this.handleErrorResponse(error, "updating");
          });
      }
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
