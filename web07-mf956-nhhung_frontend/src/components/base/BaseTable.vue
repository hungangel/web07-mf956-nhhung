<template>
  <div class="table-background">
    <div
      id="gridEmployee"
      class="table-wrapper flex"
      :class="{ expanded: contentExpanded }"
    >
      <table>
        <thead>
          <tr>
            <th class="paddingdiv mw-16 white-bg bd-0"></th>
            <th
              v-for="(thItem, thIndex) in thList"
              :key="thIndex"
              :class="[thItem.thClass, { 'bd-left-dot': thIndex > 0 }]"
            >
              <div v-if="thItem.thClass == 'checkboxdiv'">
                <Checkbox
                  @cbclick="toggleSelectAllTR()"
                  :isChecked="allSelected"
                />
              </div>

              {{ thItem.fieldText }}
            </th>
          </tr>
          <span class="thspan"></span>
        </thead>
        <tbody>
          <tr
            v-for="(entity, entityIndex) in entities"
            :key="entity[itemId]"
            :class="[
              { checked: selectedSet.has(entity[itemId]) },
              ,
              { hover: entityIndex == currentHover },
            ]"
            @dblclick="dbClickOnTR(entity[itemId])"
            @mouseenter="trOnMouseEnter(entityIndex)"
            @mouseleave="trOnMouseLeave(entityIndex)"
          >
            <td class="paddingdiv mw-16  white-bg"></td>
            <td
              v-for="(thItem, thIndex) in thList"
              :key="thIndex"
              :class="[thItem.thClass, { 'bd-left-dot': thIndex > 0 }]"
            >
              <Checkbox
                @cbclick="selectThisTR(entity[itemId])"
                v-if="thItem.thClass == 'checkboxdiv'"
                :isChecked="selectedSet.has(entity[itemId])"
              />
              <div v-else>
                {{ entity[thItem.fieldName] }}
              </div>
            </td>
          </tr>
        </tbody>
      </table>

      <table class="function-table">
        <thead>
          <tr>
            <th class="a-left minw-120 flex jc-space-ar">Chức năng</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(entity, entityIndex) in entities"
            :key="entity[itemId]"
            :class="[
              { checked: selectedSet.has(entity[itemId]) },
              ,
              { hover: entityIndex == currentHover },
            ]"
            @dblclick="dbClickOnTR(entity[itemId])"
            @mouseenter="trOnMouseEnter(entityIndex)"
            @mouseleave="trOnMouseLeave(entityIndex)"
          >
            <td class=" flex jc-space-ar">
              <OptionDropdown
                :entityClass="entityClass"
                :defaultOptionText="'Sửa'"
                :optionItemValue="entity[itemId]"
                @arrowOnClick="arrowOnClick"
                @optionOnClick="optionOnClick"
              />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <OptionMenu
      :mousePos="mousePos"
      :selectedEntityId="selectedEntityId"
      :entityClass="entityClass"
      @optionOnClick="optionOnClick"
    />
  </div>
</template>

