<template>
  <div id="app">
    <Menu />
    <div class="main-grid">
      <Header />
      <div class="router-slot">
        <router-view></router-view>
      </div>
    </div>
    <LoadingScreen />
    <PopupMessage />
    <ToastMessage />
  </div>
</template>

<script>
import LoadingScreen from "./components/base/BaseLoadingScreen.vue";
import ToastMessage from "./components/base/BaseToastMessage.vue";
import PopupMessage from "./components/base/BasePopupMessage.vue";
import Menu from "./components/layout/menu/TheMenu.vue";
import Header from "./components/layout/TheHeader.vue";
export default {
  name: "App",
  components: {
    LoadingScreen,
    Menu,
    Header,
    ToastMessage,
    PopupMessage,
  },
  data() {
    return {
      showLoadingScreen: true,
    };
  },
};
</script>

<style>
@import "./css/main.css";
</style>
