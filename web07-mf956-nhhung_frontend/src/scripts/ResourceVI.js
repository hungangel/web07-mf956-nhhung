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

    static pageSizeText = "bản ghi trên trang";

    //Các tùy chọn chức năng với đối tượng trong bảng.
    static optionTexts = {
        employee: [
            { optionId: 0, optionText: "Sửa", optionAction: "ReqEdit" },
            { optionId: 1, optionText: "Xóa", optionAction: "ReqDelete" },
            { optionId: 2, optionText: "Nhân bản", optionAction: "ReqDuplicate" },
            { optionId: 3, optionText: "Ngừng sử dụng", optionAction: "ReqStopUsing" },
        ],
        cusomter: [
            { optionId: 0, optionText: "Xóa" },
            { optionId: 1, optionText: "Nhân bản" },
        ],
    }

    //Kết quả thực hiện
    static UserMsg = {
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
        ExportFileFailed: "Xuất khẩu tệp thất bại."

    }

    //Popup message template

    static PopupMessage = {
        SaveThenClose: "Bạn có muốn lưu dữ liệu nhân viên {0} và đóng form  không?",
        SaveThenAdd: "Bạn có muốn lưu dữ liệu nhân viên {0} không?",
        UpdateThenClose: "Bạn có muốn sửa dữ liệu nhân viên {0} và đóng form  không?",
        UpdateThenAdd: "Bạn có muốn sửa dữ liệu nhân viên {0} không?",
        CloseModifedForm: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
        DeleteRecord: "Bạn có thực sự muốn xóa {0} <{1}> không?",
        DeleteMultiple: "Bạn có thực sự muốn xóa {0} {1} đã chọn không?"
    }

    static ButtonText = {
        YES: "Có",
        NO: "Không",
        CANCEL: "Hủy",
        WARNING: "Đồng ý",
        CLOSE: "ĐÓNG",
    }


    static ValidateMessage = {
        NOT_NULL: "{0} không được để trống.",
        INVALID_FORMAT: "{0} sai định dạng.",
        CONTAIN_ALPHABETS_ONLY: "{0} chỉ chứa các chữ cái.",
        DUPLICATED: "{0} <{1}> đã tồn tại! Vui lòng kiểm tra lại.",
    }



}