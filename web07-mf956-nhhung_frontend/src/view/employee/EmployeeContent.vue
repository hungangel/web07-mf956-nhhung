<template>
  <div class="content-grid" :class="{ contentExpanded: contentExpanded }">
    <div class="content" :class="{ expanded: contentExpanded }">
      <div class="content-header" :class="{ expanded: contentExpanded }">
        <div class="title">Nhân viên</div>

        <div class="head-button-zone">
          <Button
            subClass="add-entity fw-b"
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
          <!-- <ButtonIcon @btnClick="reloadTable" iconName="icon-24 i-refresh" /> -->
        </div>
        <div class="toolbar-right">
          <div class="filter">
            <FieldInputIcon
              subClass="searchfield italic"
              iconName="i-search"
              placeHolder="Tìm theo Mã, Tên hoặc Số điện thoại"
              v-model="searchboxFilter"
              @input="callFilterFunction"
            />
            <ButtonIcon @btnClick="reloadTable" iconName="icon-24 i-refresh" />
            <ButtonIcon @btnClick="exportFile" iconName="icon-24 i-excel" />
          </div>
        </div>
      </div>
      <Table
        ref="ctable"
        :entity="entity"
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
import AddEmployeeForm from "./AddEmployeeForm.vue";
import FormatFn from "../../scripts/FormatFunction.js";
import ResourceVI from "../../scripts/ResourceVI.js";
import Constant from "../../api/config/APIConfig.js";
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
      entity: "Employee",
      entityUrl: "Employees",
      //filter props
      searchboxFilter: "",
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
      this.$refs.ctable.loadTableData();
    },
    /**
     * Đổi giá trị biến để hiển thị form thông tin, thêm mới
     * gán formmode= 0 ( 0: thêm mới ,  1: cập nhật)
     * CreatedBy: NHHung(29/08)
     */
    showAddForm() {
      this.isHidden = false;
      this.formMode = 0;
      this.toggleForm = !this.toggleForm;
    },

    /**
     * Đổi giá trị biến để ẩn form thông tin
     * CreatedBy: NHHung(29/08)
     */
    hideAddForm() {
      this.isHidden = true;
      this.formMode = -1;
    },

    /**
     * Đổi giá trị biến để hiển thị form thông tin, cập nhật
     * gán formmode= 1 ( 0: thêm mới ,  1: cập nhật)
     * CreatedBy: NHHung(29/08)
     */
    dbClickOnTR(selectedEntityID) {
      this.selectedEntityID = selectedEntityID;
      this.formMode = 1;
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
        case "ReqDelete":
          vm.requestDelete(selectedEntity);
          break;
        case "ReqEdit":
          vm.formMode = 1;
          break;
        case "ReqDuplicate":
          vm.formMode = 2;
          break;
      }
      if (this.formMode == 1 || this.formMode == 2) {
        vm.selectedEntityID = selectedEntity[`${vm.entity}ID`];
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
            ResourceVI.EntityName[vm.entity],
            selectedEntity[`${vm.entity}Code`]
          ),
        };
      vm.selectedEntityID = selectedEntity[`${vm.entity}ID`];
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
        this.totalSelected,
        ResourceVI.EntityName[this.entity]
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
     * Nghe sự kiện bấm vào nút chuyển trang
     * Gọi component table tải trang tương ứngo
     **/
    onUpdatePagingInfo(pageNumber, pageSize) {
      this.pageNumber = pageNumber;
      this.pageSize = pageSize;
      this.callFilterFunction();
    },

    /**
     * Nghe sự kiện bấm vào nút chuyển trang
     * Gọi component table tải trang tương ứng
     **/
    callFilterFunction() {
      this.filters = {
        searchboxFilter: this.searchboxFilter,
      };
      this.filterUpdate = !this.filterUpdate;
      // this.$refs.ctable.loadTableData();

      // setTimeout(() => {
      //   eventBus.$emit("showLoadingScreen",);
      // }, 2000);
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
    exportFile() {
      let vm = this,
        searchboxFilter = vm.filters.searchboxFilter;
      //Các trường được xuất thông tin
      let isAllPage = "true",
        propNames = [
          "EmployeeCode",
          "FullName",
          "GenderName",
          "DateOfBirth",
          "PositionName",
          "DepartmentName",
          "BankAccountNumber",
          "BankName",
        ];
      vm.entities = {};

      let filterUrl =
        `https://localhost:44346/api/v1/Employees/Export?` +
        `pageSize=${vm.pageSize}&pageNumber=${vm.pageNumber}&isAllPage=${isAllPage}`;

      if (searchboxFilter) {
        filterUrl += `&searchKey=${searchboxFilter.trim()}`;
      }

      axios
        // add responseType
        .post(filterUrl, propNames, {
          responseType: "blob",
        })
        .then((response) => {
          if (response.status == 200) {
            //Nếu có dữ liệu trả về thì tạo element cho phép tải tệp
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const a = document.createElement("a");
            a.href = url;
            const filename = `file.xlsx`;
            a.setAttribute("download", filename);
            document.body.appendChild(a);
            a.click();
            a.remove();
          } else {
            eventBus.$emit("showToastMessage", "NoContent");
            //todo
          }
        })
        .catch((error) => {
          eventBus.$emit(
            "showToastMessage",
            "ExportFileFailed",
            "ALERT",
            "ExportFileFailed",
            error
          );
        });
    },

    /**
     * Thực hiện gọi API xóa 1 bản ghi theo ID
     * CreatedBy: NHHung(01/09)
     */
    async doDeleteEntity() {
      let vm = this;
      await axios
        .delete(`${Constant.BaseUrl}/${vm.entityUrl}/${vm.selectedEntityID}`)
        .then(() => {
          eventBus.$emit("showToastMessage", "DeleteComplete");
          vm.$refs.ctable.loadTableData();
        })
        .catch((error) => {
          eventBus.$emit(
            "showToastMessage",
            "DeleteFailed",
            "ALERT",
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
    eventBus.$on("FromContentPopupResponse", (action, choice) => {
      this.processPopupResponse(action, choice);
    });
  },
  mounted() {
    //Mảng các cột của table hiển thị
    this.thList = [
      {
        fieldName: "No",
        dataType: "Checkbox",
        fieldText: "",
        thClass: "checkboxdiv",
      },
      {
        fieldName: "EmployeeCode",
        dataType: "Text",
        fieldText: "MÃ NHÂN VIÊN",
        thClass: "a-left minw-150",
      },
      {
        fieldName: "FullName",
        dataType: "Text",
        fieldText: "TÊN NHÂN VIÊN",
        thClass: "a-left minw-250",
      },
      {
        fieldName: "GenderName",
        dataType: "Text",
        fieldText: "GIỚI TÍNH",
        thClass: "a-left minw-100",
      },
      {
        fieldName: "DateOfBirth",
        dataType: "Date",
        fieldText: "NGÀY SINH",
        thClass: "a-center minw-150",
      },
      {
        fieldName: "IdentityNumber",
        dataType: "Text",
        fieldText: "SỐ CMND",
        thClass: "a-left minw-200",
      },
      {
        fieldName: "PositionName",
        dataType: "Text",
        fieldText: "CHỨC DANH",
        thClass: "a-left minw-250",
      },
      {
        fieldName: "DepartmentName",
        dataType: "Text",
        fieldText: "TÊN ĐƠN VỊ",
        thClass: "a-left minw-250",
      },
      {
        fieldName: "BankAccountNumber",
        dataType: "Text",
        fieldText: "SỐ TÀI KHOẢN",
        thClass: "a-left minw-150",
      },
      {
        fieldName: "BankName",
        dataType: "Text",
        fieldText: "TÊN NGÂN HÀNG",
        thClass: "a-left minw-250",
      },
      {
        fieldName: "BankBranch",
        dataType: "Text",
        fieldText: "CHI NHÁNH TK NGÂN HÀNG",
        thClass: "a-left minw-250",
      },
      {
        fieldName: "MobilePhoneNumber",
        dataType: "Text",
        fieldText: "ĐT DI ĐỘNG",
        thClass: "a-left minw-150",
      },
      {
        fieldName: "LandlinePhoneNumber",
        dataType: "Text",
        fieldText: "ĐT CỐ ĐỊNH",
        thClass: "a-left minw-150",
      },
    ];
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
