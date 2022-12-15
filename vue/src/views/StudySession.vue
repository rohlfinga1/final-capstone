<template>
    <div class="root">
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.13.0/css/all.css">
        <div class="everything">
            <h1 style="font-family: Arial">{{deckName}}</h1>
            <div class="right-wrong-count">
                <div class="correct-count"><h3>Correct: {{countCorrect}}</h3></div>
                <div class="wrong-count"><h3>Incorrect: {{countWrong}}</h3></div>
                <div class="cards-remaining-count"><h3>Cards Remaining: {{cardsArray.length - countCorrect - countWrong}}</h3></div>
            </div>
            <div class="card-and-btn-block">
                <button class="left-arrow" v-on:click="goBack"><i class="arrow left"></i></button>
                <button class="wrong-btn" v-on:click="markWrong"><i id="x-mark" class="fas fa-times fa-8x"></i></button>
                <div class="card-content" v-on:click="flip"
                v-bind:class="{ 'card-content-correct': isGreen,
                'card-content-wrong': isRed}"
                 v-show="flipped == false">
                    {{currentCard.front}}
                </div>
                <div class="card-content-back" v-on:click="flip"
                v-bind:class="{ 'card-content-correct': isGreen,
                'card-content-wrong': isRed}"
                v-show="flipped == true">
                    {{currentCard.back}}
                </div>
                <button class="correct-btn" v-on:click="markCorrect"><i id="check" class="fas fa-check fa-7x"></i></button>
                <button class="right-arrow" v-on:click="goNext"><i class="arrow right"></i></button>
            </div>
            <div class="edit-and-end">
                <button class="end-btn" @click="() => TogglePopup('buttonTrigger')">End Session</button>
            </div>
        </div>
        <div class="popup">
            <popup
                v-if="popupTriggers.buttonTrigger"
                :TogglePopup="() => TogglePopup('buttonTrigger')">
                <h3 style="font-family: Arial">Are you sure you want to end your session?</h3>
                <p>Current Score: {{countCorrect}} / {{cardsArray.length}}</p>
                <p>Correct: {{countCorrect}} </p>
                <p>Incorrect: {{countWrong}} </p>
                <p>Cards Remaining: {{cardsArray.length - countCorrect - countWrong}}</p>
                <button class="end-btn-2" @click="$router.push('/')">End Session</button>
            </popup>
        </div>
    </div>
</template>

<script>
import { ref } from "vue";
import Popup from "../components/Popup.vue";
import CardService from '../services/CardService.js';
import DeckService from '../services/DeckService.js';

export default {
    name: "study-session",
    props: ["cards"],
    setup() {
        const popupTriggers = ref({
            buttonTrigger: false,
    });
        const TogglePopup = (trigger) => {
            popupTriggers.value[trigger] = !popupTriggers.value[trigger];
    };

    return {
      Popup,
      popupTriggers,
      TogglePopup,
    };
  },
    data() {
        return {
            cardsArray: [],
            flipped: false,
            currentCard: {
                cardId: this.$store.state.card.cardId,
                deckId: this.$route.params.deckId,
                front: '',
                back: '',
                cardKeywords: '',
                correct: false,
                wrong: false,
                scored: false
            },
            index: 0,
            isGreen: false,
            isRed: false,
            countCorrect: 0,
            countWrong: 0,
            cardsCorrect: new Set(),
            cardsWrong: new Set(),
            currentCardIndex: 1,
            deckName: ''
        }
    },
    created(){
        this.getCards(this.$route.params.deckId);
        this.getDeckById(this.$route.params.deckId);
    },
    methods : {
        getDeckById(deckId){
            DeckService.getDeck(deckId).then((res) => {
                this.deckName = res.data.name;
            })
        },
        flip() {
            if (this.flipped == false){
                this.flipped = true;
            }
            else if (this.flipped == true){
                this.flipped = false;
            }
        },
        goBack() {
            if (this.currentCard != this.cardsArray[0])
            {
                this.index = this.cardsArray.indexOf(this.currentCard);
                this.currentCard = this.cardsArray[this.index - 1];
                this.flipped = false;
                if(this.currentCard.scored) { 
                    this.isGreen = this.currentCard.correct;
                    this.isRed = this.currentCard.wrong;
                }
                else { 
                    this.resetColor();
                }
                this.currentCardIndex--;
            }

        },
        goNext() {
            if (this.currentCard != this.cardsArray[this.cardsArray.length - 1])
            {
                this.index = this.cardsArray.indexOf(this.currentCard);
                this.currentCard = this.cardsArray[this.index + 1];
                this.flipped = false;
                if(this.currentCard.scored) { 
                    this.isGreen = this.currentCard.correct;
                    this.isRed = this.currentCard.wrong;
                }
                else { 
                    this.resetColor();
                }
            }
            this.currentCardIndex++;
        },
        resetColor() { 
            this.isGreen = false; 
            this.isRed = false;
        },
        markCorrect() {
            // if card is not scored
            if (this.isGreen == false && this.isRed == false){
                if (this.currentCard.scored == false) {
                    this.countCorrect++;
                    this.currentCard.scored = true;
                    this.currentCard.correct = true;
                }
                this.isGreen = true;
                this.currentCard.scored = true;
                this.countCorrect++;
            }
            // if card is wrong
            else if (this.isRed == true) {
                this.isGreen = true;
                this.isRed = false;
                this.countCorrect++;
                this.countWrong--;
            }
        },
        markWrong() {
            // if card is not scored
            if (this.isGreen == false && this.isRed == false){
                if (this.currentCard.scored == false) {
                    this.countWrong++;
                    this.currentCard.scored = true;
                    this.currentCard.wrong = true;
                }
                this.isRed = true;
                this.currentCard.scored = true;
                this.countWrong++;
            }
            // if card is correct
            else if (this.isGreen == true) {
                this.isRed = true;
                this.isGreen = false;
                this.countWrong++;
                this.countCorrect--;
            }
        },
        getCards(deckId) {
            CardService.getCardsByDeckId(deckId).then((response) => {
                this.$store.commit("SET_CARDS", response.data);
                this.cardsArray = this.$store.state.cards;
                this.currentCard = this.cardsArray[0];
            }).catch((error) => {
                alert(error);
            });
        }
    },
    components: {
        Popup
    }
}
</script>

