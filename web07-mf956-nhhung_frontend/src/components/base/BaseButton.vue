<template>
  <button
    class="button"
    :class="subClass"
    @click="$emit('btnClick')"
    ref="myButton"
    :tabindex="scopedTabIndex"
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
    tabIndex: Number,
  },
  data() {
    return {
      scopedTabIndex: 0,
    };
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
  mounted() {
    if (!this.tabIndex) {
      this.scopedTabIndex = 0;
    } else {
      this.scopedTabIndex = this.tabIndex;
    }
  },
};
</script>
<style scoped>
@import "../../css/base/button.css";
</style>
