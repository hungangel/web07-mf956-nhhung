using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.Resources.ResourceVI;
using MISA.AMIS.Core.Services.CommonFunctions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MISA.AMIS.Core.Enums.EnumClass;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        readonly IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;
        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
            _serviceResult = new ServiceResult();
        }

        /// <summary>
        /// Thực hiện validate và thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>Kết quả thực hiện thêm mới</returns>
        /// CreatedBy: NHHung(28/08/2021)
        public override ServiceResult Add(Employee employee)
        {
            try
            {
                //Custom validate
                List<string> customValidateRes = CustomValidate(employee);
                if (customValidateRes[0] == "0")
                {
                    //Gọi kết nối csdl thực hiện lưu dữ liệu
                    _serviceResult = base.Add(employee);
                }

                return base.CreateErrorValidateResult(customValidateRes);
            }
            catch (Exception ex)
            {
                var devMsg = ex.Message;
                object errorMsg;

                //Dữ liệu gửi lên bị trùng
                if (devMsg.Contains("Duplicate"))
                {

                    errorMsg = new
                    {
                        devMsg,
                        userMsg = ResourceGeneralVI.Validate_Msg_ExistedImportField,
                        errorCode = "RQ_02"
                    };
                }
                else
                //Lỗi kết nối đến csdl
                {
                    errorMsg = new
                    {
                        devMsg,
                        userMsg = ResourceGeneralVI.Exception_Msg_DBConnection,
                        errorCode = "DB_01"
                    };
                }
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorMsg;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Thực hiện phân trang và lọc dữ liệu cho nhân viên
        /// </summary>
        /// <param name="pageNumber">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số bản ghi/ trang</param>
        /// <param name="searchKey">Từ khóa lọc</param>
        /// <returns></returns>
        /// CreatedBy: NHHung(28/08/2021)
        public ServiceResult FilterEmployee(int pageNumber, int pageSize, string searchKey)
        {
            try
            {
                //Gọi kết nối csdl thực hiện lấy dữ liệu
                PagingResult pagingResult = _employeeRepository.FilterEmployee(pageNumber, pageSize, searchKey);
                List<Employee> filtedEmployees = (List<Employee>)pagingResult.Entities;
                if (filtedEmployees.Any())
                {
                    _serviceResult.Data = pagingResult;
                    _serviceResult.ServiceResultCode = ServiceCode.OK;
                }
                else _serviceResult.ServiceResultCode = ServiceCode.NoContent;

                return _serviceResult;
            }
            catch (Exception ex)
            {
                //Lỗi kết nối đến csdl
                var errorMsg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceGeneralVI.Exception_Msg_DBConnection,
                    errorCode = "DB_01"
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorMsg;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Thực hiện validate thông tin  riêng của đối tượng nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần validate</param>
        /// <returns></returns>
        /// CreatedBy: NHHung(28/08/2021)
        public override List<string> CustomValidate(Employee employee)
        {
            Regex empployeeCodeReg = new Regex(@"^NV-[0-9]*$");
            if (employee.EmployeeCode != null)
            {
                if (!empployeeCodeReg.IsMatch(employee.EmployeeCode))
                    return new List<string>() { "2", "EmployeeCode", "" };

                return new List<string>() { "0", "", "" };
            }
            else return new List<string>() { "1", "EmployeeCode", "" };
        }

        /// <summary>
        /// Thực hiện update thông tin nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// CreatedBy: NHHung(28/08/2021)
        public override ServiceResult Update(Guid employeeID, Employee employee)
        {
            try
            {
                //Custom validate
                List<string> customValidateRes = CustomValidate(employee);
                if (customValidateRes[0] == "0")
                {
                    //Gọi kết nối csdl thực hiện lưu dữ liệu
                    _serviceResult = base.Update(employeeID, employee);
                    return _serviceResult;
                }
                else return base.CreateErrorValidateResult(customValidateRes);
            }
            catch (Exception ex)
            {
                var devMsg = ex.Message;
                object errorMsg;
                if (devMsg.Contains("Duplicate"))
                {
                    //Dữ liệu gửi lên bị trùng
                    errorMsg = new
                    {
                        devMsg,
                        userMsg = ResourceGeneralVI.Validate_Msg_ExistedImportField,
                        errorCode = "RQ_02"
                    };
                }
                else
                {
                    //Lỗi kết nối đến csdl
                    errorMsg = new
                    {
                        devMsg,
                        userMsg = ResourceGeneralVI.Exception_Msg_DBConnection,
                        errorCode = "DB_01"
                    };
                }
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorMsg;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Thực thi xuất khẩu thông tin nhân viên ra tệp excel
        /// </summary>
        /// <param name="pageSize">Số bản ghi/ trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="searchKey">Từ khóa lọc </param>
        /// <param name="propNames"> Danh sách các trường thông tin cần xuất ra</param>
        /// <param name="isAllPage">Lựa chọn xuất khẩu tất cả bản ghi hoặc chỉ trang hiện tại </param>
        /// <returns></returns>
        /// CreatedBy: NHHung(28/08/2021)
        public ServiceResult Export(int pageSize, int pageNumber, string searchKey, List<string> propNames, string isAllPage)
        {
            List<Employee> employees = null;
            if (isAllPage == "true")
            {
                pageNumber = 1;
                pageSize = 100000;
                employees = _employeeRepository.Get();
            }
            else
            {
                PagingResult pagingEmployee = _employeeRepository.FilterEmployee(pageNumber, pageSize, searchKey);
                employees = (List<Employee>)pagingEmployee.Entities;
            }

            if (employees.Any())
            {
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                    //STYLE CHUNG CHO CÁC CỘT DỮ LIỆU

                    //Xác định tên các cột thuộc tính  và độ rộng mặc định  (hàng thứ 3)
                    List<int> columnWidths = new List<int>();
                    workSheet.Cells[3, 1].Value = ResourcePropertyVI.ResourceManager.GetString("Prop_Index");
                    columnWidths.Add(5);
                    for (var i = 0; i < propNames.Count; i++)
                    {
                        var columnTitle = ResourcePropertyVI.ResourceManager.GetString("Prop_" + propNames[i]);
                        workSheet.Cells[3, i + 2].Value = columnTitle;
                        columnWidths.Add(columnTitle.Length + 5);
                    }

                    //Xác định phạm vi các ô có viền
                    var endCol = (char)('A' + (char)(propNames.Count));
                    var endRow = employees.Count + 3;
                    string modelRange = $"A3:{endCol}{endRow}";
                    var modelTable = workSheet.Cells[modelRange];

                    // Thêm viền cho bảng dữ liệu
                    modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    //Font style cho vùng dữ liệu
                    modelTable.Style.Font.Size = 11;
                    modelTable.Style.Font.Name = "Times New Roman";

                    //Căn lề cho cột số thứ tự
                    workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //Điều chỉnh căn lề cho các cột theo kiểu dữ liệu
                    for (int i = 0; i < propNames.Count; i++)
                    {
                        var propDataType = CommonFn.GetPropTypeByName<Employee>(propNames[i]);
                        var displayStyle = CommonFn.GetDislayStyle(propDataType);
                        switch (displayStyle)
                        {
                            case "AlignLeft":
                                workSheet.Column(i + 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                break;
                            case "AlignCenter":
                                workSheet.Column(i + 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                break;
                            case "AlignRight":
                                workSheet.Column(i + 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                break;
                        }
                    }

                    //STYLE RIÊNG CHO CỘT TIÊU ĐỀ VÀ TÊN DANH SÁCH
                    workSheet.Cells[$"A1:{endCol}1"].Merge = true;
                    workSheet.Cells[$"A2:{endCol}2"].Merge = true;
                    var fileHeader = ResourceGeneralVI.ResourceManager.GetString("Dict_Employee").ToUpper();
                    workSheet.Cells["A1"].Value = fileHeader;

                    //Header và cột tiêu đề có style khác
                    //Header có font size 16 , Tiêu đề các cột được căn giữa, in đậm
                    workSheet.Cells["A1:A2"].Style.Font.Size = 16;
                    workSheet.Cells[$"A1:{endCol}3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[$"A1:{endCol}3"].Style.Font.Bold = true;
                    workSheet.Cells[$"A1:{endCol}3"].Style.Font.Name = "Aria";

                    //Thêm màu nền cho cột thuộc tính
                    workSheet.Cells[$"A3:{endCol}3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[$"A3:{endCol}3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                    // CHÈN DỮ LIỆU VÀO BẢNG. Bắt đầu từ hàng 4
                    var rowIndex = 4;
                    foreach (var employee in employees)
                    {
                        //Cột đầu tiên điền số thứ tự, những cột sau đó điền giá trị của bản ghi
                        workSheet.Cells[rowIndex, 1].Value = rowIndex - 3;
                        for (int i = 0; i < propNames.Count; i++)
                        {
                            var propValue = CommonFn.GetCustomObjectValue(employee, propNames[i]);
                            var displayValue = CommonFn.GetDisplayValue<Employee>(propNames[i], propValue);
                            workSheet.Cells[rowIndex, i + 2].Value = displayValue.ToString();
                            columnWidths[i + 1] = Math.Max(columnWidths[i + 1], displayValue.ToString().Length + 5);
                        }
                        rowIndex++;
                    }

                    //Điều chỉnh lại độ rộng các cột theo dữ liệu thêm vào
                    for (var i = 0; i < columnWidths.Count; i++)
                        workSheet.Column(i + 1).Width = columnWidths[i];
                    package.Save();
                }

                stream.Position = 0;
                _serviceResult.Data = stream;
            }
            else _serviceResult.ServiceResultCode = ServiceCode.NoContent;
            return _serviceResult;
        }

        /// <summary>
        /// Thực hiện sinh mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NHHung(28/08/2021)
        public override ServiceResult GetNewCode()
        {
            try
            {
                string highestCode = _employeeRepository.GetHighestCode();
                int highestCodeNumber = 0;
                string newCode = "";

                //Kiểm tra dữ liệu trả về
                if (highestCode != null && highestCode != "")
                {
                    try
                    {
                        highestCodeNumber = int.Parse(highestCode.Split("NV-")[1]);
                        newCode = newCode + (highestCodeNumber + 1).ToString();
                        while (newCode.Length < highestCode.Length - 3) newCode = "0" + newCode;
                    }
                    catch (Exception)
                    {
                        //Lỗi dữ liệu trả về không hợp lệ không ở dang NV-xxx
                        var errorMsg = new
                        {
                            devMsg = ResourceGeneralVI.Exception_Msg_InvalidDBData,
                            userMsg = ResourceGeneralVI.Exception_Msg_Common,
                            errorCode = "DB_02"
                        };
                        _serviceResult.IsValid = false;
                        _serviceResult.Data = errorMsg;
                        _serviceResult.ServiceResultCode = ServiceCode.InternalServerError;
                        return _serviceResult; ;
                    }
                }

                //Tạo xâu mã mới và trả về
                _serviceResult.Data = $"NV-{newCode}";
            }
            catch (Exception ex)
            {
                _serviceResult = CreateExceptionResult(ex);
            }
            return _serviceResult;
        }
    }
}
