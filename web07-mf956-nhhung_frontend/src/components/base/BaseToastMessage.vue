<template>
  <div class="toast-message-zone" ref="toastMessageZone">
    <div class="toastmessage bd-rd-4 flex ai-center" v-if="isShow">
      <div :class="['toast-icon icon-36', toastIcon]"></div>
      <div class="toast-text">{{ toastMessage }}</div>
      <div
        :class="['toast-close', toastIconClose]"
        @click="btnCloseOnClick"
      ></div>
    </div>
  </div>
</template>
<script>
import ResourceVI from "../../scripts/resource";
import { eventBus } from "../../main";
import { MESSAGE_MODE } from '../../scripts/enum/enumgeneral';
export default {
  name: "ToastMessage",
  data() {
    return {
      toastIcon: "",
      toastIconClose: "",
      isShow: false,
    };
  },

  methods: {
    /**
     * Sự kiện bấm nút đóng toast message
     * Ẩn toast message
     * CreatedBy: NHHung(29/08)
     */
    btnCloseOnClick() {
      this.isShow = false;
    },

    /**
     * Cập nhật thông tin toast message và hiển thị 
     * Ẩn toast message sau 1 thời gian
     * CreatedBy: NHHung(29/08)
     */
    showToastMessage(actionResult, toastType) {
      let vm = this;
      this.isShow = true;
      this.toastMessage = ResourceVI.PopupMessage[actionResult];

      switch (toastType) {
        case MESSAGE_MODE.Alert:
          vm.toastIcon = "i-error";
          vm.toastIconClose = "icon-red-cross";
          break;
        case MESSAGE_MODE.Inform:
          vm.toastIcon = "icon-toast-i";
          vm.toastIconClose = "icon-blue-cross";
          break;
        case MESSAGE_MODE.Notify:
          vm.toastIcon = "i-done-32";
          vm.toastIconClose = "icon-green-cross";
          break;
      }
      setTimeout(() => {
        vm.isShow = false;
      }, 3000);
    },
  },
  created() {
    //Sự kiện từ các components gọi hiển thị toast message
    eventBus.$on("showToastMessage", (actionResult, toastType, source, error) => {
      this.showToastMessage(actionResult, toastType, source);
      if(error){
        console.log(error)
      }
    });
  },
};
</script>
<style scoped>
@import "../../css/base/toast-message.css";
</style>
