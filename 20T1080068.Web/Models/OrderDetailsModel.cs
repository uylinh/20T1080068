using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1080068.DomainModels;

namespace _20T1080068.Web.Models
{
    public class OrderDetailsModel
    {
        /// <summary>
        /// Lấy ra thông tin đơn hàng
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// Lấy ra thông tin chi tiết của đơn hàng
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }
    }
}