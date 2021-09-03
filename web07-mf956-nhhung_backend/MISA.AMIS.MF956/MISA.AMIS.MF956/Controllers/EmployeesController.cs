using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.Resources.ResourceVI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static MISA.AMIS.Core.Enums.EnumClass;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors]
    public class EmployeesController : BaseEntityController<Employee>
    {
        readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService, IBaseService<Employee> baseService) : base(baseService)
        {
            _employeeService = employeeService;
        }


        #region Methods

        /// <summary>
        /// Gọi service thực hiện lọc và phân trang theo tiêu chí truyền vào
        /// </summary>
        /// <param name="pageNumber">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số bản ghi / trang</param>
        /// <param name="searchKey">Từ khóa tìm kiếm</param>
        /// <returns>Thông tin phân trang gồm tổng số trang, tổng số bản ghi tìm được, danh sách bản ghi trang cần lọc</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpGet("Filter")]
        public IActionResult FilterEmployee(int pageNumber, int pageSize, string searchKey)
        {
            try
            {
                ServiceResult serviceResult = _employeeService.FilterEmployee(pageNumber, pageSize, searchKey);
                //Kiểm tra dữ liệu trả về
                return StatusCode((int)serviceResult.ServiceResultCode, serviceResult.Data);
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
                var response = StatusCode(500, errorMsg);
                return response;
            }
        }

        /// <summary>
        /// Gọi service thực hiện thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm</param>
        /// <returns>Kết quả thao tác thêm mới</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpPost]
        public override IActionResult Add(Employee employee)
        {
            try
            {
                ServiceResult serviceResult = new ServiceResult();
                serviceResult = _employeeService.Add(employee);

                // Kiểm tra dữ liệu trả về
                return StatusCode((int)serviceResult.ServiceResultCode, serviceResult.Data);
            }
            catch (Exception ex)
            {
                //Lỗi kết nối đến csdl
                var errorMsg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceGeneralVI.Exception_Msg_Common,
                    errorCode = "DB_01"
                };
                var response = StatusCode(500, errorMsg);
                return response;
            }
        }

        /// <summary>
        /// Gọi service thực hiện cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employeeID">ID nhân viên cần sửa thông tin</param>
        /// <param name="employee">Thông tin mới của nhân viên</param>
        /// <returns>Kết quả cập nhật  thông tin</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpPut("{EmployeeID}")]
        public override IActionResult Update(Guid employeeID, Employee employee)
        {
            //Thực hiện cập nhật thông tin nhân viên
            try
            {
                ServiceResult serviceResult = _employeeService.Update(employeeID, employee);
                return StatusCode((int)serviceResult.ServiceResultCode, serviceResult.Data);
            }
            catch (Exception ex)
            {
                var devMsg = ex.Message;
                if (devMsg.Contains("Duplicate"))
                {
                    //Dữ liệu gửi lên bị trùng
                    var errorMsg = new
                    {
                        devMsg,
                        userMsg = ResourceGeneralVI.Validate_Msg_ExistedImportField,
                        errorCode = "RQ_02"
                    };
                    var response = StatusCode(400, errorMsg);
                    return response;
                }
                else
                {
                    //Lỗi kết nối đến csdl
                    var errorMsg = new
                    {
                        devMsg,
                        userMsg = ResourceGeneralVI.Exception_Msg_DBConnection,
                        errorCode = "DB_01"
                    };
                    var response = StatusCode(500, errorMsg);
                    return response;
                }
            }
        }

        /// <summary>
        /// Gọi service thực hiện xuất khẩu dữ liệu ra tệp excel
        /// </summary>
        /// <param name="pageSize">Trang cần nhập khẩu</param>
        /// <param name="pageNumber">Số bản ghi/ trang</param>
        /// <param name="searchKey">Từ khóa lọc</param>
        /// <param name="isAllPage">Xuất khẩu theo điều kiện lọc hoặc tất cả bản ghi</param>
        /// <param name="propNames">Danh sách những cột thông tin cần xuất ra tệp</param>
        /// <returns>Tệp excel xuất khẩu danh sách nhân viên</returns>
        /// CreatedBy: NHHung(29/08/2021)
        [HttpPost("Export")]
        public IActionResult Export(int pageSize, int pageNumber, string searchKey, string isAllPage, List<string> propNames)
        {
            //List<string> propNames= new List<string>();
            ServiceResult serviceResult = _employeeService.Export(pageSize, pageNumber, searchKey, propNames, isAllPage);
            if (serviceResult.ServiceResultCode == ServiceCode.OK)
            {
                var stream = (MemoryStream)serviceResult.Data;
                string excelName = $"Export_Employee_{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            else  return StatusCode((int)serviceResult.ServiceResultCode, serviceResult.Data);
        }

        /// <summary>
        /// Gọi service lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpGet("NewCode")]
        public override IActionResult GetNewEntityCode()
        {
            try
            {

                ServiceResult serviceResult = _employeeService.GetNewCode();
                // Kiểm tra dữ liệu trả về
                return StatusCode((int)serviceResult.ServiceResultCode, serviceResult.Data);
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
                var response = StatusCode(500, errorMsg);
                return response;
            }
        }
        #endregion
    }
}

