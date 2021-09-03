<template>
  <div class="top-cover" v-if="showLoadingScreen">
    <div class="top-cover-background"></div>
    <div class="lds-roller">
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
    </div>
  </div>
</template>
<script>
import { eventBus } from "../../main";
export default {
  name: "LoadingScreen",
  props: {},
  data() {
    return {
      showLoadingScreen: true,
    };
  },
  created() {
    eventBus.$on("showLoadingScreen", () => {
      this.showLoadingScreen = true;
    });
    eventBus.$on("hideLoadingScreen", () => {
      this.showLoadingScreen = false;
    });
  },
};
</script>
<style scoped>
@import "../../css/base/loading.css";
</style>
