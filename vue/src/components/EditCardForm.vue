<template>
  <form id="edit-form" v-on:submit.prevent="submitForm" class="editCardForm">
    <div class="status-message error" v-show="errorMsg !== ''">
      {{ errorMsg }}
    </div>
    <div class="form-group">
      <label class="edit-label" for="front">Question: </label>
      <br/>
      <input
        id="front"
        type="text"
        class="form-control, input-fields"
        v-model="newCard.front"
        autocomplete="off"
      />
      <br/><br/>
      <label class="edit-label" for="back">Answer: </label>

      <input
        id="back"
        type="text"
        class="form-control, input-fields"
        v-model="newCard.back"
      />
      <br/><br/>
      <label class="edit-label" for="cardKeywords">Keywords: </label>

      <input
        id="cardKeywords"
        type="text"
        class="form-control, input-fields"
        v-model="newCard.cardKeywords"
      />
      <br/><br/>
    </div>
    <button class="edit-btn-btn">Submit</button>
    <button class="edit-btn-btn" v-on:click="cancelForm" type="button">Cancel </button>
  </form>
</template>

<script>
import cardService from "../services/CardService.js";

export default {
  name: "edit-card-form",
  data() {
    return {
      
      newCard: {
        front: "",
        back: "",
        cardKeywords: "",
        cardId: 0 || this.$route.params.cardId,
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
#edit-form {
font-family: sans-serif;
align-content: center;
width: 20%;
margin-left: 40%;
margin-right:40%;
margin-bottom:24%;
text-align: center;
background-color: white;
padding: 5px;
border-radius: 20px;
box-shadow: rgba(73,85,121,0.5) 5px 3px,
 rgba(73,85,121,0.4) 10px 6px,
 rgba(73,85,121,0.3) 15px 9px;
}
.input-fields{
  width:90%;
}
.edit-label{
  font-family: sans-serif;
  font-weight: bold; 
}
.edit-btn-btn {
   background-color: #495579;
   border-style: solid, black;
   height: 30px;
   width: 100px;
   font-size: 20px;
   border-radius: 10px;
   margin-bottom: 10px;
   margin-right:15px;
   margin-left:15px;
   color:#FFFDEB;
}
.edit-btn-btn:hover {
  background-color: rgb(190, 209, 233);
  color: black;
}
</style>
