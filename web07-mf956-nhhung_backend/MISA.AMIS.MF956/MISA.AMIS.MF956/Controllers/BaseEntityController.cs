using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.Resources.ResourceVI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseEntityController<CustomEntity> : ControllerBase
    {
        #region Declaration

        readonly IBaseService<CustomEntity> _baseService;
        ServiceResult _serviceResult;
        public BaseEntityController(IBaseService<CustomEntity> baseService)
        {
            _baseService = baseService;
            _serviceResult = new ServiceResult();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gọi service lấy tất cả bản ghi trong CSDL
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                _serviceResult = _baseService.Get();
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);
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
                return StatusCode(500, errorMsg);
            }
        }

        /// <summary>
        /// Lấy 1 đối tượng trong CSDL theo ID
        /// </summary>
        /// <param name="entityID">ID đối tượng cần lấy</param>
        /// <returns>Thông tin đối tượng tìm được </returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpGet("{entityID}")]
        public virtual IActionResult GetEntityByID(Guid entityID)
        {
            try
            {
                _serviceResult = _baseService.GetByID(entityID);
                // Kiểm tra dữ liệu trả về
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);

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
        /// Kiểm tra trùng lặp mã đối tượng
        /// </summary>
        /// <param name="entityCode">Mã cần kiểm tra trùng</param>
        /// <returns>Danh sách bản ghi có mã trùng với mã kiểm tra </returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpGet("CheckExist")]
        public virtual IActionResult CheckExistingCode(string entityCode)
        {
            try
            {
                _serviceResult = _baseService.CheckExistingCode(entityCode);
                // Kiểm tra dữ liệu trả về
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);
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
        /// Sinh mã mới nhất cho đối tượng
        /// </summary>
        /// <returns>Mã mới nhất tạo được</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpGet("NewCode")]
        public virtual IActionResult GetNewEntityCode()
        {
            try
            {

                _serviceResult = _baseService.GetNewCode();
                // Kiểm tra dữ liệu trả về
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);
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
        /// Thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi cần thêm mới</param>
        /// <returns>Kết quả thực hiện thêm mới</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpPost]
        public virtual IActionResult Add(CustomEntity entity)
        {
            try
            {
                _serviceResult = new ServiceResult();
                _serviceResult = _baseService.Add(entity);

                // Kiểm tra dữ liệu trả về
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);
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
        /// Thực hiện cập nhật thông tin đối tượng
        /// </summary>
        /// <param name="entityID">ID đối tượng cần sửa</param>
        /// <param name="entity">Thông tin mới của đối tượng</param>
        /// <returns>Kết quả thao tác sửa</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpPut("{entityID}")]
        public virtual IActionResult Update(Guid entityID, CustomEntity entity)
        {
            //Thực hiện cập nhật thông tin đối tượng
            try
            {
                _serviceResult = _baseService.Update(entityID, entity);
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);
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
        /// Xóa bản ghi theo ID truyền vào
        /// </summary>
        /// <param name="entityID">ID bản ghi cần xóa</param>
        /// <returns>Kết quả thao tác xóa</returns>
        /// CreatedBy: NHHung(28/08/2021)
        [HttpDelete("{entityID}")]
        public virtual IActionResult Delete(Guid entityID)
        {
            try
            {
                _serviceResult = _baseService.Delete(entityID);
                // Kiểm tra dữ liệu trả về
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);

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
        /// Thực hiện xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityGuids">Danh sách ID bản ghi cần xóa</param>
        /// <returns>Kết quả thao tác xóa nhiều</returns>
        /// CreatedBy: NHHung(29/08/2021)
        [HttpPost("Multiple/Delete")]
        public virtual IActionResult DeleteMultiple([FromBody] List<Guid> entityGuids)
        {
            try
            {
                _serviceResult = _baseService.DeleteMultiple(entityGuids);
                // Kiểm tra dữ liệu trả về
                return StatusCode((int)_serviceResult.ServiceResultCode, _serviceResult.Data);

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
