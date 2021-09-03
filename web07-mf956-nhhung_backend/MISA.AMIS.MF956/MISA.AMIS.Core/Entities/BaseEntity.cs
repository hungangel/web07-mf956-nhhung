using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class BaseEntity
    {


        #region Property
        /// <summary>
        /// Ngày thêm mới
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người thêm mới
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi cuối
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string  ModifiedBy { get; set; }


        #endregion
    }
}
