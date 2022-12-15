<template>
  <form @submit.prevent="submitForm" class="cardForm">
    <div class="status-message error" v-show="errorMsg !== ''">{{errorMsg}}</div>
    <div class="form-group">
      <label class="label" for="front">Question:</label><br/>
      
      <input class="input-field" id="front" type="text" name="front" v-model="newCard.front" />
    </div>
    <br/>
     <div class="form-group">
      <label class="label" for="back">Answer:</label><br/>
      <input class="input-field" type="text" name="back" v-model="newCard.back" value="test"/>
    </div>
    <br/>
    <div class="form-group">
      <label class="label" for="cardKeywords">Keywords:</label><br/>
      <input class="input-field" id="cardKeywords" type="text" name="cardKeywords" v-model="newCard.cardKeywords" />
    </div>
    <br/>
    <div class="actions">
      <button class="btn-btn" >Submit</button>
    </div>
    <br/>
  </form>
</template>

<script>
import cardService from "../services/CardService.js";
import moment from 'moment'

export default {
  name: "card-form",
  props: {
    user: Object,
  },
  data() {
    return {
      newCard: {
        front: "",
        back: "",
        cardKeywords: "",
        creatorId: this.$store.state.user.userId,
        cardDate: moment(new Date()).format('DD-MM-YYYY')
      },
      errorMsg: ""
    };
  },
  created() {},
  methods: {
    submitForm() {
      const tempDeckCard = {
        deckId: this.$route.params.deckId,
        cardId: this.newCard.cardId
        };
      const tempCard = {
        front: this.newCard.front,
        back: this.newCard.back,
        cardKeywords: this.newCard.cardKeywords,
        creatorId: this.newCard.creatorId,
        cardDate: this.newCard.cardDate
      };
        cardService
          .addCard(tempCard, tempDeckCard.deckId)
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
.label{
  font-family: sans-serif;
  align-self: left;
  font-weight: bold;
}
.cardForm{
 align-content: right;
}
.input-field{
  width: 300px;
  word-wrap: break-word;
}
</style>
