<template>
  <div class="inputbox" :class="[hasError ? 'error' : '', subClass]">
    <label
      >{{ labelText }}
      <span v-if="isRequired"
        ><span class="fw-400 required"> * </span></span
      ></label
    >
    <input
      :type="inputType"
      class="field-input mt-4"
      :class="[hasError ? 'missing' : '']"
      :id="id"
      tabindex="0"
      :datatype="dataType"
      :placeholder="placeHolder"
      :maxlength="maxLength"
      @input="onInput($event.target.value)"
      @blur="validateInput($event.target.value)"
      v-model="modelData"
      ref="myself"
    />
    <Tooltip :errorMessage="errorMessage" />
  </div>
</template>

<script>
import ValidateFn from "../../scripts/common/validatefunction";
import FormatFn from "../../scripts/common/formatfunction";
import Tooltip from "./BaseTooltip.vue";
export default {
  name: "BaseInputLabel",
  components: {
    Tooltip,
  },
  props: {
    // Common props
    labelText: String,
    inputType: String,
    id: String,
    subClass: String,
    fieldName: String,
    tabIndex: String,
    originValue: String,
    dataType: String,
    placeHolder: String,
    isRequired: Boolean,
    maxLength: Number,

    //formattning props
    autoFocus: Boolean,
    isShowed: Boolean,
  },
  data() {
    return {
      modelData: "",
      isEmpty: true,
      hasError: false,
      errorMessage: "",
    };
  },
  methods: {
    /**
     * Khi input, emit giá trị hiện tại của ô nhập
     * Nguyễn Hùng 30/07
     */
    onInput(value) {
      this.$emit("input", value);
    },

    /**'
     * Khi blur ô input, thực hiện validate định dạng dữ liệu
     * Hiển thị tooltip thông báo lôi nếu có
     * CreatedBy: NHHung(29/08)
     */
    validateInput() {
      let vm = this,
      isRequired= vm.isRequired;

      vm.hasError = false;
      //Những trường nhập cần validate
      let PopupMessage = ValidateFn.validateInputFormat( vm.modelData, vm.dataType, isRequired );

      if (PopupMessage != "Correct") {
        vm.hasError = true;
        vm.errorMessage = FormatFn.formatString( PopupMessage, vm.labelText );
      }
      //Nếu có lỗi thì emit thông báo lỗi
      if (vm.hasError) return vm.errorMessage;
      else return "Correct";
    },

    /**
     * Khi bấm vào nút clear, xóa dữ liệu trong ô nhập
     */
    clearInputOnClick() {
      this.modelData = "";
      this.isEmpty = true;
    },

    /**
     * Thực hiện focus vào chính ô input.
     * CreatedBy: NHHung(29/08)
     */
    doFocus(forceFocus) {
      if (forceFocus) {
        this.$refs.myself.focus();
        this.hasError = true;
      } else {
        if (this.autoFocus) {
          this.$refs.myself.focus();
        }
      }
    },

    resetFieldInput() {
      this.clearInputOnClick();
      this.hasError = false;
    },
  },
  watch: {
    originValue: function() {
      //Khởi tạo giá trị ban đầu cho input
      if (this.originValue != "undefined") {
        this.modelData = this.originValue;
      }
      this.hasError = false;
    },

    //focus vào ô nhập nếu ô này có autofocus
    isShowed: function() {
      this.doFocus();
    },
  },
};
</script>

<style scoped>
@import "../../css/common/format.css";
@import "../../css/common/default.css";
</style>
