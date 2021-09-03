using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.CustomAttribute
{
    public class CustomAttribute
    {
        /// <summary>
        /// Thuộc tính cho trường Email
        /// </summary>
        /// CreatedBy: NHHung(28/08/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class EmailField : Attribute
        {
        }

        /// <summary>
        /// Thuộc tính cho trường  duy nhất , không được trùng
        /// </summary>
        /// CreatedBy: NHHung(28/08/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class Unique : Attribute
        {
        }

        /// <summary>
        /// Thuộc tính cho trường không bỏ trống
        /// </summary>
        /// CreatedBy: NHHung(28/08/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class NotNull : Attribute
        {
        }

        /// <summary>
        /// Thuộc tính cho trường tên riêng của người
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class PersonName : Attribute
        {
        }

        /// <summary>
        /// Thuộc tính cho nhưng trường của đối tượng mà không ánh xạ vào CSDL
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class NotMap : Attribute
        {
        }
    }
}