<style>
#check {
    font-size: 48px;
}
#x-mark {
    font-size: 48px;
}
button {
    font-size: 20px;
}
.arrow {
    border: solid black;
    border-width: 0 3px 3px 0;
    display: inline-block;
    padding: 3px;
}
.right {
    transform: rotate(-45deg);
    -webkit-transform: rotate(-45deg);
}
.left {
    transform: rotate(135deg);
    -webkit-transform: rotate(135deg);
}
.left-arrow {
    position: absolute;
    top: 37.5%;
    left: -13.5%;
}
.right-arrow {
    position: absolute;
    top: 37.5%;
    right: -13.5%;
}
.left-arrow:hover {
    cursor: pointer;
}
.right-arrow:hover {
    cursor: pointer;
}
.everything {
    display: inline-block;
    position: relative;
    width: 100vw;
    height: 100vh;
}
.card-and-btn-block {
    display: inline-flex;
    position: absolute;
    top: 25%;
    left: 50%;
    transform: translate(-50%, -50%);
}
.card-content {
    display: flex;
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: white;
    border-radius: 8px;
    width: 800px;
    height: 300px;
    align-items: center;
    justify-content: center;
    font-size: 24px;
    font-weight: bold;
    font-family: Arial, Helvetica, sans-serif;
}
.card-content:hover {
    box-shadow: 0 0 30px rgba(33,33,33,.2); 
}
.card-content-back {
    display: flex;
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: white;
    border-radius: 8px;
    width: 800px;
    height: 300px;
    align-items: center;
    justify-content: center;
    font-size: 24px;
    font-weight: bold;
    font-style: italic;
    font-family: Arial, Helvetica, sans-serif;
}
.card-content-back:hover {
    box-shadow: 0 0 30px rgba(33,33,33,.2); 
}
.card-content-correct {
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: rgb(199, 255, 199);
    border-radius: 8px;
    width: 800px;
    height: 300px;
    align-items: center;
    justify-content: center;
    text-align: center;
    font-size: 24px;
    font-weight: bold;
    font-family: Arial, Helvetica, sans-serif;
}
.card-content-wrong {
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: rgb(255, 175, 175);
    border-radius: 8px;
    width: 800px;
    height: 300px;
    text-align: center;
    align-items: center;
    justify-content: center;
    font-size: 24px;
    font-weight: bold;
    font-family: Arial, Helvetica, sans-serif;
}
.flip-btn {
    width: 400px;
    height: 100px;
    background-color: rgb(153, 153, 255);
    font-size: 36px;
}
.flip-btn:hover {
    background-color: rgb(192, 192, 255);
    cursor: pointer;
}
.correct-btn {
    background-color: rgb(154, 255, 154);
    width: 150px;
    height: 150px;
    margin-left: 100px;
    transform: translate(0, 50%);
}
.correct-btn:hover {
    cursor: pointer;
    background-color: rgb(215, 255, 215);
}
.wrong-btn {
    background-color: rgb(255, 152, 174);
    width: 150px;
    height: 150px;
    margin-right: 100px;
    transform: translate(0, 50%);
}
.wrong-btn:hover {
    cursor: pointer;
    background-color: rgb(255, 184, 198);
}
.right-arrow {
    margin-left: 50px;
    width: 70px;
    height: 70px;
}
.left-arrow {
    margin-right: 50px;
    width: 70px;
    height: 70px;
}
.flip-right-wrong {
    display: inline-block;
    position: absolute;
    top: 45%;
    left: 50%;
    transform: translate(-50%);
}
.edit-and-end {
    display: inline-block;
    position: absolute;
    left: 50%;
    top: 48%;
    transform: translate(-50%);
}
.end-btn {
    width: 200px;
    height: 50px;
    background-color: cornflowerblue;
}
.end-btn:hover {
    background-color: rgb(153, 181, 231);
    cursor: pointer;
}
.end-btn-2 {
    background-color: cornflowerblue;
}
.end-btn-2:hover {
    background-color: rgb(153, 181, 231);
    cursor: pointer;
}
.right-wrong-count {
    font-family: Arial, Helvetica, sans-serif;
    display: flex;
    width: 800px;
    justify-content: space-between;
    position: absolute;
    left: 49%;
    top: 40%;
    transform: translate(-49%);
    margin: 10px;
}
.correct-count {
    color: green;
}
.wrong-count {
    transform: translate(50%);
    color: red
}
.cards-remaining-count {
}

</style>
