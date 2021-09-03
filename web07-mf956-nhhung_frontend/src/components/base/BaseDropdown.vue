<template>
  <div>
    <div>
      <label v-if="labelText">{{ labelText }}</label>
    </div>
    <button :class="['dropdown', subClass]" style="z-index:2">
      <div class="choice">
        <div class="divtext">
          {{ defaultName }}
        </div>
        <div class="arrow-zone">
          <div class="arrow rot-0"></div>
        </div>
      </div>
    </button>
  </div>
</template>

<script>
// import axios from "axios";
export default {
  name: "BaseDropdown",
  props: {
    labelText: String,
    itemId: String,
    itemName: String,
    defaultName: String,
    subClass: String,
  },
};
</script>
<style scoped>
@import "../../css/base/dropdown.css";
</style>
