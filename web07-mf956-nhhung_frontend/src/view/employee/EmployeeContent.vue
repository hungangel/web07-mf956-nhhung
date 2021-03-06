<template>
  <div class="content-grid" :class="{ contentExpanded: contentExpanded }">
    <div class="content" :class="{ expanded: contentExpanded }">
      <div class="content-header" :class="{ expanded: contentExpanded }">
        <div class="title">Nhân viên</div>

        <div class="head-button-zone">
          <Button
            subClass="fw-b"
            buttonText="Thêm mới nhân viên"
            @btnClick="showAddForm"
          />
        </div>
      </div>
      <div class="toolbar" :class="{ expanded: contentExpanded }">
        <div class="toolbar-left">
          <Button
            :subClass="
              totalSelected > 1 ? 'fw-b secondary' : 'fw-b secondary disabled'
            "
            buttonText="Thao tác hàng loạt"
            @btnClick="requestDeleteMultiple"
          />
        </div>
        <div class="toolbar-right">
          <div class="filter">
            <FieldInputIcon
              subClass="searchfield italic"
              iconName="i-search"
              placeHolder="Tìm theo Mã, Tên hoặc Số điện thoại"
              v-model="searchKey"
              @input="onUpdateFilter"
            />
            <ButtonIcon @btnClick="reloadTable" iconName="icon-24 i-refresh" :title="'Làm mới trang'"/>
            <ButtonIcon @btnClick="exportFile" iconName="icon-24 i-excel" :title="'Xuất tệp excel'"/>
          </div>
        </div>
      </div>
      <Table
        ref="ctable"
        :entityClass="'Employee'"
        entityUrl="Employees"
        :contentExpanded="contentExpanded"
        :warningResponse="warningResponse"
        :thList="thList"
        :pageSize="pageSize"
        :pageNumber="pageNumber"
        :filters="filters"
        :filterUpdate="filterUpdate"
        @optionOnClick="optionOnClick"
        @checkOnItem="checkOnItem"
        @callReloadTable="reloadTable"
        @onTableLoadDone="onTableLoadDone"
      />
      <PageNavigation
        :contentExpanded="contentExpanded"
        :totalRecord="totalRecord"
        :totalPage="totalPage"
        :pageSize="pageSize"
        :pageNumber="pageNumber"
        @onUpdatePagingInfo="onUpdatePagingInfo"
      />
    </div>
    <AddEmployeeForm
      :isHidden="isHidden"
      entityUrl="Employees"
      :employeeID="selectedEntityID"
      :formMode="formMode"
      :toggleForm="toggleForm"
      :warningResponse="warningResponse"
      :departments="departments"
      :updateDropdown="updateDropdown"
      @callReloadTable="reloadTable"
      @hideAddForm="hideAddForm"
      @changeFormMode="changeFormMode"
    />
  </div>
</template>

