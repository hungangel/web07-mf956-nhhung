using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Enums
{
    public static class EnumClass
    {
        /// <summary>
        /// Enum giới tính
        /// </summary>
        /// CreatedBy: NHHung(28/08/2021)
        public enum Gender
        {
            Male = 0,
            Female = 1,
            Other = 2
        }

        /// <summary>
        /// Enum trạng thái của ServiceResult
        /// </summary>
        /// CreatedBy: NHHung(28/08/2021)
        public enum ServiceCode
        {
            OK = 200,
            Created = 201,
            NoContent = 204,
            BadRequest = 400,
            NotFound = 404,
            InternalServerError = 500,
        }

        /// <summary>
        /// Enum kết quả validate
        /// </summary>
        /// CreatedBy: NHHung(28/08/2021)
        public enum ValidateCode
        {
            OK = 0,
            IsNull = 1,
            InvalidFormat = 2,
            IsExisted = 3,
            NotMatched = 4,
        }
    }
}
