<template>
  <div>
    <div class="page-navigation" :class="{ expanded: contentExpanded }">
      <div class="number-total-info">
        <p>
          Tổng số:
          <span class="number-total">
            {{ totalRecord }}
          </span>
          bản ghi
        </p>
      </div>

      <div class="per-page-info">
        <ComboBox
          tabIndex="0"
          subClass="w-100p"
          cbDirection="bottom-32"
          itemId="pageSizeValue"
          itemName="pageSizeText"
          :textDisable="true"
          :inputItems="pageSizeOptions"
          :defaultId="pageSizeOptions[0]['pageSizeValue']"
          :defaultName="pageSizeOptions[0]['pageSizeText']"
          :updateCombobox="updateCombobox"
          :numberOfCol="1"
          v-model="currentPageSize"
        />
        <div class="navigation-button">
          <Button
            :subClass="
              currentPageNumber <= 1
                ? 'disabledText page-number white-hv mr-13'
                : 'page-number white-hv mr-13'
            "
            @btnClick="pageButtonOnClick('PrevPage')"
            :buttonText="'Trước'"
            :disabled="currentPageNumber <= 1"
          />
          <Button
            v-for="(value, index) in pageIndexBtns"
            :key="index"
            subClass="page-number white-hv "
            :class="[{ selected: currentPageNumber == value }]"
            :buttonText="value + ''"
            @btnClick="pageButtonOnClick(value)"
          />

          <Button
            :subClass="
              currentPageNumber >= totalPage
                ? 'disabledText page-number white-hv ml-13'
                : 'page-number white-hv ml-13'
            "
            @btnClick="pageButtonOnClick('NextPage')"
            :buttonText="'Sau'"
            :disabled="currentPageNumber >= totalPage"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "./BaseButton.vue";
import ComboBox from "./BaseComboBox.vue";
import ResourceVI from "../../js/ResourceVI.js";
import { eventBus } from "../../main.js";
export default {
  name: "PageNavigation",
  props: {
    contentExpanded: Boolean,
    totalRecord: Number,
    totalPage: Number,
    pageSize: Number,
    pageNumber: Number,
  },
  components: {
    Button,
    ComboBox,
  },
  data() {
    return {
      currentPageNumber: 1,
      currentPageSize: this.pageSize,
      updateCombobox: true,
      startInd: 0,
      endInd: 0,
      leftLimit: 0,
      rightLimit: 1,
      numberOfShowButton: 5,
      pageIndexBtns: [],
      pageSizeOptions: [
        { pageSizeValue: 10, pageSizeText: "10 " + ResourceVI.pageSizeText },
        { pageSizeValue: 20, pageSizeText: "20 " + ResourceVI.pageSizeText },
        { pageSizeValue: 30, pageSizeText: "30 " + ResourceVI.pageSizeText },
        { pageSizeValue: 50, pageSizeText: "50 " + ResourceVI.pageSizeText },
        { pageSizeValue: 100, pageSizeText: "100 " + ResourceVI.pageSizeText },
      ],
    };
  },
  methods: {
    /**
     * Sự kiện bấm vào nút chọn trang
     * Cập nhật số trang hiện tại và emit sự kiện tải lại dữ liệu
     * CreatedBy: NHHung(29/08)
     */
    pageButtonOnClick(currentPageNumber) {
      let vm = this;
      switch (currentPageNumber) {
        case "PrevPage":
          if (vm.currentPageNumber > 1) {
            vm.currentPageNumber -= 1;
          }
          break;
        case "NextPage":
          if (vm.currentPageNumber < vm.totalPage) {
            vm.currentPageNumber += 1;
          }
          break;
        case "...":
          return;
        default:
          vm.currentPageNumber = currentPageNumber;
          break;
      }
      vm.$emit("onUpdatePagingInfo", vm.currentPageNumber, vm.pageSize);
      vm.adjustCenterButtonNumber();
    },

    /**
     * Sự kiện chọn số bản ghi / trang bằng combobox, emit sự kiện làm mới lại trang
     * CreatedBy: NHHung(29/08)
     */
    updatePageSize(newPageSize) {
      let vm = this;
      vm.adjustCenterButtonNumber();
      vm.$emit("onUpdatePagingInfo", vm.currentPageNumber, newPageSize);
    },

    /**
     * Cập nhật thông tin bản ghi hiển thị/ tổng số bản ghi
     * CreatedBy: NHHung(29/08)
     */
    updateDisplayText() {
      let vm = this;
      vm.startInd =
        vm.totalRecord > 0 ? (vm.currentPageNumber - 1) * vm.pageSize + 1 : 0;
      vm.endInd = Math.min(vm.startInd + vm.pageSize - 1, vm.totalRecord);
    },

    /**
     * Điều chỉnh số lượng button chọn trang hiển thị
     * Giữ cho trang hiện tại ở trung tâm
     * CreatedBy: NHHung(29/08)
     **/
    adjustCenterButtonNumber() {
      let vm = this;
      vm.currentPageNumber = Math.min(vm.currentPageNumber, vm.totalPage);

      //Điều chỉnh để luôn hiện ra tối thiểu nút trang đầu tiên / cuối cùng
      if (vm.currentPageNumber <= 2) {
        vm.leftLimit = 1;
        vm.rightLimit = vm.leftLimit + 2;
      } else if (vm.currentPageNumber + 1 >= vm.totalPage) {
        vm.rightLimit = vm.totalPage;
        vm.leftLimit = vm.rightLimit - 2;
      } else {
        vm.leftLimit = vm.currentPageNumber - 1;
        vm.rightLimit = vm.leftLimit + 2;
      }

      //Tạo mảng chứa số trang và nút ... dựa theo vị trí trang hiện tại
      let newBtnArr = [],
        btnDot;
      for (let i = 1; i <= vm.totalPage; i++) {
        if (
          i == 1 ||
          i == vm.totalPage ||
          (i >= vm.leftLimit && i <= vm.rightLimit)
        ) {
          newBtnArr.push(i);
        }
      }

      //Cập nhật lại mảng nút số trang hiển thị
      vm.pageIndexBtns = [];
      for (let i of newBtnArr) {
        if (btnDot && i - btnDot != 1) vm.pageIndexBtns.push("...");
        vm.pageIndexBtns.push(i);
        btnDot = i;
      }
      vm.updateDisplayText();
    },
  },
  watch: {
    totalRecord: function() {
      this.adjustCenterButtonNumber();
      eventBus.$emit("hideLoadingScreen", "updateTotalRecord");
    },
    totalPage: function() {
      this.adjustCenterButtonNumber();
    },
    currentPageSize: function(newPageSize) {
      this.adjustCenterButtonNumber();
      this.updatePageSize(newPageSize);
    },
  },
};
</script>