<script>
import axios from "axios";
import { eventBus } from "../../main.js";
import Button from "../../components/base/BaseButton.vue";
import ButtonIcon from "../../components/base/BaseButtonIcon.vue";
import FieldInputIcon from "../../components/base/BaseFieldInputIcon.vue";
import PageNavigation from "../../components/base/BasePageNavigation.vue";
import Table from "../../components/base/BaseTable.vue";
import AddEmployeeForm from "./EmployeeForm.vue";
import FormatFn from "../../scripts/common/formatfunction.js";
import ResourceVI from "../../scripts/resource.js";
import URL from "../../api/config/api_config.js";
import DefaultConfig from "../../scripts/defaultconfig.js";
import { FORM_MODE, HTTP_STATUS, MESSAGE_MODE } from "../../scripts/enum/enumgeneral.js";
export default {
  name: "EmployeePage",
  components: {
    Button,
    ButtonIcon,
    FieldInputIcon,
    Table,
    AddEmployeeForm,
    PageNavigation,
  },
  props: {
    //content props
    contentExpanded: Boolean,
  },
  data() {
    return {
      entityClass: "Employee",
      entityUrl: "Employees",

      //filter props
      searchKey: "",
      updateTime: 0,

      //dropDown props:
      departments: [],
      positions: [],
      updateDropdown: true,

      //pageNavigation props
      pageNumber: 1,
      totalPage: 1,
      totalRecord: 1,
      pageSize: 10,
      filters: {},
      filterUpdate: true,

      //table props
      thList: [],

      //form props
      isHidden: true,
      toggleForm: true,
      totalSelected: 0,
      selectedEntityID: "",
      formMode: -1,

      //popup props
      popupMessage: {},
      action: "",
      isHiddenWarning: true,
      warningResponse: "",

      //toastmessage props
      isHiddenToastMessage: true,
      toastMessage: {},
    };
  },
  methods: {
    reloadTable() {
      this.$refs.ctable.reloadTableData();
    },
    /**
     * Đổi giá trị biến để hiển thị form thông tin, thêm mới
     * gán formmode= 0 ( 0: thêm mới ,  1: cập nhật)
     * CreatedBy: NHHung(29/08)
     */
    showAddForm() {
      this.isHidden = false;
      this.formMode = FORM_MODE.Add;
      this.toggleForm = !this.toggleForm;
    },

    /**
     * Đổi giá trị biến để ẩn form thông tin
     * CreatedBy: NHHung(29/08)
     */
    hideAddForm() {
      this.isHidden = true;
      this.formMode = -1;
      this.reloadTable();
    },

    /**
     * Thay đổi chế độ của form chi tiết
     * CreatedBy: NHHung(30/08)
     */
    changeFormMode(newMode) {
      this.formMode = newMode;
    },

    /**
     * Đổi giá trị biến để hiển thị form thông tin, cập nhật
     * gán formmode= 1 ( 0: thêm mới ,  1: cập nhật)
     * CreatedBy: NHHung(29/08)
     */
    dbClickOnTR(selectedEntityID) {
      this.selectedEntityID = selectedEntityID;
      this.formMode = FORM_MODE.Add;
      this.isHidden = false;
      this.toggleForm = !this.toggleForm;
    },

    /**
     * Sự kiện bấm context menu, gọi hàm tương ứng với hành động đã bấm
     * CreatedBy: NHHung(01/09)
     */
    optionOnClick(action, selectedEntity) {
      let vm = this;
      switch (action) {
        case "RequestDelete":
          vm.requestDelete(selectedEntity);
          break;
        case "RequestEdit":
          vm.formMode = FORM_MODE.Update;
          break;
        case "RequestDuplicate":
          vm.formMode = FORM_MODE.Duplicate;
          break;
      }
      if (
        this.formMode == FORM_MODE.Update ||
        this.formMode == FORM_MODE.Duplicate
      ) {
        vm.selectedEntityID = selectedEntity[`${vm.entityClass}ID`];
        vm.isHidden = false;
        vm.toggleForm = !vm.toggleForm;
      }
    },

    /**
     *Hiển thị thông báo hỏi xóa 1 bản ghi
    CreatedBy: NHHung(01/09)
    */
    requestDelete(selectedEntity) {
      //nếu hàng này dược chọn
      let vm = this,
        action = "DeleteRecord",
        message = {
          messageType: "CONFIRM",
          textBody: FormatFn.formatString(
            ResourceVI.PopupMessage[action],
            selectedEntity[`${vm.entityClass}Code`]
          ),
        };
      vm.selectedEntityID = selectedEntity[`${vm.entityClass}ID`];
      eventBus.$emit("showPopupMessage", "FromContent", message, action);
    },

    /**
     * Hiển thị thông báo xác nhận xóa nhiều bản ghi
     * CreatedBy: NHHung(01/09)
     */
    requestDeleteMultiple() {
      //nếu hàng này dược chọn
      let message = {},
        action = "DeleteMultiple";
      message.messageType = "CONFIRM";
      message.textBody = FormatFn.formatString(
        ResourceVI.PopupMessage[action],
        this.totalSelected
      );
      eventBus.$emit("showPopupMessage", "FromContent", message, action);
    },

    /**
     * Thực hiện cập nhật số lượng item được check khi check vào checkbox, table Emit.
     */
    checkOnItem(selectedCount) {
      this.totalSelected = selectedCount;
    },

    /**
     * Sự kiện thay đổi dữ liệu lọc, cập nhật thông tin phân trang
     * CreatedBy: NHHung(30/08)
     */
    onUpdateFilter() {
      this.onUpdatePagingInfo(this.pageNumber, this.pageSize);
    },

    /**
     * Sự kiện bấm vào nút chuyển trang, gọi hàm làm mới bảng
     * CreatedBy: NHHung(30/08)
     **/
    onUpdatePagingInfo(pageNumber, pageSize) {
      this.pageNumber = pageNumber;
      this.pageSize = pageSize;
      this.filters = {
        searchKey: this.searchKey,
      };
      //Thay đổi trạng thái để kích hoạt sự kiện tải lại trang trong table
      setTimeout(() => {
        this.reloadTable();
      }, 200);
    },

    /**
     * Sư kiện sau khi  tải xong dữ liệu
     * Cập nhật số trang , số bản ghi
     * CreatedBy: NHHung(31/08)
     */
    onTableLoadDone(totalRecord, totalPage) {
      this.totalRecord = totalRecord;
      this.totalPage = totalPage;
      this.pageNumber = Math.min(totalPage, this.pageNumber);
    },

    /**
     * Thực hiện gọi API xuất bản dữ liệu
     * CreatedBy: NHHung(02/09)
     */
    async exportFile() {
      try {
        let response = await this.getExportFile();
        if (response.status == HTTP_STATUS.Ok) {
          //Nếu có dữ liệu trả về thì tạo element cho phép tải tệp
          let url = window.URL.createObjectURL(new Blob([response.data])),
            a = document.createElement("a"),
            filename = DefaultConfig.ExportFileName;

          a.href = url;
          a.setAttribute("download", filename);
          document.body.appendChild(a);
          a.click();
          a.remove();
        } else {
          //Thông báo lỗi nếu không có dữ liệu
          eventBus.$emit("showToastMessage", "NoContent");
        }
      } catch (error) {
        eventBus.$emit(
          "showToastMessage",
          "ExportFileFailed",
          MESSAGE_MODE.Alert,
          "ExportFileFailed",
          error
        );
      }
    },

    /**
     * Gọi API lấy dữ liệu tệp xuất khẩu
     * CreatedBy: NHHung(30/08)
     */
    async getExportFile() {
      let vm = this,
        isAllPage = "true",
        searchKey = vm.filters.searchKey,
        //Các trường được xuất thông tin
        propNames = DefaultConfig.ExportField,
        filterUrl =
          `${URL.BASE_URL}/${vm.entityUrl}/Export?` +
          `pageSize=${vm.pageSize}&pageNumber=${vm.pageNumber}&isAllPage=${isAllPage}`;

      if (searchKey) {
        filterUrl += `&searchKey=${searchKey.trim()}`;
      }
      //Kiểu dữ liệu trả về là đối tượng nhị phân của tệp xuất khẩu
      return axios.post(filterUrl, propNames, {
        responseType: "blob",
      });
    },

    /**
     * Thực hiện gọi API xóa 1 bản ghi theo ID
     * CreatedBy: NHHung(01/09)
     */
    async doDeleteEntity() {
      let vm = this;
      await axios
        .delete(`${URL.BASE_URL}/${vm.entityUrl}/${vm.selectedEntityID}`)
        .then(() => {
          eventBus.$emit("showToastMessage", "DeleteComplete");
          vm.$refs.ctable.selectThisTR(vm.selectedEntityID)
          vm.reloadTable();
        })
        .catch((error) => {
          eventBus.$emit(
            "showToastMessage",
            "DeleteFailed",
            MESSAGE_MODE.Alert,
            "DeleteFailed",
            error
          );
        });
    },

    /**
     * Thực hiện hành động tương ứng với kết quả nhận về sau khi confirm popup message
     * CreatedBy: NHHung(01/09)
     */
    async processPopupResponse(action, choice) {
      let vm = this;
      if (choice == "CONFIRM") {
        switch (action) {
          case "DeleteRecord":
            vm.doDeleteEntity();
            break;
          case "DeleteMultiple":
            await vm.$refs.ctable.doDeleteMultiple();
            break;
        }
      }
    },
  },

  created() {
    //Bắt sự kiện phản hồi từ popup message
    eventBus.$on("FromContentPopupResponse", (action, choice) => {
      this.processPopupResponse(action, choice);
    });
  },
  mounted() {
    //Mảng các cột của table hiển thị
    this.thList = DefaultConfig.TableColumn;
  },
};
</script>

<style scoped>
@import "../../css/layout/content.css";
.footer .navigation-button .button {
  width: 32px !important;
  min-width: 32px !important;
}
</style>
