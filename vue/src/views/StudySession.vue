<template>
    <div class="root">
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.13.0/css/all.css">
        <div class="everything">
            <h1 style="font-family: Arial">DECK TITLE</h1>
            <div class="right-wrong-count">
                <div class="correct-count"><h3>Correct: {{countCorrect}}</h3></div>
                <div class="wrong-count"><h3>Incorrect: {{countWrong}}</h3></div>
                <div class="cards-remaining-count"><h3>Cards Remaining: {{cardsArray.length - currentCardIndex}}</h3></div>
            </div>
            <div class="card-and-btn-block">
                <button class="left-arrow" v-on:click="goBack"><i class="arrow left"></i></button>
                <button class="wrong-btn" v-on:click="markWrong"><i class="fas fa-times fa-8x"></i></button>
                <div class="card-content" v-on:click="flip"
                v-bind:class="{ 'card-content-correct': isGreen,
                'card-content-wrong': isRed}"
                 v-show="flipped == false">
                    {{currentCard.front}}
                </div>
                <div class="card-content" v-on:click="flip"
                v-bind:class="{ 'card-content-correct': isGreen,
                'card-content-wrong': isRed}"
                v-show="flipped == true">
                    {{currentCard.back}}
                </div>
                <button class="correct-btn" v-on:click="markCorrect"><i class="fas fa-check fa-7x"></i></button>
                <button class="right-arrow" v-on:click="goNext"><i class="arrow right"></i></button>
            </div>
            <div class="edit-and-end">
                <button class="edit-btn">Edit</button>
                <button class="end-btn" @click="() => TogglePopup('buttonTrigger')">End Session</button>
            </div>
        </div>
        <div class="popup">
            <popup
                v-if="popupTriggers.buttonTrigger"
                :TogglePopup="() => TogglePopup('buttonTrigger')">
                <h3>Are you sure you want to end your session?</h3>
                <p>Current Score: {{countCorrect}} / {{cardsArray.length}}</p>
                <p>Correct: {{countCorrect}} </p>
                <p>Incorrect: {{totalWrong}} </p>
                <p>Cards Remaining: {{cardsArray.length - currentCardIndex}}</p>
                <button class="end-btn-2" @click="$router.push('/')">End Session</button>
            </popup>
        </div>
        <div>
            <!-- <single-card-display class="card-display"/>
            <card-service class="card-service"/> -->
        </div>
    </div>
</template>

<script>
// import SingleCardDisplay from '../components/SingleCardDisplay.vue'
import { ref } from "vue";
import Popup from "../components/Popup.vue";
import CardService from '../services/CardService.js';
// import deckService from '../services/DeckService.js';

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
            currentCardIndex: 1
        }
    },
    created(){
        this.getCards(this.$route.params.deckId);
    },
    methods : {
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
            this.currentCard.correct = true;
            this.currentCard.wrong = false;
            this.currentCard.scored = true;
            this.isGreen = true;
            this.isRed = false;
            if(!this.cardsCorrect.has(this.currentCard.cardId)){
                this.countCorrect++;
                this.cardsCorrect.add(this.currentCard.cardId);
            }
        },
        markWrong() {
            this.currentCard.correct = false;
            this.currentCard.wrong = true;
            this.currentCard.scored = true;
            this.isRed = true;
            this.isGreen = false;
            if(!this.cardsWrong.has(this.currentCard.cardId)){
                this.countWrong++;
                this.cardsWrong.add(this.currentCard.cardId);
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
    computed : {
        // in here, I need to auto-update the correct/incorrect count
        // totalCorrect() {
        //     // const arrayOfCards = this.$store.state.cards;
        //     //     let sum = arrayOfCards.reduce((currentSum,)=>{
        //     //         return currentSum + (this.currentCard.Correct == true ? 1 : 0);},0);
        //     //         return sum;
        //     // },
        //     let cardsCorrect = 0;
        //     for (let i = 0; i < this.cardsArray.length; i++) {
        //         if (this.cardsArray[i].isGreen == true && this.cardsArray[i].isRed == false) {
        //             cardsCorrect++;
        //         }
        //     }
        //     return cardsCorrect;
        // },
        totalWrong() {
            let cardsWrong = 0;
            for (let i = 0; i < this.cardsArray.length; i++) {
                if (this.cardsArray[i].isGreen == false && this.cardsArray[i].isRed == true) {
                    cardsWrong++;
                }
            }
            return cardsWrong;
        },
        cardsLeft() {
            let cardsClicked = 0;
            for (let i = 0; i < this.cardsArray.length; i++) {
                if (this.cardsArray[i].isGreen == false && this.cardsArray[i].isRed == false){
                    cardsClicked++;
                }
            }
            let cardsRemaining = this.cardsArray.length - cardsClicked;
            return cardsRemaining;
        }
    },
    components: {
        // SingleCardDisplay,
        Popup,
    }
}
</script>

<style>
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
    background-color: rgb(202, 219, 250);
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
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: white;
    border-radius: 8px;
    width: 800px;
    height: 300px;
    text-align: center;
    font-size: 24px;
    font-weight: bold;
    font-family: Arial, Helvetica, sans-serif;
}
.card-content-correct {
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: rgb(199, 255, 199);
    border-radius: 8px;
    width: 800px;
    height: 300px;
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
.edit-btn {
    margin-right: 50px;
    width: 200px;
    height: 50px;
    background-color: cornflowerblue;
}
.edit-btn:hover {
    cursor: pointer;
    background-color: rgb(153, 181, 231);
}
.end-btn {
    margin-left: 50px;
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
    position: absolute;
    left: 49%;
    top: 40%;
    transform: translate(-50%);
    margin: 10px;
}
.correct-count {
    margin-right: 100px;
    color: green;
}
.wrong-count {
    margin-left: 30px;
    color: red
}
.cards-remaining-count {
    margin-left: 50px;
}

</style>
