<template>
  <div class="popup">
      <div class="popup-inner">
          <slot><card-form /></slot>
      <button class="popup-close, btn-btn" @click="TogglePopup()">
          Cancel
      </button>
      </div>
      
  </div>
</template>

<script>
import CardForm from './CardForm.vue'

export default {
    props: ['TogglePopup', 'cards'],
	components: CardForm
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
	width: 20%;
	box-shadow: rgba(73,85,121,0.4) 5px 5px;
	border-radius: 20px;
}

</style>
