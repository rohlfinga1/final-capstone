<template>
  <div class="popup">
      <div class="popup-inner">
          <slot><edit-card-form :card="card" /></slot>
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
      <label for="cardKeywords">Tags:  </label>
      <input id="cardKeywords" type="text" name="cardKeywords" v-model="newCard.cardKeywords" />
    </div>
    <br/>
    <div class="actions">
      <button class="btn btn-submit" >Submit</button>
    </div>
  </form>
      <button class="popup-close" @click="EditTogglePopup()">
          Cancel (close popup)
      </button>
      </div>
      
  </div>
</template>

<script>
import EditCardForm from './EditCardForm.vue'

export default {
    props: ['EditTogglePopup', 'card'],
	components: EditCardForm
}
</script>

<style scoped>
.popup {
	position: fixed;
	top: 0;
	left: 0;
	right: 0;
	bottom: 0;
	z-index: 99;
	background-color: rgba(0, 0, 0, 0.2);
	
	display: flex;
    flex-direction: column;
	align-items: center;
	justify-content: center;
    text-align: center;
}
.popup-inner {
	background-color: rgb(255, 255, 255);
	padding: 32px;
}

</style>
