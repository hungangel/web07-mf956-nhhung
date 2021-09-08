<template>
  <div class="option-dropdown">
    <div class="option-head flex">
      <div class="default-option fw-600" @click="optionOnClick">
        {{ defaultOptionText }}
      </div>
      <div class="arrow-zone">
        <div
          ref="arrowBtn"
          class="option-icon icon-32 i-blue-arrow"
          @click="arrowOnClick"
        ></div>
      </div>
    </div>
  </div>
</template>

<script>
import ResourceVI from "../../../scripts/resource";
export default {
  name: "OptionDropdown",
  props: {
    optionItemValue: String,
    entityClass: String,
  },
  data() {
    return {
      top: 0,
      defaultOptionText: "",
      isShow: false,
    };
  },
  methods: {
    /**
     * Sự kiện bấm vào mũi tên của contextmenu.
     * emit vị trí bấm và gọi hiển thị contextmenu
     * CreatedBy: NHHung(29/08)
     */
    arrowOnClick() {
      this.isShow = !this.isShow;
      this.top = this.isShow
        ? this.$refs.arrowBtn.getBoundingClientRect().top
        : 0;
      this.$emit("arrowOnClick", this.top, this.optionItemValue);
    },

    /**
     * Sự kiện bấm vào chức năng mặc định của contextmenu
     * Emit sự kiện bấm lựa chọn mặc  định
     *  CreatedBy: NHHung(29/08)
     */
    optionOnClick() {
      let action = ResourceVI.OptionText[this.entityClass][0].optionAction;
      this.$emit("optionOnClick", action, this.optionItemValue);
    },
  },
  mounted() {
    this.options = ResourceVI.OptionText[this.entityClass];
    this.defaultOptionText =
      ResourceVI.OptionText[this.entityClass][0].optionText;
  },
};
</script>

<style scoped>
@import "../../../css/base/context-menu.css";
</style>
