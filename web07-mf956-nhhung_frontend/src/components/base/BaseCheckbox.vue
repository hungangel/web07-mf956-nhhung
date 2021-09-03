<template>
  <div class="checkbox">
    <div
      class="checkbox-outline bd-rd-2"
      :class="[isChecked ? 'checked rot-0' : 'rot-m90']"
    >
      <div
        class="checkbox-box bd-rd-2"
        :class="{ 'i-check': isChecked }"
        @click="checkboxOnClick"
      ></div>
    </div>
    <div class="checkbox-text" v-if="checkboxText">{{ checkboxText }}</div>
  </div>
</template>

<script>
export default {
  name: "Checkbox",
  props: {
    isChecked: Boolean,
    checkboxText: String,
  },
  methods: {
    checkboxOnClick() {
      this.$emit("cbclick");
    },
  },
};
</script>

<style scoped>
@import "../../css/base/checkbox.css";
</style>
