using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _20T1080068.DomainModels
{
    /// <summary>
    /// mã nhà cung cấp
    /// </summary>
    public class Supplier
    {
        public int SupplierID { get; set; }
        /// <summary>
        ///  tên nhà cung cấp
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// Tên giao dịch
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// số điện thoại
        /// </summary>
        public string Phone { get; set; }
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
       
        
    }
}
