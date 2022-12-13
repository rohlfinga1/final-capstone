<template>
    <div class="root">
        <div class="everything">
            <h1 style="font-family: Arial">DECK TITLE</h1>
            <div class="right-wrong-count">
                <div class="correct-count"><h3>Correct: {{totalCorrect}}</h3></div>
                <div class="wrong-count"><h3>Incorrect: {{totalWrong}}</h3></div>
                <div class="cards-remaining-count"><h3>Cards Remaining: {{cardsLeft}}</h3></div>
            </div>
            <div class="card-and-btn-block">
                <button class="left-arrow" v-on:click="goBack"><i class="arrow left"></i></button>
                <button class="wrong-btn" v-on:click="markWrong">Wrong</button>
                <div class="card-content" v-on:click="flip"
                :class="{ 'card-content-correct': currentCard.correct,
                'card-content-wrong': currentCard.wrong}"
                 v-show="flipped == false">
                    {{currentCard.front}}
                </div>
                <div class="card-content" v-on:click="flip"
                :class="{ 'card-content-correct': currentCard.correct,
                'card-content-wrong': currentCard.wrong}"
                v-show="flipped == true">
                    {{currentCard.back}}
                </div>
                <button class="correct-btn" v-on:click="markCorrect">Correct</button>
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
                <p>Current Score: {{totalCorrect}} / {{cards.length}}</p>
                <p>Correct: {{totalCorrect}} </p>
                <p>Incorrect: {{totalWrong}} </p>
                <p>Cards Remaining: {{cardsLeft}}</p>
                <button class="end-btn-2" @click="$router.push('/')">End Session</button>
            </popup>
        </div>
        <div>
            <single-card-display class="card-display"/>
        </div>
    </div>
</template>

<script>
import SingleCardDisplay from '../components/SingleCardDisplay.vue'
import { ref } from "vue";
import Popup from "../components/Popup.vue";
import CardService from '../services/CardService.js';

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
            flipped: false,
            currentCard: {
                front: '',
                back: '',
                correct: false,
                wrong: false,
                scored: false
            },
            index: 0
        }
    },
    created(){
        this.getCards(this.$store.state.cardDeckId.deckId);
        this.currentCard = this.$store.state.cards[0];
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
            if (this.currentCard != this.$store.state.cards[0])
            {
                this.index = this.cards.indexOf(this.currentCard);
                this.currentCard = this.$store.state.cards[this.index - 1];
                this.flipped = false;
            }
        },
        goNext() {
            if (this.currentCard != this.cards[this.cards.length - 1])
            {
                this.index = this.cards.indexOf(this.currentCard);
                this.currentCard = this.cards[this.index + 1];
                this.flipped = false;
            }
        },
        // these next two methods are about altering data on the card in the
        // store, which means I need to make mutations for them
        markCorrect() {
            this.currentCard.correct = true;
            this.currentCard.wrong = false;
            this.currentCard.scored = true;
        },
        markWrong() {
            this.currentCard.correct = false;
            this.currentCard.wrong = true;
            this.currentCard.scored = true;
        },
        getCards(deckId) {
            CardService.getCardsByDeckId(deckId).then((response) => {
                this.$store.commit("SET_CARDS", response.data);
            }).catch((error) => {
                alert(error);
            });
        }
    },
    computed : {
        // in here, I need to auto-update the correct/incorrect count
        totalCorrect() {
            let cardsCorrect = 0;
            for (let i = 0; i < this.cards.length; i++) {
                if (this.cards[i].correct == true && this.cards[i].scored == true) {
                    cardsCorrect++;
                }
            }
            return cardsCorrect;
        },
        totalWrong() {
            let cardsWrong = 0;
            for (let i = 0; i < this.cards.length; i++) {
                if (this.cards[i].correct == false && this.cards[i].scored == true) {
                    cardsWrong++;
                }
            }
            return cardsWrong;
        },
        cardsLeft() {
            let cardsClicked = 0;
            for (let i = 0; i < this.cards.length; i++) {
                if (this.cards[i].scored == true){
                    cardsClicked++;
                }
            }
            let cardsRemaining = this.cards.length - cardsClicked;
            return cardsRemaining;
        }
    },
    components: {
        SingleCardDisplay,
        Popup
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