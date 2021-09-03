using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.AMIS.Core.CustomAttribute.CustomAttribute;
using static MISA.AMIS.Core.Enums.EnumClass;

namespace MISA.AMIS.Core.Entities
{
    public class Employee: Person
    {
        #region Property

        /// <summary>
        /// ID nhân viên
        /// </summary>
        [NotNull]
        [Unique]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotNull]
        [Unique]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [NotNull]
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [NotMap]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Nơi cấp CMND/CCCD
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp CMND/CCCD
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string LandlinePhoneNumber { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string BankBranch { get; set; }


        #endregion
    }
}
