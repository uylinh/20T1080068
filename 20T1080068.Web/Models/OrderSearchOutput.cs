using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1080068.Web.Models
{      /// <summary>
       /// Lưu trữ kết quả tìm kiếm và phân trang của đơn hàng
       /// </summary>
    public class OrderSearchOutput : PaginationsSearchOutput
    {
        /// <summary>
        /// Dữ liệu đầu ra
        /// </summary>
        public List<Order> Data { get; set; }
        
    }
}