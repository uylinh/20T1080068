using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1080068.Web.Models
{
        /// <summary>
        /// Lưu trữ thông tin đầu vào dùng để tìm kiếm, phân trang
        /// Lọc theo mã loại hàng và mã nhà cung cấp cho mặt hàng
        /// </summary>
        public class ProductSearchInput : PaginationSearchInput
        {
            /// <summary>
            /// Mã loại hàng
            /// </summary>
            public int CategoryID { get; set; }
            /// <summary>
            /// Mã nhà cung cấp
            /// </summary>
            public int SupplierID { get; set; }
        }
    
}