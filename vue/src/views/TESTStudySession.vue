<template>
    <div class="root">
        <div class="everything">
            <div class="right-wrong-count">
                <div class="correct-count"><h3>Correct: {{totalCorrect}}</h3></div>
                <div class="wrong-count"><h3>Incorrect: {{totalWrong}}</h3></div>
                <div class="cards-remaining-count"><h3>Cards Remaining: {{cardsLeft}}</h3></div>
            </div>
            <div class="card-and-btn-block">
                <button class="wrong-btn" v-on:click="markWrong">Wrong</button>
                <div class="card-content" style="font-family: Arial" v-show="flipped == false">
                    {{currentCard.front}}
                </div>
                <div class="card-content" style="font-family: Arial" v-show="flipped == true">
                    {{currentCard.back}}
                </div>
                <button class="correct-btn" v-on:click="markCorrect">Correct</button>
            </div>
            
            <div class="flip-right-wrong">
                <button class="left-arrow" v-on:click="goBack"><i class="arrow left"></i></button>
                <button class="flip-btn" v-on:click="flip" >THIS IS TEST MODE</button>
                <button class="right-arrow" v-on:click="goNext"><i class="arrow right"></i></button>
            </div>

            <div class="edit-and-end">
                <button class="edit-btn">Edit</button>
                <button class="end-btn">End Session</button>
            </div>
        </div>
        <div>
            <single-card-display class="card-display"/>
        </div>
    </div>
</template>

<script>
import SingleCardDisplay from '../components/SingleCardDisplay.vue'

export default {
    data() {
        return {
            correct: 0,
            wrong: 0,
            flipped: false,
            isRight: false,
            isWrong: false,
            currentCard: {},
            index: 0,
            cards: [
                {
                    front: "front1",
                    back: "back1",
                    correct: false,
                    scored: false
                },
                {
                    front: "front2",
                    back: "back2",
                    correct: false,
                    scored: false
                },
                {
                    front: "front3",
                    back: "back3",
                    correct: false,
                    scored: false
                }
            ]
        }
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
            if (this.currentCard != this.cards[0])
            {
                this.index = this.cards.indexOf(this.currentCard);
                this.currentCard = this.cards[this.index - 1];
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
        SingleCardDisplay
    },
    created(){
        this.currentCard = this.cards[0];
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
    width: 600px;
    height: 300px;
    text-align: center;
    font-size: 24px;
    font-weight: bold;
}
.card-content-correct {
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: rgb(199, 255, 199);
    border-radius: 8px;
    width: 600px;
    height: 300px;
    text-align: center;
    font-size: 24px;
    font-weight: bold;
}
.card-content-wrong {
    border-style: solid;
    border-color: rgb(0, 0, 0);
    background-color: rgb(255, 175, 175);
    border-radius: 8px;
    width: 600px;
    height: 300px;
    text-align: center;
    font-size: 24px;
    font-weight: bold;
}
.flip-btn {
    width: 400px;
    height: 100px;
    background-color: rgb(153, 153, 255);
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
    background-color: rgb(255, 169, 186);
    width: 150px;
    height: 150px;
    margin-right: 100px;
    transform: translate(0, 50%);
}
.wrong-btn:hover {
    cursor: pointer;
    background-color: rgb(255, 98, 130);
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
    top: 60%;
    transform: translate(-50%);
}
.edit-btn {
    margin-right: 50px;
    width: 100px;
    height: 50px;
    background-color: orange;
}
.edit-btn:hover {
    cursor: pointer;
    background-color: rgb(255, 205, 111);
}
.end-btn {
    margin-left: 50px;
    width: 100px;
    height: 50px;
    background-color: cornflowerblue;
    font-size: 12px;
}
.end-btn:hover {
    background-color: rgb(185, 210, 255);
    cursor: pointer;
}
.right-wrong-count {
    font-family: Arial, Helvetica, sans-serif;
    display: flex;
    position: absolute;
    left: 49%;
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