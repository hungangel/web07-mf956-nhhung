<template>
  <div :class="{ 'd-none': isHidden }">
    <div class="modal-background" @keydown="handleKeydown"></div>
    <div class="modal-zone" entityId="manv" @keydown="handleKeydown">
      <div class="modal">
        <div class="md-header">
          <div class="flex">
            <div class="title">Thông tin nhân viên</div>
            <Checkbox
              @cbclick="checkIsCustomer"
              :isChecked="isCustomer"
              :checkboxText="'Là khách hàng'"
            />
            <Checkbox
              @cbclick="checkIsProvider"
              :isChecked="isProvider"
              :checkboxText="'Là nhà cung cấp'"
            />
          </div>

          <div class="flex">
            <div class="btn icon-24 i-question" />
            <div class="btn icon-24 i-close" @click="btnCloseOnClick" />
          </div>
        </div>
        <div class="md-content-grid" ref="detail">
          <div class="md-content-row h-50p flex">
            <div class="md-content-col pd-r-26">
              <div class="md-input-row flex">
                <FieldInputLabel
                  labelText="Mã"
                  :isRequired="true"
                  :maxLength="20"
                  :autoFocus="true"
                  :isShowed="isShowed"
                  dataType="EmployeeCode"
                  subClass="w-40p pd-r-6"
                  v-model="employee.EmployeeCode"
                  :originValue="employee.EmployeeCode"
                  :validate="triggerValidate"
                  ref="validateFieldCode"
                />

                <FieldInputLabel
                  labelText="Tên"
                  :isRequired="true"
                  :maxLength="100"
                  dataType="PersonName"
                  subClass="w-60p"
                  v-model="employee.FullName"
                  :originValue="employee.FullName"
                  ref="validateFieldFullName"
                />
              </div>
              <div class="md-input-row flex">
                <ComboBox
                  labelText="Đơn vị"
                  subClass="w-100p"
                  itemId="DepartmentID"
                  itemName="DepartmentName"
                  entityUrl="Departments"
                  entity="Department"
                  ddDirection="top-32"
                  :isRequired="true"
                  :selectedId="'0'"
                  v-model="employee.DepartmentID"
                  :originValue="employee.DepartmentID + ''"
                  :updateCombobox="updateCombobox"
                  :numberOfCol="2"
                  ref="validateFieldDep"
                />
              </div>
              <div class="md-input-row flex">
                <FieldInputLabel
                  labelText="Chức danh"
                  :maxLength="100"
                  v-model="employee.PositionName"
                  :originValue="employee.PositionName"
                  :validate="triggerValidate"
                />
              </div>
            </div>

            <div class="md-content-col">
              <div class="md-input-row flex">
                <div class="flex-col">
                  <label for="">Ngày sinh</label>
                  <DatePicker
                    v-model="employee.DateOfBirth"
                    :format="defaultDateFormat"
                    :value-type="'YYYY-MM-DD'"
                    :placeholder="defaultDateFormat"
                    :disabled-date="(date) => date >= new Date()"
                    style="width: 100%; outline-color: #2ca01c"
                  >
                    <FieldInputLabel
                      :inputType="'date'"
                      subClass="w-40p pd-r-6"
                      v-model="employee.DateOfBirth"
                      :originValue="employee.DateOfBirth"
                      :validate="triggerValidate"
                    />
                  </DatePicker>
                </div>

                <RadioButton
                  labelText="Giới tính"
                  subClass="w-60p pd-l-10"
                  v-model="employee.Gender"
                  :originValue="employee.Gender + ''"
                />
              </div>
              <div class="md-input-row flex">
                <FieldInputLabel
                  labelText="Số CMND"
                  :maxLength="25"
                  subClass="w-60p pd-r-6"
                  v-model="employee.IdentityNumber"
                  :originValue="employee.IdentityNumber"
                  :validate="triggerValidate"
                />

                <div class="flex-col">
                  <label for="">Ngày cấp</label>
                  <DatePicker
                    v-model="employee.IdentityDate"
                    :format="defaultDateFormat"
                    :value-type="'YYYY-MM-DD'"
                    :placeholder="defaultDateFormat"
                    :disabled-date="(date) => date >= new Date()"
                    style="width: 100%; outline-color: #2ca01c"
                  >
                    <FieldInputLabel
                      :inputType="'date'"
                      subClass="w-40p pd-r-6"
                      v-model="employee.IdentityDate"
                      :originValue="employee.IdentityDate"
                      :validate="triggerValidate"
                    />
                  </DatePicker>
                </div>
              </div>
              <div class="md-input-row flex">
                <FieldInputLabel
                  labelText="Nơi cấp"
                  :maxLength="255"
                  v-model="employee.IdentityPlace"
                  :originValue="employee.IdentityPlace"
                />
              </div>
            </div>
          </div>

          <div class="md-content-row h-50p mt-20 flex-col">
            <div class="md-content-row">
              <div class="md-input-row">
                <FieldInputLabel
                  labelText="Địa chỉ"
                  :maxLength="255"
                  v-model="employee.Address"
                  :originValue="employee.Address"
                />
              </div>
            </div>
            <div class="md-content-row flex">
              <div class="md-content-col flex">
                <div class="md-input-row w-50p pd-r-6">
                  <FieldInputLabel
                    labelText="ĐT di động"
                    :maxLength="50"
                    v-model="employee.MobilePhoneNumber"
                    :originValue="employee.MobilePhoneNumber"
                  />
                </div>
                <div class="md-input-row w-50p pd-r-6">
                  <FieldInputLabel
                    labelText="ĐT cố định"
                    :maxLength="50"
                    v-model="employee.LandlinePhoneNumber"
                    :originValue="employee.LandlinePhoneNumber"
                  />
                </div>
              </div>
              <div class="md-content-col">
                <div class="md-input-row  w-50p">
                  <FieldInputLabel
                    ref="validateFieldEmail"
                    labelText="Email"
                    :maxLength="100"
                    dataType="Email"
                    v-model="employee.Email"
                    :originValue="employee.Email"
                    :validate="triggerValidate"
                  />
                </div>
              </div>
            </div>
            <div class="md-content-row flex">
              <div class="md-content-col flex">
                <div class="md-input-row w-50p pd-r-6">
                  <FieldInputLabel
                    labelText="Tài khoản ngân hàng"
                    :maxLength="25"
                    v-model="employee.BankAccountNumber"
                    :originValue="employee.BankAccountNumber"
                  />
                </div>
                <div class="md-input-row w-50p pd-r-6">
                  <FieldInputLabel
                    labelText="Tên ngân hàng"
                    :maxLength="255"
                    v-model="employee.BankName"
                    :originValue="employee.BankName"
                  />
                </div>
              </div>
              <div class="md-content-col">
                <div class="md-input-row  w-50p">
                  <FieldInputLabel
                    labelText="Chi nhánh"
                    :maxLength="255"
                    v-model="employee.BankBranch"
                    :originValue="employee.BankBranch"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="md-footer">
          <div class="footer-top"></div>
          <div class="footer-bottom">
            <Button
              subClass="secondary fw-700 cancel"
              buttonText="Hủy"
              :tabIndex="1"
              @btnClick="btnCloseOnClick"
            />
            <div class="flex">
              <Button
                subClass="secondary fw-700"
                buttonText="Cất"
                @btnClick="btnSaveOnClick('SaveAndClose')"
              />
              <Button
                subClass="primary fw-700 ml-10  btn-save "
                buttonText="Cất và Thêm"
                @btnClick="btnSaveOnClick('SaveAndAdd')"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";
