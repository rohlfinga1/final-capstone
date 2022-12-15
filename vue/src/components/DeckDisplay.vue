<template>
  <div>
    <div class="deck" v-bind:key="deck.deckId" v-bind:deck="deck">
      <div class="details">
        <h2 class="title">{{deck.name}}</h2>
        <span class="caption">{{deck.description}}</span>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  name: "deck-display",
  props: {
    deck: Object,
  },
}
</script>

<style>
.deck {
  /* overflow: visible; */
  position: relative;
  width: 190px;
  height: 254px;
  background: #fff;
  box-shadow: 0 2px 10px rgba(0, 0, 0, .2);
}

.deck:before,
.deck:after {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  border-radius: 4px;
  background: #fff;
  transition: 0.5s;
  z-index: -99;
}

.details {
  /* position: absolute; */
  left: 50px;
  right: 50px;
  bottom: 50px;
  top: 50px;
  padding: 10px;
  /* height: 60px; */
  text-align: center;
  text-transform: uppercase;
}

.decks {
  text-decoration: none;
}

/*Text*/
.title {
  font-weight: 600;
  font-size: 20px;
  color: #203159;
}

.caption {
  font-weight: 500;
  font-size: 16px;
  color: #495579;
  display: block;
  margin-top: 5px;
  font-family: sans-serif;
}

/*Hover*/
.deck:hover .imgbox {
  bottom: 80px;
}

.deck:hover:before {
  transform: rotate(20deg);
}

.deck:hover:after {
  transform: rotate(10deg);
  box-shadow: 0 2px 20px rgba(0, 0, 0, .2);
}
</style>