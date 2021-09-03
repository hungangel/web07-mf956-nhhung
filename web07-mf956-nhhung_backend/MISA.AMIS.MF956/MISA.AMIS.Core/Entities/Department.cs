using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.AMIS.Core.Enums.EnumClass;

namespace MISA.AMIS.Core.Entities
{
    public class Department:BaseEntity
    {

        #region Property

        /// <summary>
        /// ID đơn vị (phòng ban)
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã đơn vị (phòng ban)
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị (phòng ban)
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả về đơn vị
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
