<template>
  <div
    class="combobox-zone"
    :class="[subClass, hasError ? 'error ' : '']"
    v-on-clickaway="cbxOnClickAway"
  >
    <Tooltip :errorMessage="errorMessage" />
    <label
      >{{ labelText }}
      <span v-if="isRequired"
        ><span class="fw-400 required"> * </span></span
      ></label
    >
    <div
      :id="id"
      :class="[
        'combobox mt-4',
        subClass,
        closed ? '' : 'cb-active',
        hasError ? 'missing' : '',
      ]"
      ref="currentCombobox"
      v-on:keydown="keydownOnItem"
      style="z-index:2"
    >
      <div class="choice flex" style="z-index:5">
        <input
          ref="myself"
          type="text"
          :tabindex="tabIndex"
          class="divtext"
          style="z-index:5"
          @focus="openItems()"
          @input="cbxOnInput()"
          @blur="inputOnBlur()"
          v-model="inputValue"
          :disabled="textDisable ? true : false"
        />
        <div class="arrow-zone" @click="toggleItems()">
          <div :class="['icon-32 i-solid-arrow']"></div>
        </div>
      </div>

      <div
        class="itemwrapper"
        :class="[cbDirection, { mustshow: !closed }]"
        style="z-index:10"
      >
        <div class="item flex thead " :class="{ 'd-none': numberOfCol < 2 }">
          <div class="item-col pd-r-26">{{ thItemCode }}</div>
          <div class="item-col">{{ thItemName }}</div>
        </div>
        <div
          ref="item"
          class="item flex"
          v-for="(item, itemIndex) in items"
          :class="[
            { selected: item[itemId] == currentId },
            { highlighted: itemIndex == currentFocus },
          ]"
          :key="itemIndex"
          @click="[clickItem(itemIndex)]"
        >
          <div class="item-col  pd-r-26" :class="{ 'd-none': numberOfCol < 2 }">
            {{ item[`${entity}Code`] }}
          </div>
          <div class="item-col">{{ item[itemName] }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { mixin as clickaway } from "vue-clickaway";
import URL from "../../api/config/api_config";
import FormatFn from "../../scripts/common/formatfunction";
import Tooltip from "./BaseTooltip.vue";
import ResourceVI from "../../scripts/resource";
import { eventBus } from "../../main";
import EntityModel from "../../scripts/model/entitymodel";
export default {
  mixins: [clickaway],
  name: "BaseCombobox",
  components: {
    Tooltip,
  },
  props: {
    labelText: String,
    isRequired: Boolean,
    id: String,
    tabIndex: String,
    subClass: String,
    itemId: String,
    itemName: String,
    cbDirection: String,
    textDisable: Boolean,
    entityUrl: String,
    entity: String,
    defaultName: String,
    originValue: String,
    inputItems: Array,
    updateCombobox: Boolean,
    numberOfCol: Number,
    defaultId: Number,
  },
  data() {
    return {
      items: [],
      currentId: "-1",
      closed: true,

      currentFocus: -1,
      inputValue: null,
      errorMessage: "",
      hasError: false,
      thItemCode: "",
      thItemName: "",
    };
  },
  methods: {
    /**
     * Sự kiện bấm ra ngoài vùng combobox, đóng danh sách lựa chọn
     * CreatedBy: NHHung(29/08)
     */
    cbxOnClickAway() {
      this.closed = true;
    },

    /**
     * Thực hiện focus vào chính ô input.
     */
    doFocus(forceFocus) {
      if (forceFocus) {
        this.hasError = true;
        this.$refs.myself.focus();
      } else {
        if (this.autoFocus) {
          this.$refs.myself.focus();
        }
      }
    },

    /**
     * Sự kiện bấm vào mũi tên của combobox, mở danh sách lựa chọn
     * CreatedBy: NHHung(29/08)
     */
    toggleItems() {
      this.closed = !this.closed;
    },

    /**
     * Thực hiện mở danh sách lựa chọn
     * CreatedBy: NHHung(29/08)
     */
    openItems() {
      this.closed = false;
    },

    /**
     * Đóng combobox, hủy highlight lựa chọn trong combobox
     * CreatedBy: NHHung(29/08)
     */
    closeCombobox() {
      this.closed = true;
      this.currentFocus = -1;
    },

    /**
     * Sự kiện bấm vào 1 lựa chọn
     * Emit sự kiện chứa giá trị và id của lựa chọn vừa bấm
     * CreatedBy: NHHung(29/08)
     */
    clickItem(itemIndex) {
      let vm = this,
        item = vm.items[itemIndex];
      vm.currentId = item[vm.itemId];
      vm.inputValue = item[vm.itemName];
      
      let digitValue = Number(vm.currentId);
      if (!isNaN(digitValue)) {
        vm.$emit("input", digitValue);
      } else {
        vm.$emit("input", item[vm.itemId]);
      }

      setTimeout(() => {
        vm.closed = true;
      }, 250);
      vm.currentFocus = -1;
      vm.hasError = false;
    },

    /**
     * Sự kiện nhập trên ô nhập của combobox, highlight lựa chọn trùng khớp với ô nhập
     * CreatedBy: NHHung(29/08)
     */
    cbxOnInput() {
      let vm = this;
      vm.openItems();
      if (vm.inputValue) {
        for (let i = 0; i < vm.items.length; i++) {
          let itemName = vm.items[i][vm.itemName];
          if (FormatFn.includeIgnoreCase(itemName, vm.inputValue)) {
            vm.currentFocus = i;
            break;
          } else {
            vm.currentFocus = -1;
          }
        }
      } else {
        vm.currentFocus = -1;
        vm.inputValue = null;
      }
    },

    /**
     * Sự kiện blur khỏi ô nhập, tự động lấy lựa chọn trùng khớp với ô nhập
     * Báo lỗi nếu không có giá trị trùng khớp
     * CreatedBy: NHHung(29/08)
     */
    inputOnBlur() {
      let vm = this,
        hasMatchItem = false;

      for (let i = 0; i < vm.items.length; i++) {
        let itemName = vm.items[i][vm.itemName];
        if (FormatFn.includeIgnoreCase(itemName, vm.inputValue)) {
          vm.clickItem(i);
          hasMatchItem = true;
          break;
        }
      }

      if (!hasMatchItem) {
        vm.currentId = vm.defaultId;
        // vm.inputValue = vm.defaultName;
        vm.hasError = true;
        if (vm.inputValue) {
          vm.errorMessage = FormatFn.formatString(
            ResourceVI.PopupMessage.NotMatch,
            vm.labelText
          );
        } else {
          vm.errorMessage = FormatFn.formatString(
            ResourceVI.PopupMessage.NotNull,
            vm.labelText
          );
        }
        vm.$emit("input", null);
      }
    },

    /**
     * Thực hiện validate combobox nếu combobox bắt buộc nhập
     * trả về true nếu không bắt buộc/ nhập đúng
     * CreatedBy: NHHung(29/08)
     */
    validateInput() {
      let vm = this;
      if (vm.isRequired) {
        if (vm.currentId == "-1" || !vm.currentId) vm.hasError = true;
        else vm.hasError = false;
      }
      vm.errorMessage = FormatFn.formatString(
        ResourceVI.PopupMessage.NotNull,
        vm.labelText
      );
      if (vm.hasError) return vm.errorMessage;
      return "Correct";
    },
    resetFieldInput() {
      this.currentId = "";
      this.inputValue = "";
      this.hasError = false;
    },

    /**
     * Sự kiện keydown khi đang thao tác với combobox
     * Xử lí thay đổi / lựa chọn bằng phím mũi tên, đổi tab, đóng combobox
     * CreatedBy: NHHung(29/08)
     */
    keydownOnItem(event) {
      let vm = this;

      switch (event.key) {
        //Trường hợp bấm lên xuống chọn item
        case "ArrowDown":
          event.preventDefault();
          if (vm.currentFocus < vm.items.length - 1 && !vm.closed) {
            vm.currentFocus = vm.currentFocus + 1;
          }
          break;
        case "ArrowUp":
          event.preventDefault();
          if (vm.currentFocus > 0 && !vm.closed) {
            vm.currentFocus = vm.currentFocus - 1;
          }
          break;

        //Trường hợp bấm Enter chọn item đang focus
        case "Enter":
          event.preventDefault();
          if (vm.currentFocus >= 0) {
            vm.clickItem(vm.currentFocus);
          }
          break;
        //Trường hợp bấm Tab
        case "Tab":
          vm.closeCombobox();
          break;

        //Trường hợp bấm Esc để đóng combobox
        case "Escape":
          event.preventDefault();
          vm.closeCombobox();
          break;
      }
    },

    /**
     * Gọi API lấy dữ liệu combobox
     * CreatedBy: NHHung(29/08)
     */
    loadComboboxData() {
      let vm = this;
      // vm.items = [];
      if (vm.inputItems) {
        vm.items = vm.inputItems;
      }
      if (vm.entityUrl) {
        (async () => {
          await axios
            .get(`${URL.BASE_URL}/${vm.entityUrl}`)
            .then((response) => {
              vm.items = response.data;
            })
            .catch((error) => {
              eventBus.$emit(
                "showToastMessage",
                "CommonError",
                "LoadComboboxData",
                error
              );
            });
        })();
      }
      setTimeout(() => {
        vm.initChoice();
      }, 1000);
    },

    /**
     * Khởi tạo các giá trị cho combobox
     * Nếu là thêm mới thì làm trống các ô
     * Nếu là xem thông tin thì hiển thị thông tin hiện tại
     * CreatedBy: NHHung(29/08)
     */
    initChoice() {
      let vm = this;
      if ((vm.originValue + "").length > 0) {
        vm.items.forEach((item) => {
          if (vm.originValue == item[vm.itemId]) {
            vm.inputValue = item[vm.itemName];
            vm.currentId = item[vm.itemId];
          }
        });
      } else {
        vm.currentId = "-1";
        vm.inputValue = "  ";
      }
    },
  },
  mounted() {
    this.loadComboboxData();
    //Gán giá  trị mặc định cho combobox
    if (this.defaultName) {
      this.inputValue = this.defaultName;
    }
    //Nếu Combobox có tiêu đề cho từng cột thì hiển thị
    if (this.entity) {
      this.thItemCode =
        EntityModel[`${this.entity}`][`${this.entity}Code`].fieldText;
      this.thItemName =
        EntityModel[`${this.entity}`][`${this.entity}Name`].fieldText;
    }
  },
  watch: {
    originValue: function() {
      this.initChoice();
    },
    updateCombobox: function() {
      this.loadComboboxData();
    },
  },
};
</script>
<style scoped>
@import "../../css/base/combobox.css";
@import "../../css/common/format.css";
</style>
