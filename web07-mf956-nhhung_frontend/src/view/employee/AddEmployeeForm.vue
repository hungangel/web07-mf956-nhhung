<template>
  <div :class="{ 'd-none': isHidden }">
    <div class="modal-background"></div>
    <div class="modal-zone" entityId="manv">
      <div class="modal">
        <div class="md-header">
          <div class="flex">
            <div class="title">Thông tin nhân viên</div>
            <Checkbox :checkboxText="'Là khách hàng'" />
            <Checkbox :checkboxText="'Là nhà cung cấp'" />
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
                  :maxLength="10"
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
                  myurl="Departments"
                  entity="Department"
                  ddDirection="top-32"
                  :isRequired="true"
                  :selectedId="'0'"
                  v-model="employee.DepartmentID"
                  :originValue="employee.DepartmentID"
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
                    :format="'DD/MM/YYYY'"
                    :value-type="'YYYY-MM-DD'"
                    placeholder="DD/MM/YYYY"
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
                  :triggerUpdate="triggerUpdate"
                />
              </div>
              <div class="md-input-row flex">
                <FieldInputLabel
                  labelText="Số CMND"
                  :maxLength="100"
                  subClass="w-60p pd-r-6"
                  v-model="employee.IdentityNumber"
                  :originValue="employee.IdentityNumber"
                  :validate="triggerValidate"
                />

                <div class="flex-col">
                  <label for="">Ngày sinh</label>
                  <DatePicker
                    v-model="employee.DateOfBirth"
                    :format="'DD/MM/YYYY'"
                    :value-type="'YYYY-MM-DD'"
                    placeholder="DD/MM/YYYY"
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
              </div>
              <div class="md-input-row flex">
                <FieldInputLabel
                  labelText="Nơi cấp"
                  :maxLength="100"
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
                    :maxLength="255"
                    v-model="employee.MobilePhoneNumber"
                    :originValue="employee.MobilePhoneNumber"
                  />
                </div>
                <div class="md-input-row w-50p pd-r-6">
                  <FieldInputLabel
                    labelText="ĐT cố định"
                    :maxLength="255"
                    v-model="employee.LandlinePhoneNumber"
                    :originValue="employee.LandlinePhoneNumber"
                  />
                </div>
              </div>
              <div class="md-content-col">
                <div class="md-input-row  w-50p">
                  <FieldInputLabel
                    labelText="Email"
                    :maxLength="255"
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
                    :maxLength="255"
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
              @btnClick="btnCloseOnClick"
            />
            <div class="flex">
              <Button
                subClass="secondary fw-700"
                buttonText="Cất"
                @btnClick="btnSaveOnClick('SaveThenClose')"
              />
              <Button
                subClass="primary fw-700 ml-10  btn-save "
                buttonText="Cất và Thêm"
                @btnClick="btnSaveOnClick('SaveThenAdd')"
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
import FormatFn from "../../js/FormatFunction.js";
import Checkbox from "../../components/base/BaseCheckbox.vue";
import RadioButton from "../../components/base/BaseRadioButton.vue";
import FieldInputLabel from "../../components/base/BaseFieldInputLabel.vue";
import ComboBox from "../../components/base/BaseComboBox.vue";
import Button from "../../components/base/BaseButton.vue";
import { eventBus } from "../../main.js";
import ResourceVI from "../../js/ResourceVI.js";
import Constant from "../../api/config/APIConfig.js";
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
      isShowed: true,
      updateCombobox: true,
      triggerValidate: true,
      triggerUpdate: true,
      firstErrorField: null,
      entity: "Employee",
      firstErrorMessage: "",
    };
  },
  props: {
    isHidden: Boolean,
    employeeID: String,
    formMode: Number,
    toggleForm: Boolean,
    warningResponse: String,
    thList: Object,
    myurl: String,
  },
  methods: {
    //#region Sự kiện trên các nút / components

    /**
     * Thực thi cho sự kiện do nút save emit .
     * Kiểm tra trạng thái form thêm mới (0) hay cập nhật (1)
     * Thực hiện validate và hiện popup confirm / thông báo lỗi
     *  CreatedBy: NHHung(29/08)
     */
    async btnSaveOnClick(formAction) {
      let vm = this,
        errorField = vm.validateFormData();
      //Nếu form đang ở chế độ sửa thông tin
      if (vm.formMode == 1) {
        formAction = formAction.replace("Save", "Update");
      }

      if (!errorField) {
        let isValid = false;

        //Kiểm tra mã trùng
        isValid = await vm.checkExistingCode(vm.employee.EmployeeCode);
        if (isValid) {
          //Nếu không có lỗi xảy ra  , hiện popup xác nhận lưu
          let popupMessage = {
            messageType: "CONFIRM",
            textBody: FormatFn.formatString(
              ResourceVI.PopupMessage[formAction],
              vm.employee.FullName
            ),
          };
          eventBus.$emit(
            "showPopupMessage",
            "FromAddForm",
            popupMessage,
            formAction
          );
        } else {
          //Nếu mã bị trùng
          let popupMessage = {
            messageType: "ALERT",
            textBody: FormatFn.formatString(
              ResourceVI.ValidateMessage.DUPLICATED,
              ResourceVI.EntityName[vm.entity],
              vm.employee.EmployeeCode
            ),
          };
          eventBus.$emit(
            "showPopupMessage",
            "FromAddForm",
            popupMessage,
            formAction
          );
        }
      } else {
        //Nếu kết quả validate không hợp lệ
        let action = "ValidateOnSave",
          message = {
            messageType: "ALERT",
            textBody: vm.firstErrorMessage,
          };
        eventBus.$emit("showPopupMessage", "FromAddForm", message, action);
      }
    },

    /**
     * Sự kiện bấm nút đóng form
     * Thực hiện kiểm tra form có bị thay đổi không, hiển thị popup confirm nếu có thay đổi
     * CreatedBy: NHHung(29/08)
     */
    btnCloseOnClick() {
      let isModifiedForm = this.checkModifiedEntity(),
        formAction = "SaveThenClose";

      if (isModifiedForm) {
        let popupMessage = {
          messageType: "FULL",
          textBody: ResourceVI.PopupMessage["CloseModifedForm"],
        };
        eventBus.$emit(
          "showPopupMessage",
          "FromAddForm",
          popupMessage,
          formAction
        );
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
        if (vm.employee[key] != vm.originEntity[key]) return true;
      }
      return false;
    },

    /**
     * Kiểm tra các trường dữ liệu cần validate
     * Xác định trường đầu tiên bị lỗi và thông báo lỗi
     * CreatedBy: NHHung(29/08)
     */
    validateFormData() {
      let vm = this,
        errorField = null;

      for (let [key] of Object.entries(vm.$refs)) {
        if (key.includes("validateField")) {
          let validateMsg = vm.$refs[key].validateInput();
          if (validateMsg != "Correct" && errorField == null) {
            errorField = key;
            vm.firstErrorMessage = validateMsg;
          }
        }
      }
      vm.firstErrorField = errorField;
      return errorField;
    },

    /**
     * Focus vào ô bị lỗi đầu tiên
     * CreatedBy: NHHung(01/09)
     */
    focusOnFirstErrorField() {
      this.$refs[this.firstErrorField].doFocus(true);
    },

    /**
     * Xử lí khi nhận phản hồi từ popup messsage
     * CreatedBy: NHHung(01/09)
     */
    processAddFormResponse(action, choice) {
      let vm = this;
      if (choice == "CONFIRM") {
        console.log(action);

        switch (action) {
          //Trường hợp confirm khi thông báo lỗi, focus ô bị lỗi
          case "ValidateOnSave":
            vm.focusOnFirstErrorField();
            break;
          default:
            //Mặc địnhh kiểm tra hành động có phải là lưu hay update, gọi hàm tương ứng
            //Thực hiện khi không có lỗi
            if (
              (action.includes("SaveThen") || action.includes("UpdateThen")) &&
              vm.firstErrorField == null
            ) {
              if (vm.formMode == 0 || vm.formMode == 2) {
                console.log("do save");
                vm.doSaveEntity(action);
              } else if (vm.formMode == 1) {
                console.log("do update");
                vm.doUpdateEntity(action);
              }
            }
            break;
        }
        return;
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
    doSaveEntity(formAction) {
      let vm = this,
        isValid = true;
      axios
        .post(`${Constant.LocalUrl}/${vm.myurl}/`, vm.employee)
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
    doUpdateEntity(formAction) {
      let vm = this,
        isValid = true;
      axios
        .put(`${Constant.LocalUrl}/${vm.myurl}/${vm.employeeID}`, vm.employee)
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
            `${Constant.LocalUrl}/${vm.myurl}/CheckExist?entityCode=${entityCode}`
          )
          .then((response) => {
            if (response.status == 200) {
              if (vm.formMode == 0 || vm.formMode == 2) {
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
              "DANGER",
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
    getNewEntityCode() {
      let vm = this;
      return new Promise((resolve) => {
        axios
          .get(`${Constant.LocalUrl}/${vm.myurl}/NewCode`)
          .then((response) => {
            // vm.$set(vm.employee, `${vm.entity}Code`, response.data);
            // vm.assignOriginEntity();
            resolve(response.data);
          })
          .catch((error) => {
            eventBus.$emit(
              "showToastMessage",
              "GetNewCodeFailed",
              "DANGER",
              "GetNewCode",
              error
            );
          });
      });
    },

    /**
     * GỌi API lấy thông tin đối tượng theo ID truyền vào
     * trả về thông tin đối tượn
     * CreatedBy: NHHung(01/09)
     */
    getEntityInfo() {
      let vm = this;
      return new Promise((resolve) => {
        axios
          .get(`${Constant.LocalUrl}/${vm.myurl}/${vm.employeeID}`)
          .then((response) => {
            let foundEntity = response.data;
            // vm.employee = FormatFn.formatEntityData(foundEntity);
            // vm.assignOriginEntity();
            resolve(foundEntity);
          })
          .catch((error) => {
            eventBus.$emit(
              "showToastMessage",
              "GetInfoFailed",
              "DANGER",
              "GetInfo",
              error
            );
          });
      });
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
      if (formAction.includes("Close")) {
        vm.$emit("hideAddForm");
        vm.$emit("callReloadTable");
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
      } else {
        responseData = response.response.data;
        vm.createPopupMessage(actionResult, isValid, responseData);
      }
    },

    /**
     * Hiển thị popup message trong response trả về của server nếu có
     * Nếu không hiện thị thông báo mặc định theo hành động gọi
     * CreatedBy: NHHung(01/09)
     */
    createPopupMessage(actionResult, isValid, responseData) {
      let messageType = "",
        textBody = "";

      if (isValid) {
        messageType = "NOTIFY";
        textBody = responseData.userMsg
          ? responseData.userMsg
          : ResourceVI.UserMsg[actionResult];
      } else {
        if (responseData) {
          messageType = "ALERT";
          textBody = responseData.userMsg
            ? responseData.userMsg
            : ResourceVI.UserMsg[actionResult];
        } else {
          textBody = ResourceVI.UserMsg[actionResult];
        }
      }

      let popupMessage = { messageType, textBody };
      eventBus.$emit(
        "showPopupMessage",
        "FromAddForm",
        popupMessage,
        messageType
      );
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
        vm.triggerUpdate = !vm.triggerUpdate;
        let entityInfo = {},
          newEntity = {},
          newEntityCode = "";
        //Mode= 0: THêm mới, 1:Sửa thông tin, 2:Nhân bản
        switch (vm.formMode) {
          case 0:
            //Lấy mã mới cho form
            newEntityCode = await vm.getNewEntityCode();
            newEntity[`${vm.entity}Code`] = newEntityCode;
            vm.employee = newEntity;
            vm.assignOriginEntity();
            break;

          case 1:
            //Lấy thông tin đối tượng lên trên form
            vm.employee = await vm.getEntityInfo();
            vm.employee = FormatFn.formatEntityData(vm.employee, vm.entity);
            vm.assignOriginEntity();
            break;
          case 2:
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
    mounted() {
      // window.addEventListener(
      //   "keydown",
      //   function(e) {
      //     console.log( "window", e.code);
      //   }.bind(this)
      // );
    },
  },
  created() {
    let vm = this;
    eventBus.$on("FromAddFormPopupResponse", (action, choice) => {
      vm.processAddFormResponse(action, choice);
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