<script>
import axios from "axios";
import FormatFn from "../../scripts/common/formatfunction";
import { eventBus } from "../../main.js";
import OptionDropdown from "./optiondropdown/BaseContextButton.vue";
import Checkbox from "./BaseCheckbox.vue";
import OptionMenu from "./optiondropdown/BaseContextMenu.vue";
import URL from "../../api/config/api_config.js";
import { HTTP_STATUS, MESSAGE_MODE } from "../../scripts/enum/enumgeneral";
export default {
  name: "BaseTable",
  props: {
    warningResponse: String,
    contentExpanded: Boolean,
    thList: Array,

    //page navigation props
    pageNumber: Number,
    pageSize: Number,
    filters: Object,
    filterUpdate: Boolean,
    entityUrl: String,
    entityClass: String,
  },
  components: {
    OptionDropdown,
    OptionMenu,
    Checkbox,
  },
  data() {
    return {
      entities: [],
      selectedEntityId: "",
      currentHover: -1,
      selectedSet: new Set(),
      allSelected: false,
      mousePos: 0,
      itemId: `${this.entityClass}ID`,
    };
  },
  methods: {
    /**
     * Sự kiện đưa chuột vào / ra khỏi hàng trên bảng
     * Hển thị hiệu ứng cho hàng được hover
     * CreatedBy: NHHung(29/08)
     */
    trOnMouseEnter(entityIndex) {
      this.currentHover = entityIndex;
    },
    trOnMouseLeave() {
      this.currentHover = -1;
    },

    /**
     * Sự kiện bấm vào nút contextMenu trên từng hàng
     * Nhận id của bản ghi được bấm và hiển thị contextMenu
     * CreatedBy: NHHung(29/08)
     */
    arrowOnClick(top, selectedEntityId) {
      this.mousePos = top;
      this.selectedEntityId = selectedEntityId;
    },

    /**
     * Thực hiện reset giá trị vị trí của chuột
     * Ẩn contextMenu ( khi mousePos=0)
     * CreatedBy: NHHung(29/08)
     */
    closeOptionMenu() {
      this.mousePos = 0;
    },

    /**
     * Sự kiện khi bấm vào lựa chọn trong contextMenu
     * Emit sự kiện tương ứng với lựa chọn được bấm
     * CreatedBy: NHHung(29/08)
     */
    optionOnClick(action, selectedEntityId) {
      this.closeOptionMenu();
      let selectedEntity = this.entities.filter(
        (entity) => entity[this.itemId] == selectedEntityId
      );
      this.$emit("optionOnClick", action, selectedEntity[0]);
    },

    /**
     * Gọi hàm lấy và hiển thị dữ liệu cho danh sách
     * CreatedBy: NHHung(29/08)
     */
    async reloadTableData() {
      let vm = this;

      try {
        eventBus.$emit("showLoadingScreen");
        vm.entities = [];
        let response = await vm.getTableData();

        if (response && response.status == HTTP_STATUS.Ok) {
          //Có dữ liệu trả về, định dạng dữ liệu hiện thị
          let resData = response.data;
          vm.entities = FormatFn.formatTableData(
            resData.Entities,
            vm.entityClass
          );

          //Emit thông tin phân trang hiện tại lên Content
          this.$emit("onTableLoadDone", resData.TotalRecord, resData.TotalPage);
          // eventBus.$emit("showToastMessage", "LoadDataSuccess", "NOTIFY");
          eventBus.$emit("showToastMessage", "LoadDataSuccess",MESSAGE_MODE.Notify);
          vm.mousePos = 0;
          vm.checkTotalSelected();
        } else if (response && response.status == HTTP_STATUS.NoContent) {
          //Dữ liệu trả về rỗng
          eventBus.$emit("showToastMessage", "NoContent",MESSAGE_MODE.Alert);
          this.$emit("onTableLoadDone", 0, 1);
        }
      } catch (error) {
        eventBus.$emit(
          "showToastMessage",
          "LoadDataFailed",
          MESSAGE_MODE.Alert,
          "LoadTableData",
          error
        );
      }
      eventBus.$emit("hideLoadingScreen");

      setTimeout(() => {
        eventBus.$emit("hideLoadingScreen");
      }, 10000);
    },

    /**
     * Gọi API lấy danh sách nhân viên theo tiêu chí lọc, thông tin phân trang
     * Cập nhật số trang, bản ghi
     * CreatedBy: NHHung(29/08)
     */
    async getTableData() {
      let vm = this,
        searchKey = vm.filters.searchKey;

      let filterUrl = `${URL.BASE_URL}/${vm.entityUrl}/Filter?pageSize=${vm.pageSize}&pageNumber=${vm.pageNumber}`;
      if (searchKey) {
        filterUrl += `&searchKey=${searchKey.trim()}`;
      }
      return axios.get(filterUrl);
    },

    /**
     * Sư kiện click vào ô checkbox của một hàng
     * Thêm id của bản ghi vào trong set được chọn
     * CreatedBy: NHHung(29/08)
     */
    selectThisTR(entityId) {
      let tmpSelectedSet = this.selectedSet;
      if (tmpSelectedSet.has(entityId)) tmpSelectedSet.delete(entityId);
      else tmpSelectedSet.add(entityId);

      this.selectedSet = new Set(tmpSelectedSet);
      let totalSelected = this.checkTotalSelected();
      this.$emit("checkOnItem", totalSelected);
    },

    /**
     * Hàm đếm tổng số hàng được chọn trong bảng
     * Return số hàng được chọn trong bảng
     * CreatedBy: NHHung(29/08)
     */
    checkTotalSelected() {
      try {
        let vm = this,
          isAllSelected = true,
          selectedCount = vm.selectedSet.size;
        vm.entities.forEach((entity) => {
          if (!vm.selectedSet.has(entity[vm.itemId])) {
            isAllSelected = false;
          }
        });
        //Cập nhật trạng thái nếu tât cả các ô đã được chon
        vm.allSelected = isAllSelected;
        return selectedCount;
      } catch (error) {
        eventBus.$emit("showToastMessage", "CommonError",MESSAGE_MODE.Alert, "", error);
      }
    },

    /**
     * Khi click vào ô checkbox trên TH
     * Đánh dấu tất cả các hàng là được chọn / không chọn
     * CreatedBy: NHHung(29/08)
     */
    toggleSelectAllTR() {
      try {
        let vm = this,
          tmpSelectedSet = vm.selectedSet;

        vm.allSelected = !vm.allSelected;

        if (vm.allSelected) {
          vm.entities.forEach((entity) => {
            tmpSelectedSet.add(entity[vm.itemId]);
          });
        } else tmpSelectedSet.clear();

        vm.selectedSet = tmpSelectedSet;
        vm.$emit("checkOnItem", vm.checkTotalSelected());
      } catch (error) {
        eventBus.$emit("showToastMessage", "CommonError", MESSAGE_MODE.Alert, "", error);
      }
    },

    /**
     * Sự kiện doubleclick vào 1 hàng, mở form Edit.
     * CreatedBy: NHHung(29/08)
     */
    dbClickOnTR(itemId) {
      this.optionOnClick("RequestEdit", itemId);
    },

    /**
     * Duyệt danh sách những TR được chọn
     * Lấy ID của entity và gọi API xóa dữ liệu tương ứng
     * CreatedBy: NHHung(29/08)
     */
    async doDeleteMultiple() {
      let vm = this,
        pendingItems = [];

      for (let deleteId of vm.selectedSet) {
        pendingItems.push(deleteId);
      }
      await axios
        .post(`${URL.BASE_URL}/${vm.entityUrl}/Multiple/Delete`, pendingItems)
        .then(() => {
          eventBus.$emit("showToastMessage", "DeleteComplete", "NOTIFY");
          vm.selectedSet.clear();
          vm.$emit("checkOnItem", 0);
          vm.reloadTableData();
        })
        .catch((error) => {
          eventBus.$emit("showToastMessage", "DeleteFailed", MESSAGE_MODE.Alert, error);
        });
    },

    /**
     * Gọi API lấy mã nhân viên mới và trả về mã mới nhất
     * CreatedBy: NHHung(29/08)
     */
    getNewCode() {
      let vm = this;
      return new Promise((resolve) => {
        axios
          .get(`${URL.BASE_URL}/${vm.entityUrl}/NewCode`)
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            eventBus.$emit(
              "showToastMessage",
              "GetNewCodeFailed",
              MESSAGE_MODE.Alert,
              error
            );
          });
      });
    },

    /**
     * Gọi API lấy đối tượng theo ID
     * Trả về object chứa đối thông tin đối tượng
     * CreatedBy: NHHung(29/08)
     */
    getEntityById(replicateId) {
      let vm = this;
      return new Promise((resolve) => {
        axios
          .get(`${URL.BASE_URL}/${vm.entityUrl}/${replicateId}`)
          .then((res) => {
            resolve(res.data);
          })
          .catch((error) => {
            eventBus.$emit(
              "showToastMessage",
              "GetInfoFailed",
              MESSAGE_MODE.Alert,
              "",
              error
            );
          });
      });
    },
  },
  watch: {
    filterUpdate: function() {
      this.reloadTableData();
    },
  },
  mounted() {
    this.reloadTableData();
  },
};
</script>

<style scoped>
@import "../../css/base/table.css";
</style>
