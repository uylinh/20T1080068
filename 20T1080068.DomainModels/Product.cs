using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1080068.DomainModels
{
    public class Product
    {

        /// <summary>
        ///  Mã mặt hàng
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// Tên mặt hàng
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// Mã loại hàng
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Đơn vị 
        /// </summary>

        public string Unit { get; set; }
        /// <summary>
        /// Giá
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// Ảnh
        /// </summary>
        public string Photo { get; set; }
    }
}
