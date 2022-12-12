<template>
  <div class="popup">
      <div class="popup-inner">
          <slot><edit-card-form :card="card" /></slot>
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
