import ResourceVI from './resource'

export default class DefaultConfig {
    static DateFormat = "DD/MM/YYYY"

    static ExportFileName = ResourceVI.ExportFileName;

    static ExportField = [
        "EmployeeCode",
        "FullName",
        "GenderName",
        "DateOfBirth",
        "PositionName",
        "DepartmentName",
        "BankAccountNumber",
        "BankName",
    ]

    static TableColumn = [{
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

    ]
}