import axios from "axios";
import FormatFn from "../../scripts/common/formatfunction";
import Checkbox from "../../components/base/BaseCheckbox.vue";
import RadioButton from "../../components/base/BaseRadioButton.vue";
import FieldInputLabel from "../../components/base/BaseFieldInputLabel.vue";
import ComboBox from "../../components/base/BaseComboBox.vue";
import Button from "../../components/base/BaseButton.vue";
import { eventBus } from "../../main.js";
import ResourceVI from "../../scripts/resource.js";
import URL from "../../api/config/api_config.js";
import DefaultConfig from "../../scripts/defaultconfig.js";
import {
  FORM_MODE,
  HTTP_STATUS,
  MESSAGE_MODE,
  VALIDATE_CODE,
} from "../../scripts/enum/enumgeneral";

export default {
  mixins: [FormatFn],
  name: "EmployeeForm",
  components: {
    ComboBox,
    FieldInputLabel,
    DatePicker,
    Button,
    Checkbox,
    RadioButton,
  },
  data() {
    return {
      originEntity: {},
      employee: {},
      defaultDateFormat: DefaultConfig.DateFormat,
      isShowed: true,
      updateCombobox: true,
      triggerValidate: true,
      firstErrorField: null,
      entity: "Employee",
      firstErrorMessage: "",
      keyCombination: "",

      isCustomer: false,
      isProvider: false,

      validateFields: [
        "validateFieldCode",
        "validateFieldFullName",
        "validateFieldDep",
        "validateFieldEmail",
      ],
    };
  },
  props: {
    isHidden: Boolean,
    employeeID: String,
    formMode: Number,
    toggleForm: Boolean,
    warningResponse: String,
    thList: Object,
    entityUrl: String,
  },
  methods: {
    //#region Sự kiện trên các nút / components

    /**
     * Sự kiện bấm ô tích chọn là khách hàng
     * CreatedBy: NHHung(02/09)
     */
    checkIsCustomer() {
      this.isCustomer = !this.isCustomer;
    },

    /**
     * Sự kiện bấm ô tích chọn là nhà cung cấp
     * CreatedBy: NHHung(02/09)
     */
    checkIsProvider() {
      this.isProvider = !this.isProvider;
    },

    /**
     * Xử lí sự kiện keydown trên modal
     * CreatedBy: NHHung(30/08)
     */
    handleKeydown() {
      // let vm = this,
      //   keyName = event.key,
      //   isCorrectCombine = false;
      // console.log(keyName);
      // if (keyName == "Control" || keyName == "Control") {
      //   vm.keyCombination += keyName;
      //   isCorrectCombine = false;
      // }
      // if (
      //   vm.keyCombination == "Control" &&
      //   (keyName == "s" || keyName == "S")
      // ) {
      //   event.preventDefault();
      //   isCorrectCombine = true;
      //   console.log("save");
      // }
      // if (
      //   vm.keyCombination == "ControlShift" &&
      //   (keyName == "s" || keyName == "S")
      // ) {
      //   event.preventDefault();
      //   isCorrectCombine = true;
      //   console.log("savee addd");
      // }
      // if (!isCorrectCombine) {
      //   vm.keyCombination = "";
      //   console.log("reset combine");
      // }
    },

    /**
     * Thực thi cho sự kiện do nút save emit, mặc định thêm và đóng .
     * Kiểm tra trạng thái form thêm mới (0) hay cập nhật (1)
     * Thực hiện validate và hiện popup confirm / thông báo lỗi
     *  CreatedBy: NHHung(29/08)
     */
    async btnSaveOnClick(formAction) {
      try {
        let vm = this,
          hasErrorField = await vm.validateFormData();

        if (!hasErrorField && vm.formMode == FORM_MODE.Update) {
          formAction = formAction.replace("Save", "Update");
          vm.doUpdateEntity(formAction);
        } else if (!hasErrorField) {
          vm.doSaveEntity(formAction);
        }
      } catch (error) {
        eventBus.$emit(
          "showToastMessage",
          "CommonError",
          MESSAGE_MODE.Alert,
          "CheckExistingCode",
          error
        );
      }
    },

    /**
     * Sự kiện bấm nút đóng form
     * Thực hiện kiểm tra form có bị thay đổi không, hiển thị popup confirm nếu có thay đổi
     * CreatedBy: NHHung(29/08)
     */
    btnCloseOnClick() {
      //Kiểm tra trạng thái thay đổi của form
      let isModifiedForm = this.checkModifiedEntity();

      if (isModifiedForm) {
        this.createPopupMessage("FULL", "CloseModifiedForm");
      } else {
        this.resetEntityData();
        this.$emit("hideAddForm");
      }
    },

    //#endregion sự kiện trên các nút/ components

    /**
     * Kiểm tra xem thông in đối tượng đã bị thay đổi chưa
     * CreatedBy: NHHung(29/08)
     */
    checkModifiedEntity() {
      let vm = this;
      for (let key in vm.employee) {
        //Nếu dữ liệu so sánh và dữ liệu hiện tại là trống / null thì bỏ qua
        if (
          (vm.employee[key] == null || vm.employee[key] == "") &&
          (vm.originEntity[key] == null || vm.originEntity[key] == "")
        ) {
          continue;
        }
        if (vm.employee[key] != vm.originEntity[key]) return true;
      }
      return false;
    },

    /**
     * Kiểm tra các trường dữ liệu cần validate
     * Xác định trường đầu tiên bị lỗi và thông báo lỗi
     * CreatedBy: NHHung(29/08)
     */
    async validateFormData() {
      let vm = this,
        errorField = null,
        errorMessage = null;

      //Validate định dạng các trường nhập, bắt buộc
      // for (let [key] of Object.entries(vm.$refs)) {
      vm.validateFields.forEach((key) => {
        let validateMsg = vm.$refs[key].validateInput();
        if (validateMsg != "Correct" && errorField == null) {
          errorField = key;
          errorMessage = validateMsg;
        }
      });

      //Validate trùng mã
      if (!errorField) {
        let validateExistCode = await vm.checkExistingCode(
          vm.employee.EmployeeCode
        );
        if (!validateExistCode) {
          errorField = "validateFieldCode";
          vm.createPopupMessage(
            "NOTIFY",
            "Duplicated",
            vm.employee.EmployeeCode
          );
          vm.$refs.validateFieldCode.errorCode = VALIDATE_CODE.Existed;
        }
      } else if (errorField && errorMessage) {
        vm.createPopupMessage("ALERT", "ValidateOnSave", errorMessage, true);
      }
      vm.firstErrorField = errorField;
      vm.firstErrorMessage = errorMessage;
      return errorField;
    },

    /**
     * Focus vào ô bị lỗi đầu tiên
     * CreatedBy: NHHung(01/09)
     */
    focusOnFirstErrorField() {
      if (this.firstErrorField) this.$refs[this.firstErrorField].doFocus(true);
    },

    /**
     * Xử lí khi nhận phản hồi từ popup messsage
     * CreatedBy: NHHung(01/09)
     */
    async processPopupResponse(action, choice) {
      let vm = this;
      vm.focusOnFirstErrorField();
      if (choice == "CONFIRM") {
        if (action == "CloseModifiedForm") {
          if (vm.formMode == FORM_MODE.Update) {
            await vm.btnSaveOnClick("UpdateAndClose");
          } else {
            await vm.btnSaveOnClick("SaveAndClose");
          }
        }
      }
      //Trường hợp từ chối thì đóng form và reset dữ liệu trên form
      if (choice == "DECLINE") {
        vm.resetEntityData();
        vm.$emit("hideAddForm");
      }
    },

    /**
     * GỌi API lưu dữ liệu trên form
     * Thông báo kết quả thực hiện
     * CreatedBy: NHHung(31/08)
     */
    async doSaveEntity(formAction) {
      let vm = this,
        isValid = true;
      await axios
        .post(`${URL.BASE_URL}/${vm.entityUrl}/`, vm.employee)
        .then((response) => {
          vm.processSaveResponse("AddSuccess", isValid, response);
          vm.processAfterSave(formAction);
        })
        .catch((error) => {
          isValid = false;
          vm.processSaveResponse("AddFailed", isValid, error);
        });
    },

    /**
     * Thực hiện cập nhật thông tin đối tượng
     * THông báo kết quả thực hiện
     * CreatedBy: NHHung(31/08)
     */
    async doUpdateEntity(formAction) {
      let vm = this,
        isValid = true;
      await axios
        .put(`${URL.BASE_URL}/${vm.entityUrl}/${vm.employeeID}`, vm.employee)
        .then((response) => {
          vm.processSaveResponse("UpdateSuccess", isValid, response);
          vm.processAfterSave(formAction);
        })
        .catch((error) => {
          isValid = false;
          vm.processSaveResponse("UpdateFailed", isValid, error);
        });
    },

    /**
     * Goi API thực hiện kiểm tra mã trùng
     * Trả về kết quả kiểm tra
     * CreatedBy: NHHung(01/09)
     */
    checkExistingCode(entityCode) {
      let vm = this;
      return new Promise((resolve) => {
        axios
          .get(
            `${URL.BASE_URL}/${vm.entityUrl}/CheckExist?entityCode=${entityCode}`
          )
          .then((response) => {
            if (response.status == HTTP_STATUS.Ok) {
              if (
                vm.formMode == FORM_MODE.Add ||
                vm.formMode == FORM_MODE.Duplicate
              ) {
                resolve(false);
              } else {
                let isExsisted = true,
                  existingEntities = response.data;
                existingEntities.forEach((entity) => {
                  if (entity[`${vm.entity}ID`] != vm.employeeID) {
                    isExsisted = false;
                  }
                });
                resolve(isExsisted);
              }
            } else {
              resolve(true);
            }
          })
          .catch((error) => {
            eventBus.$emit(
              "showToastMessage",
              "CommonError",
              MESSAGE_MODE.Alert,
              "CheckExistingCode",
              error
            );
            resolve(false);
          });
      });
    },

    /**
     * Gọi API lấy mã mới cho đối tượng
     * Trả về mã lấy được
     * CreatedBy: NHHung(31/08)
     */
    async getNewEntityCode() {
      let vm = this;
      try {
        let response = await axios.get(
          `${URL.BASE_URL}/${vm.entityUrl}/NewCode`
        );
        return response.data;
      } catch (error) {
        eventBus.$emit(
          "showToastMessage",
          "GetInfoFailed",
          MESSAGE_MODE.Alert,
          "GetInfo",
          error
        );

        return {};
      }
    },

    /**
     * GỌi API lấy thông tin đối tượng theo ID truyền vào
     * trả về thông tin đối tượn
     * CreatedBy: NHHung(01/09)
     */
    async getEntityInfo() {
      let vm = this;
      try {
        let response = await axios.get(
          `${URL.BASE_URL}/${vm.entityUrl}/${vm.employeeID}`
        );
        return response.data;
      } catch (error) {
        eventBus.$emit(
          "showToastMessage",
          "GetInfoFailed",
          MESSAGE_MODE.Alert,
          "GetInfo",
          error
        );

        return {};
      }
    },

    /**
     * Gán giá trị ban đầu của đối tượng
     * Sử dụng khi kiểm tra dữ liệu thay đổi
     * CreatedBy: NHHung(01/09)
     */
    assignOriginEntity() {
      this.originEntity = {};
      Object.assign(this.originEntity, this.employee);
    },

    /**
     * Làm mới dữ liệu trên các ô nhập
     * CreatedBy: NHHung(01/09)
     */
    resetEntityData() {
      let vm = this,
        newEntity = {};
      vm.employee = newEntity;

      //Xóa dữ liệu trên các ô input
      for (let [key] of Object.entries(vm.$refs)) {
        if (key.includes("validate")) {
          vm.$refs[key].resetFieldInput();
        }
      }
    },

    /**
     * Xử lí sau khi lưu, dựa theo hành động lưu và đóng hay lưu và thêm mới
     * Thực hiện đóng form hoặc lấy dữ liệu mới
     */
    processAfterSave(formAction) {
      let vm = this;
      vm.resetEntityData();
      this.$emit("changeFormMode", FORM_MODE.Add);
      if (formAction.includes("Close")) {
        vm.$emit("hideAddForm");
      } else {
        (async () => {
          let newEntityCode = await vm.getNewEntityCode(),
            newEntity = {};

          newEntity[`${vm.entity}Code`] = newEntityCode;
          vm.employee = newEntity;
          vm.assignOriginEntity();
          vm.isShowed = !vm.isShowed;
        })();
      }
      vm.$emit("callReloadTable");
    },
    //#endregion

    //#region các hàm tạo toast message  , popup
    /**
     * Hiển thị toast message trong response trả về của server nếu có
     * Nếu không hiện thị thông báo mặc định theo hành động gọi
     * CreatedBy: NHHung(01/09)
     */
    processSaveResponse(actionResult, isValid, response) {
      let vm = this,
        responseData = {};
      if (isValid) {
        eventBus.$emit("showToastMessage", actionResult, "NOTIFY");
      } else if (response && (response.data || response.response.data)) {
        responseData = response.response.data;
        vm.createMessageFromResponse(actionResult, isValid, responseData);
      }
    },

    /**
     * Tạo Message popup và hiển thị
     * CreatedBy: NHHung(05/09/2021)
     */
    createPopupMessage(messageType, action, params, isCustomMessage) {
      if (!isCustomMessage) {
        let textBody = FormatFn.formatString(
            ResourceVI.PopupMessage[action],
            params
          ),
          message = {
            messageType,
            textBody,
          };
        eventBus.$emit("showPopupMessage", "FromAddForm", message, action);
      } else {
        let message = {
          messageType,
          textBody: params,
        };
        setTimeout(() => {
          eventBus.$emit("showPopupMessage", "FromAddForm", message, action);
        }, 200);
      }
    },

    showPopupMessage(message, action) {
      eventBus.$emit("showPopupMessage", "FromAddForm", message, action);
    },

    /**
     * Hiển thị popup message trong response trả về của server nếu có
     * Nếu không hiện thị thông báo mặc định theo hành động gọi
     * CreatedBy: NHHung(01/09)
     */
    createMessageFromResponse(actionResult, isValid, responseData) {
      let messageType = "",
        textBody = "";

      if (isValid) {
        messageType = "NOTIFY";
        textBody = responseData.userMsg
          ? responseData.userMsg
          : ResourceVI.PopupMessage[actionResult];
      } else {
        if (responseData) {
          messageType = MESSAGE_MODE.Alert;
          textBody = responseData.userMsg
            ? responseData.userMsg
            : ResourceVI.PopupMessage[actionResult];
        } else {
          textBody = ResourceVI.PopupMessage[actionResult];
        }
      }
      this.createPopupMessage(messageType, textBody, "", true);
      // let popupMessage = { messageType, textBody };
      // this.showPopupMessage(popupMessage);
    },
    //#endregion

    /**
     * Thực hiện tải dữ liệu ban đầu cho form
     * Khi mở form. dựa theo form mode: 0 : thêm mới , 1: Sửa, 2: Nhân bản
     * CreatedBy: NHHung(01/09)
     */
    preLoadFormData() {
      let vm = this;

      (async () => {
        //Autofocus vào ô có ref= "autofocus"
        vm.isShowed = !vm.isShowed;
        vm.updateCombobox = !vm.updateCombobox;
        let entityInfo = {},
          newEntity = {},
          newEntityCode = "";

        //Tải dữ liệu phụ thuộc formmode
        switch (vm.formMode) {
          case FORM_MODE.Add:
            //Lấy mã mới cho form
            newEntityCode = await vm.getNewEntityCode();
            newEntity[`${vm.entity}Code`] = newEntityCode;
            vm.employee = newEntity;
            vm.assignOriginEntity();
            break;

          case FORM_MODE.Update:
            //Lấy thông tin đối tượng lên trên form
            vm.employee = await vm.getEntityInfo();
            vm.employee = FormatFn.formatEntityData(vm.employee, vm.entity);
            vm.assignOriginEntity();
            break;

          case FORM_MODE.Duplicate:
            //Lấy mã mới và thông tin của đối tượng lên form
            newEntityCode = await vm.getNewEntityCode();
            entityInfo = await vm.getEntityInfo();
            newEntity = {};

            Object.assign(newEntity, entityInfo);
            entityInfo[`${vm.entity}Code`] = newEntityCode;
            entityInfo = FormatFn.formatEntityData(entityInfo, vm.entity);
            vm.employee = entityInfo;
            vm.assignOriginEntity();
            break;
        }
      })();
    },
    mounted() {},
  },
  created() {
    let vm = this;
    eventBus.$on("FromAddFormPopupResponse", (action, choice) => {
      vm.processPopupResponse(action, choice);
    });
  },
  watch: {
    toggleForm: function() {
      this.preLoadFormData();
    },
  },
};
</script>
<style scoped>
@import "../../css/base/modal.css";
</style>
