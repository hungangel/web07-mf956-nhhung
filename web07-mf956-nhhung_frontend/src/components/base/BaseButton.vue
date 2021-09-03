<template>
  <button
    class="button"
    :class="subClass"
    @click="$emit('btnClick')"
    ref="myButton"
    tabindex="0"
  >
    {{ buttonText }}
  </button>
</template>
<script>
export default {
  name: "Button",
  props: {
    buttonText: String,
    subClass: String,
    reFocus: Boolean,
  },
  watch: {
    /**
     * Thực hiện focus vào nút bấm
     * CreatedBy: NHHung(29/08)
     */
    reFocus: function() {
      this.$refs.myButton.focus();
    },
  },
};
</script>
<style scoped>
@import "../../css/base/button.css";
</style>
