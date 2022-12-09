<template>
    <div class="root">
        <div class="everything">
            <div class="card-and-btn-block">
                <button class="wrong-btn">Wrong</button>
                <div class="card-content" style="font-family: Arial"><single-card-display/></div>
                <button class="correct-btn">Correct</button>
            </div>
            
            <div class="flip-right-wrong">
                <button class="left-arrow"><i class="arrow left"></i></button>
                <button class="flip-btn">Flip</button>
                <button class="right-arrow"><i class="arrow right"></i></button>
            </div>

            <div class="edit-and-end">
                <button class="edit-btn">Edit</button>
                <button class="end-btn">End Session</button>
            </div>
        </div>
        <single-card-display class="card-display"/>
    </div>
</template>

<script>
import SingleCardDisplay from '../components/SingleCardDisplay.vue'

export default {
    components: {
        SingleCardDisplay
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
    top: 20%;
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
    top: 40%;
    left: 50%;
    transform: translate(-50%);
}
.edit-and-end {
    display: inline-block;
    position: absolute;
    left: 50%;
    top: 55%;
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

</style>