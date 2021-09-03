<template>
  <div
    class="option-wrapper bd-rd-2"
    :class="{ 'd-none': !isShow }"
    :style="optionStyle"
  >
    <div
      v-for="(option, index) in options"
      :key="index"
      class="option-item"
      :class="{ 'd-none': index == 0 }"
      @click="optionItemOnClick(option.optionId)"
    >
      {{ option.optionText }}
    </div>
  </div>
</template>

<script>
import ResourceVI from "../../../js/ResourceVI";
export default {
  name: "OptionItem",
  props: {
    selectedEntityId: String,
    optionEntity: String,
    mousePos: Number,
  },
  data() {
    return {
      options: [],
      isShow: false,
      optionStyle: "",
    };
  },
  methods: {
    /**
     * Sự kiện bấm vào lựa chọn trong contextMenu
     * Emit sự kiện tương ứng với lựa chọn
     * CreatedBy: NHHung(29/08)
     */
    optionItemOnClick(optionId) {
      this.$emit(
        "optionOnClick",
        this.options[optionId].optionAction,
        this.selectedEntityId
      );
      this.isShow = false;
    },

    /**
     * Sự kiện thay đổi vị trí bấm chuôt
     * Thay đổi vị trí contextmenu
     * CreatedBy: NHHung(29/08)
     */
    updateMenuPosition() {
      if (this.mousePos > 0) {
        this.isShow = true;
        this.optionStyle = `top: ${this.mousePos - 25}px`;
        let scHeight = document.body.clientHeight;
        if (this.mousePos + 120 > scHeight) {
          this.optionStyle = `top: ${this.mousePos - 145}px`;
        }
      } else {
        this.isShow = false;
      }
    },
  },
  mounted() {
    this.options = ResourceVI.optionTexts[this.optionEntity];
  },
  watch: {
    itemId: function() {},
    mousePos: function() {
      this.updateMenuPosition();
    },
  },
};
</script>

<style scoped>
@import "../../../css/base/context-menu.css";
</style>
