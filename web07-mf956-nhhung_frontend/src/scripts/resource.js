export default class ResourceVI {

    static LeftMenuText = {
        dashboard: "Tổng quan",
        cash: "Tiền mặt",
        deposit: "Tiền gửi",
        buy: "Mua hàng",
        sell: "Bán hàng",
        invoice: "Quản lý hóa đơn",
        warehouse: "Kho",
        tool: "Công cụ dụng cụ",
        assets: "Tài sản cố định",
        tax: "Thuế",
        price: "Giá thành",
        sumary: "Tổng hợp",
        budget: "Ngân sách",
        report: "Báo cáo",
        analysis: "Phân tích tài chính"
    }

    static Gender = {
        Male: "Nam",
        Female: "Nữ",
        Other: "Khác"
    }

    static EntityName = {
        Employee: "Nhân viên",
        Customer: "Khách hàng"
    }

    static PageSizeText = "bản ghi trên trang";

    //Các tùy chọn chức năng với đối tượng trong bảng.
    static optionTexts = {
        Employee: [
            { optionId: 0, optionText: "Sửa", optionAction: "RequestEdit" },
            { optionId: 1, optionText: "Xóa", optionAction: "RequestDelete" },
            { optionId: 2, optionText: "Nhân bản", optionAction: "RequestDuplicate" },
            { optionId: 3, optionText: "Ngừng sử dụng", optionAction: "RequestStopUsing" },
        ],
        Cusomter: [
            { optionId: 0, optionText: "Xóa" },
            { optionId: 1, optionText: "Nhân bản" },
        ],
    }

    //Popup message template

    static PopupMessage = {
        //Confirm message
        SaveAndClose: "Bạn có muốn lưu dữ liệu nhân viên {0} và đóng form  không?",
        SaveAndAdd: "Bạn có muốn lưu dữ liệu nhân viên {0} không?",
        UpdateAndClose: "Bạn có muốn sửa dữ liệu nhân viên {0} và đóng form  không?",
        UpdateAndAdd: "Bạn có muốn sửa dữ liệu nhân viên {0} không?",
        CloseModifiedForm: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
        DeleteRecord: "Bạn có thực sự muốn xóa Nhân viên <{0}> không?",
        DeleteMultiple: "Bạn có thực sự muốn xóa {0} nhân viên đã chọn không?",

        //Toast Message
        AddSuccess: "Thao tác thêm mới thành công.",
        AddFailed: "Thao tác thêm mới thất bại.",
        UpdateSuccess: "Thao tác cập nhật thành công.",
        UpdateFailed: "Thao tác cập nhật thất bại.",
        ValidateFailed: "Vui lòng kiểm tra lại các trường.",
        CommonError: "Có lỗi xảy ra. Vui lòng thử lại.",
        GetNewCodeFailed: "Không thể lấy được mã nhân viên mới.",
        GetInfoFailed: "Không lấy được thông tin nhân viên.",
        LoadDataSuccess: "Làm mới dữ liệu thành công.",
        LoadDataFailed: "Làm mới dữ liệu thất bại.",
        NoContent: "Không có dữ liệu phù hợp.",
        DeleteComplete: "Hoàn thành xóa bản ghi.",
        DeleteFailed: "Xóa thất bại. Dữ liệu không thay đổi.",
        ExportFileFailed: "Xuất khẩu tệp thất bại.",

        //Validate Message
        NotMatch: "{0} không trùng khớp.",
        NotNull: "{0} không được để trống.",
        InvalidFormat: "{0} sai định dạng.",
        InvalidValue: "{0} không hợp lệ.",
        ContainAlphabetsOnly: "{0} chỉ chứa các chữ cái.",
        Duplicated: "Nhân viên <{0}> đã tồn tại! Vui lòng kiểm tra lại.",
        Existed: "{0} đã tồn tại."
    }

    static ButtonText = {
        Yes: "Có",
        No: "Không",
        Cancel: "Hủy",
        Accept: "Đồng ý",
        Close: "Đóng",
    }

    static ExportFileName = "Danh sách nhân viên.xlsx"
}