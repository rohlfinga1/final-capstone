<template>
  <header id="menubar">
    <div class="title">
      <div>
        <h2>Crash Cards</h2>
        <br/>
      </div>
      <div>
        <div class="loader"></div>
        <br/>
      </div>
    </div>
    <nav>
      <ul>
        <!--<li><router-link v-bind:to="{ name: 'login' }" v-if="token==''">LOGIN</router-link></li> possibly switch v-bind for v-model   @click="processResults"-->
        <li>
          <router-link v-bind:to="{ name: 'home' }" class="link"
            >HOME</router-link
          >
        </li>
        <li>
          <router-link v-bind:to="{ name: 'account-page' }" class="link"
            >ACCOUNT</router-link
          >
        </li>
        <!--<li>
          <router-link v-bind:to="{ name: 'StudySession' }" class="link"
            >STUDY</router-link
          >
        </li>-->
        <li>
          <router-link v-bind:to="{ name: 'logout' }" class="link"
            >LOG OUT</router-link
          >
        </li>
      </ul>
    </nav>
  </header>
</template>

<script>
export default {
  name: "menu-bar",
};
</script>

<style>
#menubar {
  align-items: center;
}
header {
  font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto,
    "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji",
    "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
  background-color: #495579;
  display: flex;
  align-items: center;
  justify-content: space-between;
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  flex-grow: 1;
}
.title {
  font-weight: 600;
  font-size: 20px;
  color: #203159;
  height: 50px;
  width: 250px;
  align-items: center;
  color: #FFFDEB;
  margin-left: 20px;
  margin-top: 0;
  justify-content: space-between;
}


header div {
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  justify-content: space-between;
  align-content: space-around
}
header nav {
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  flex-grow: 1;
  justify-content: flex-end;
}
header nav ul {
  justify-content: space-evenly;
  flex-grow: 0.5;
  justify-items: center;
  align-items: center;
}
/* #header nav ul li a 
{
    display: flex;
    flex-direction: row;
    justify-content: space-evenly;
    text-decoration:  none;

} */

nav ul {
  display: inline;
  margin: 0;
  padding: 20px 0 0 0;
  display: flex;
  flex-direction: row;
}

nav ul li {
  list-style: none;
  width: 30%;
  border-radius: 5px;
  text-align: center;
  line-height: 50px;
  text-decoration: none;
  font-size: 1.2rem;
  color: #FFFDEB;
}

.link {
  text-decoration: none;
  font-size: 1.2rem;
  color: #FFFDEB;
}

.btn {
  text-emphasis: none;
  background: white;
  color: #03446a;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}
h2 {
  font-family: "Arial Narrow Bold", sans-serif;
  text-align: center;
  font-style: italic;
  color: #FFFDEB;
  margin-top: 0;
  font-size: 2rem;
}
.loader {
  width: 48px;
  height: 48px;
  margin: auto;
  position: relative;
  animation-play-state: paused;
}

.loader:before {
  content: '';
  width: 48px;
  height: 5px;
  background: rgba(73, 85, 121, 0.5);
  position: absolute;
  top: 60px;
  left: 0;
  border-radius: 50%;
  /* animation: shadow324 0.5s linear infinite; */
  animation-name: shadow324;
  animation-duration: 0.5s;
  animation-iteration-count: infinite;
  animation-play-state: paused;
}

.loader:after {
  content: '';
  width: 100%;
  height: 100%;
  background: skyblue;
  position: absolute;
  top: 0;
  left: 0;
  border-radius: 4px;
  /* animation: jump7456 0.5s linear infinite; */
  animation-name: jump7456;
  animation-duration: 0.5s;
  animation-iteration-count: infinite;
  animation-play-state: paused;
}

@keyframes jump7456 {
  15% {
    border-bottom-right-radius: 3px;
  }

  25% {
    transform: translateY(9px) rotate(22.5deg);
  }

  50% {
    transform: translateY(18px) scale(1, .9) rotate(45deg);
    border-bottom-right-radius: 40px;
  }

  75% {
    transform: translateY(9px) rotate(67.5deg);
  }

  100% {
    transform: translateY(0) rotate(90deg);
  }
}

@keyframes shadow324 {

  0%,
    100% {
    transform: scale(1, 1);
  }

  50% {
    transform: scale(1.2, 1);
  }
}

.loader:hover::after {
  animation-play-state: running;
}
.loader:hover::before {
  animation-play-state: running;
}
/* card logo from https://uiverse.io/alexruix/white-cat-50*/
</style>