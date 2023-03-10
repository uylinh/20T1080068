using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1080068.DomainModels
{
    public class Custommer
    {
        /// <summary>
        ///  Mã nhà cung cấp
        /// </summary>
        public int CustommerID { get; set; }
        /// <summary>
        /// Tên nhà khách hàng
        /// </summary>
        public string CustommerName { get; set; }
        /// <summary>
        /// Tên liên lạc
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address{ get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Mã bưu chính
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }

      
    }

}
