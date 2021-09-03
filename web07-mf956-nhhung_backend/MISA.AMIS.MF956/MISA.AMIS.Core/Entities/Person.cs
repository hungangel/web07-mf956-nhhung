using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.AMIS.Core.CustomAttribute.CustomAttribute;
using static MISA.AMIS.Core.Enums.EnumClass;

namespace MISA.AMIS.Core.Entities
{
    public class Person:BaseEntity
    {
        /// <summary>
        /// Họ và tên
        /// </summary>
        [NotNull]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính 0:Nam, 1:Nữ, 2:Khác
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Tên hiển thị của giới tính
        /// </summary>
        [NotMap]
        public string GenderName { get; set; }

        /// <summary>
        /// Số CMND / CCCD
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Đia chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [EmailField]
        public string Email { get; set; }
    }
}
