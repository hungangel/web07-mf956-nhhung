using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IBaseService<CustomEntity>
    {
        #region Get

        /// <summary>
        /// Lấy tất cả bản ghi trong CSDL
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: NHHung(28/08/2021)
        ServiceResult Get();

        /// <summary>
        /// Lấy 1 bản ghi theo ID truyền vào
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns>Thông tin bản ghi tìm được</returns>
        /// CreatedBy: NHHung(28/08/2021)
        ServiceResult GetByID(Guid entiyID);

        /// <summary>
        /// Lấy danh sách các bản ghi có mã trùng với mã kiểm tra
        /// </summary>
        /// <param name="entityCode">Mã cần kiểm tra</param>
        /// <returns> Danh sách bản ghi tìm được</returns>
        /// CreatedBy: NHHung(29/08/2021)
        ServiceResult CheckExistingCode(string entityCode);

        #endregion

        /// <summary>
        /// Thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="customEntity">Thông tin bản ghi cần thêm mới</param>
        /// <returns>Kết quả thực hiện thêm mới 1:Thành công, 0:Thất bại</returns>
        /// CreatedBy: NHHung(28/08/2021)
        ServiceResult Add(CustomEntity customEntity);

        /// <summary>
        /// Thực hiện cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="entityID">ID bản ghi cần sửa</param>
        /// <param name="customEntity">Thông tin của bản ghi cần sửa</param>
        /// <returns>Kết quả thực cập nhật 1:Thành công, 0:Thất bại</returns>
        /// CreatedBy: NHHung(28/08/2021)
        ServiceResult Update(Guid entityID, CustomEntity customEntity);

        /// <summary>
        /// Xóa 1 bản ghi theo ID truyền vào
        /// </summary>
        /// <param name="entityID">ID bản ghi cần xóa</param>
        /// <returns>Kết quả thực hiện xóa 1:Thành công, 0:Thất bại</returns>
        /// CreatedBy: NHHung(28/08/2021)
        ServiceResult Delete(Guid entityID);


        #region Utils
        /// <summary>
        /// Lấy các bản ghi có thuộc tính giống với điều kiện tìm kiếm
        /// </summary>
        /// <param name="fieldName">Tên trường tìm kiếm</param>
        /// <param name="fieldValue">Dữ liệu cần tìm </param>
        /// <param name="fieldType">Kiểu dữ liệu</param>
        /// <returns>Danh sách bản ghi có trường tìm kiếm và dữ liệu khớp</returns>
        /// CreatedBy: NHHung(28/08/2021)
        ServiceResult CheckDuplicatedField(string fieldName, object fieldValue, Type fieldType);

        /// <summary>
        /// Thực hiện validate các thông tin chung của đối tượng
        /// </summary>
        /// <param name="entityID">ID của đối tượng cần validate</param>
        /// <param name="customEntity">Thông tin đối tượng</param>
        /// <param name="validateMode">Chế độ validate khi thêm mới hay cập nhật</param>
        /// <returns>Kết quả validate</returns>
        /// CreatedBy: NHHung(28/08/2021)
        List<string> BaseValidate(Guid entityID, CustomEntity customEntity, int validateMode);

        /// <summary>
        /// Lấy lớn nhất hiện trong CSDL của đối tượng
        /// </summary>
        /// <returns>Mã lớn nhất tìm được</returns>
        /// CreatedBy: NHHung(28/08/2021)
        List<string> CustomValidate(CustomEntity customEntity);

        /// <summary>
        /// Thực hiện tạo mã mới cho đối tượng
        /// </summary>
        /// <returns>Mã mới tạo được</returns>
        ServiceResult GetNewCode();

        /// <summary>
        /// Thực hiện xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityGuids"></param>
        /// <returns></returns>
        /// CreatedBy: NHHung(28/08/2021)
        ServiceResult DeleteMultiple(List<Guid> entityGuids);

        #endregion
    }
}
