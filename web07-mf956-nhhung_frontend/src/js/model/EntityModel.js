class EntityModel {
    Employee = {
        EmployeeCode: {
            dataType: "String",
            fieldText: "MÃ NHÂN VIÊN",
        },
        FullName: {
            dataType: "String",
            fieldText: "TÊN NHÂN VIÊN",
        },
        Gender: {
            dataType: "Enum",
            fieldText: "GIỚI TÍNH",
        },
        GenderName: {
            dataType: "Text",
            fieldText: "GIỚI TÍNH",
        },
        DateOfBirth: {
            dataType: "Date",
            fieldText: "NGÀY SINH",
        },
        IdentityNumber: {
            dataType: "String",
            fieldText: "SỐ CMND",
        },
        IdentityDate: {
            dataType: "Date",
            fieldText: "NGÀY CẤP",
        },
        PositionName: {
            dataType: "String",
            fieldText: "CHỨC DANH",
        },
        DepartmentName: {
            dataType: "String",
            fieldText: "TÊN ĐƠN VỊ",
        },
        BankAccountNumber: {
            dataType: "String",
            fieldText: "SỐ TÀI KHOẢN",
        },
        BankName: {
            dataType: "String",
            fieldText: "TÊN NGÂN HÀNG",
        },
        BankBranch: {
            dataType: "String",
            fieldText: "CHI NHÁNH TK NGÂN HÀNG",
        },
    }

    Department = {
        DepartmentCode: {
            dataType: "String",
            fieldText: "Mã đơn vị",
        },
        DepartmentName: {
            dataType: "String",
            fieldText: "Tên đơn vị",
        },
        Description: {
            dataType: "Enum",
            fieldText: "Mô tả",
        },
    }
}
export default new EntityModel();