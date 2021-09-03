using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<CustomEntity>
    {
        /// <summary>
        /// Lấy tất cả bản ghi trong CSDL
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: NHHung(28/08/2021)
        List<CustomEntity> Get();

        /// <summary>
        /// Lấy 1 bản ghi theo ID truyền vào
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns>Thông tin bản ghi tìm được</returns>
        /// CreatedBy: NHHung(28/08/2021)
        CustomEntity GetByID(Guid entityID);

        /// <summary>
        /// Thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="customEntity">Thông tin bản ghi cần thêm mới</param>
        /// <returns>Kết quả thực hiện thêm mới 1:Thành công, 0:Thất bại</returns>
        /// CreatedBy: NHHung(28/08/2021)
        int Add(CustomEntity customEntity);

        /// <summary>
        /// Thực hiện cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="entityID">ID bản ghi cần sửa</param>
        /// <param name="customEntity">Thông tin của bản ghi cần sửa</param>
        /// <returns>Kết quả thực cập nhật 1:Thành công, 0:Thất bại</returns>
        /// CreatedBy: NHHung(28/08/2021)
        int Update(Guid entityID, CustomEntity customEntity);

        /// <summary>
        /// Xóa 1 bản ghi theo ID truyền vào
        /// </summary>
        /// <param name="entityID">ID bản ghi cần xóa</param>
        /// <returns>Kết quả thực hiện xóa 1:Thành công, 0:Thất bại</returns>
        /// CreatedBy: NHHung(28/08/2021)
        int Delete(Guid entityID);


        #region Utils
        /// <summary>
        /// Lấy các bản ghi có thuộc tính giống với điều kiện tìm kiếm
        /// </summary>
        /// <param name="fieldName">Tên trường tìm kiếm</param>
        /// <param name="fieldValue">Dữ liệu cần tìm </param>
        /// <param name="fieldType">Kiểu dữ liệu</param>
        /// <returns>Danh sách bản ghi có trường tìm kiếm và dữ liệu khớp</returns>
        /// CreatedBy: NHHung(28/08/2021)
        List<CustomEntity> CheckDuplicatedField(string fieldName, object fieldValue, Type fieldType);

        /// <summary>
        /// Lấy lớn nhất hiện trong CSDL của đối tượng
        /// </summary>
        /// <returns>Mã lớn nhất tìm được</returns>
        /// CreatedBy: NHHung(28/08/2021)
        string GetHighestCode();

        /// <summary>
        /// Xóa nhiều bản ghi 
        /// </summary>
        /// <param name="guids">ID những bản ghi cần xóa</param>
        /// <returns>Kết quả xóa hàng loạt</returns>
        /// CreatedBy: NHHung(29/08/2021)
        int DeleteMultiple(List<Guid> guids);

        #endregion

    }
}
