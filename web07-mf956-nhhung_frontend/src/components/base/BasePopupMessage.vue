<template>
  <div class="hello" :class="{ 'd-none': isHiddenWarning }">
    <div class="message-background"></div>
    <div class="message-grid bd-rd-3">
      <div class="message-content flex">
        <div class="message-icon icon-36" :class="icon"></div>
        <div class="message-text flex">
          <span class="text-name">{{ message.textName }}</span>
          <span class="text-value">{{ message.textValue }}</span>
          <span class="text-body">{{ message.textBody }}</span>
        </div>
      </div>
      <div
        class="message-footer flex jc-space-bw"
        :class="{ 'jc-space-ar': messageType == 'ALERT' }"
      >
        <div
          class="mess-footer-left"
          :class="{ 'd-none': messageType == 'ALERT' }"
        >
          <Button
            v-if="showBtnArr[0]"
            class="secondary fw-600"
            :buttonText="btnCancelText"
            @btnClick="messageBtnOnClick('CANCEL')"
          />
        </div>
        <div class="mess-footer-right flex">
          <Button
            v-if="showBtnArr[1]"
            class="secondary fw-600 mr-8"
            :subClass="messageType == 'ALERT' ? 'd-none' : ''"
            :buttonText="btnnDeclineText"
            @btnClick="messageBtnOnClick('DECLINE')"
          />
          <Button
            class="primary fw-600"
            :buttonText="btnConfirmText"
            @btnClick="messageBtnOnClick('CONFIRM')"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import ResourceVI from "../../js/ResourceVI.js";
import { eventBus } from "../../main.js";
import Button from "./BaseButton.vue";
export default {
  name: "PopupMessage",
  components: {
    Button,
  },
  data() {
    return {
      isHiddenWarning: true,
      action: "",
      messageType: "",
      btnnDeclineText: ResourceVI.ButtonText.NO,
      btnConfirmText: ResourceVI.ButtonText.YES,
      btnCancelText: ResourceVI.ButtonText.CANCEL,
      showBtnArr: [true, false],
      source: "",
      icon: "i-blue-question",
      message: {},
    };
  },
  methods: {
    messageBtnOnClick(choice) {
      switch (this.messageType) {
        case "NOTIFY":
          break;
        default:
          eventBus.$emit(`${this.source}PopupResponse`, this.action, choice);
          break;
      }
      this.isHiddenWarning = true;
    },

    /**
     * Tạo thông tin popup message dựa theo loại message gửi lên
     * Xác định các nút bị ẩn / hiện tùy theo loại popup
     * Hiển thị popup message
     * CreatedBy: NHHung(31/08)
     */
    showPopupMessage(source, message, action) {
      let vm = this;
      vm.source = source;
      vm.messageType = message.messageType;
      switch (vm.messageType) {
        case "FULL":
          vm.showBtnArr = [true, true];
          vm.btnnDeclineText = ResourceVI.ButtonText.NO;
          vm.btnConfirmText = ResourceVI.ButtonText.YES;
          vm.btnCancelText = ResourceVI.ButtonText.CANCEL;
          vm.icon = "i-blue-question";
          break;
        case "CONFIRM":
          vm.showBtnArr = [true, false];
          vm.btnConfirmText = ResourceVI.ButtonText.YES;
          vm.btnCancelText = ResourceVI.ButtonText.NO;
          vm.icon = "i-warning";
          break;
        case "NOTIFY":
          vm.showBtnArr = [false, false];
          vm.btnConfirmText = ResourceVI.ButtonText.WARNING;
          vm.icon = "i-warning";
          break;
        case "ALERT":
          vm.showBtnArr = [false, false];
          vm.btnConfirmText = ResourceVI.ButtonText.CLOSE;
          vm.icon = "i-error";
          break;
      }
      this.message = message;
      this.action = action;
      this.isHiddenWarning = false;
    },
  },
  created() {
    let vm = this;
    eventBus.$on("showPopupMessage", (source, message, action) => {
      vm.showPopupMessage(source, message, action);
    });
  },
  watch: {},
};
</script>

<style scoped>
@import "../../css/base/message-popup.css";
</style>
