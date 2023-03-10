using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1080068.Web.Models
{
    /// <summary>
    /// Lưu trữ kết quả tìm kiếm và phân trang của nhà cung cấp
    /// </summary>
    public class SupplierSearchOutput : PaginationsSearchOutput
    {
        /// <summary>
        /// Dữ liệu đầu ra
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}
