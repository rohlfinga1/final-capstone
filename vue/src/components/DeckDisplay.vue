<template>
  <div>
    <div class="deck" v-bind:key="deck.deckId" v-bind:deck="deck">
      <div class="details">
        <h2 class="title">{{deck.name}}</h2>
        <div class="caption">{{deck.description}}</div>
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
 width: 300px;
 height: 300px;
 background: #ffffff;
 transition: box_shadow .25s ease-in;
 display: flex;
 justify-content: center;
 text-align: center;
 font-size: 20px;
 /* line-height: 200px; */
 border-radius: 20px;
}

.deck {
 box-shadow: rgba(73,85,121,0.5) 5px 3px,
  rgba(73,85,121,0.4) 10px 6px,
  rgba(73,85,121,0.3) 15px 9px;
}

.deck:hover {
 box-shadow: rgba(32,49,81,0.5) 5px 3px,
  rgba(32,49,81,0.4) 10px 6px,
  rgba(32,49,81,0.3) 15px 9px;
}

/* .deck {
  /* overflow: visible;
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
}*/

.details {
  /* position: absolute;*/
  /* left: 50px;
  right: 50px;
  bottom: 50px;
  top: 50px;
  padding: 10px; */
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
  height: 50px;
  width: 250px;
  margin-top: 50px;
}

.caption {
  font-weight: 500;
  font-size: 16px;
  color: #495579;
  margin-top: 5px;
  font-family: sans-serif;
}

/*Hover*/
/* .deck:hover .imgbox {
  bottom: 80px;
}

.deck:hover:before {
  transform: rotate(20deg);
}

.deck:hover:after {
  transform: rotate(10deg);
  box-shadow: 0 2px 20px rgba(0, 0, 0, .2);
} */
/* deck from https://uiverse.io/pedertanberg/dry-goose-81 */
</style>