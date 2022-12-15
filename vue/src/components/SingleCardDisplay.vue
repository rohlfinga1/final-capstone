<template>
  <section>
    <div class="card" v-bind:key="card.cardId" v-bind:card="card">
      <div class="content">
        <div class="front">
          <p class="text">{{ card.front }}</p>
        </div>
        <div class="back">
          <p class="text">{{ card.back }}</p>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: "single-card-display",
  data() {
    return {
      showFront: true,
    };
  },
  props: {
    card: Object,
  },
};
</script>

<!--  Single card CSS & HTML from: https://codepen.io/edeesims/pen/wvpYWW -->
<style>
@import "https://fonts.googleapis.com/css?family=Lily+Script+One";

body {
  background: #eee;
  font-family: "Lily Script One", "Trebuchet MS", "Lucida Sans Unicode",
    "Lucida Grande", "Lucida Sans", Arial, sans-serif;
}

.card {
  /* position: absolute;
  top: 50%;
  left: 50%; */
  width: 300px;
  height: 300px;
  margin: 30px;
  /* float: left; */
  perspective: 500px;
}

.content {
  /* position: absolute; */
  width: 100%;
  height: 100%;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);

  transition: transform 1s;
  transform-style: preserve-3d;
}

.card:hover .content {
  transform: rotateY(180deg);
  transition: transform 0.5s;
}

.front,
.back {
  position: absolute;
  height: 100%;
  width: 100%;
  background: white;
  line-height: normal;
  color: #203159;/* #03446a;*/
  text-align: center;
  font-size: 30px;
  border-radius: 5px;
  backface-visibility: hidden;
}

.back {
  background: white;
  color: #203159;/* white; #03446a;*/
  transform: rotateY(180deg);
}

.text {
  margin-left: 10%;
  margin-right: 10%;
}
</style>