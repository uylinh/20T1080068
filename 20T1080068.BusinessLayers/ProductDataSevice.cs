using _20T1080068.DataLayers;
using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1080068.DataLayers.SQLServer;
using _20T1080068.Datalayers;

namespace _20T1080068.BusinessLayers
{
    public class ProductDataSevice
    {
        private static readonly IProductDAL productDB;

        static ProductDataSevice()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            productDB = new Datalayers.SQLServer.ProductDAL(connectionString);
        }

        #region xử lý liên quan đến Mặt hàng 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> ListOfProducts(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = productDB.Count(searchValue);
            return productDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên không phân trang
        /// </summary>
        /// <param name="searchValue"></param> giá trị tìm kiếm ( chuỗi rỗng nếu không tìm kiếm)
        /// <returns></returns>
        public static List<Product> ListOfProducts(string searchValue = "")
        {
            return productDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 nhân viên dựa vào mã
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }
        /// <summary>
        /// Bổ sung nhân viên. Hàm trả về mã của nhân viên được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }
        /// <summary>
        ///  Cập nhật nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }
        /// <summary>
        /// xóa nhân viên
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productID)
        {
            return productDB.Delete(productID);
        }
        /// <summary>
        /// Kiểm tra xem nhân viên có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static bool InUserProduct(int productID)
        {
            return productDB.InUsed(productID);
        }
        #endregion
    }
}

