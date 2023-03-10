using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using _20T1080068.DomainModels;
using _20T1080068.Datalayers;
using _20T1080068.DataLayers;
using _20T1080068.Datalayers.SQLServer;

namespace _20T1080068.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng nghiệp vụ xử lý dữ liệu chung liên quan đến:
    /// Quốc gia, nhà cung cấp, khách hàng, người giao hàng, nhân viên, loại hàng
    /// </summary>
    public class CommonDataService
    {
        private static readonly ICountryDAL countryDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Custommer> customerDB;
        private static readonly ICommonDAL<Employees> employeeDB;
        private static readonly ICommonDAL<Shipper> shipperDB;

        /// ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new Datalayers.SQLServer.SupplierDAL(connectionString);
            customerDB = new Datalayers.SQLServer.CustomerDAL(connectionString);
            shipperDB = new Datalayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new Datalayers.SQLServer.EmployeesDAL(connectionString);
            categoryDB = new Datalayers.SQLServer.CategoryDAL(connectionString);


        }

        #region Xử lý liên quan đến quốc gia
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #endregion

        #region xử lý liên quan đến nhà cung cấp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp không phân trang
        /// </summary>
        /// <param name="searchValue"></param> giá trị tìm kiếm ( chuỗi rỗng nếu không tìm kiếm)
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue = "")
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp dựa vào mã
        /// </summary>
        /// <param name="supplierID"></param> 
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        /// <summary>
        /// Bổ sung nhà cung cấp. Hàm trả về mã của nhà cung cấp được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        /// <summary>
        ///  Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }
        /// <summary>
        /// Kiểm tra xem nhà cung cấp có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUserSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion

        #region xử lý liên quan đến khách hàng
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Custommer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng không phân trang
        /// </summary>
        /// <param name="searchValue"></param> giá trị tìm kiếm ( chuỗi rỗng nếu không tìm kiếm)
        /// <returns></returns>
        public static List<Custommer> ListOfCustomers(string searchValue = "")
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 khách hàng dựa vào mã
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Custommer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Bổ sung khách hàng. Hàm trả về mã của khách hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Custommer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        ///  Cập nhật khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Custommer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// xóa khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Kiểm tra xem khách hàng có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUserCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        #endregion

        #region xử lý liên quan đến nhân viên giao hàng
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên giao hàng không phân trang
        /// </summary>
        /// <param name="searchValue"></param> giá trị tìm kiếm ( chuỗi rỗng nếu không tìm kiếm)
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(string searchValue = "")
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 nhân viên giao hàng dựa vào mã
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }
        /// <summary>
        /// Bổ sung nhân viên giao hàng . Hàm trả về mã của nhân viên giao hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        /// <summary>
        ///  Cập nhật nhân viên giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        /// <summary>
        /// xóa nhân viên giao hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }
        /// <summary>
        /// Kiểm tra xem nhân viên giao hàng có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool InUserShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        #endregion

        #region xử lý liên quan đến nhân viên
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employees> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên không phân trang
        /// </summary>
        /// <param name="searchValue"></param> giá trị tìm kiếm ( chuỗi rỗng nếu không tìm kiếm)
        /// <returns></returns>
        public static List<Employees> ListOfEmployees(string searchValue = "")
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 nhân viên dựa vào mã
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static Employees GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }
        /// <summary>
        /// Bổ sung nhân viên. Hàm trả về mã của nhân viên được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employees data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        ///  Cập nhật nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employees data)
        {
            return employeeDB.Update(data);
        }
        /// <summary>
        /// xóa nhân viên
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return employeeDB.Delete(employeeID);
        }
        /// <summary>
        /// Kiểm tra xem nhân viên có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool InUserEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        #endregion

        #region xử lý liên quan đến loại hàng
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategorys(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách loại hàng không phân trang
        /// </summary>
        /// <param name="searchValue"></param> giá trị tìm kiếm ( chuỗi rỗng nếu không tìm kiếm)
        /// <returns></returns>
        public static List<Category> ListOfCategorys(string searchValue = "")
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 loại hàng dựa vào mã
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        /// <summary>
        /// Bổ sung loại hàng. Hàm trả về mã của loại hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        /// <summary>
        ///  Cập nhật loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        /// <summary>
        /// xóa loại hàng
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            return categoryDB.Delete(categoryID);
        }
        /// <summary>
        /// Kiểm tra xem loại hàng có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool InUserCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }

        public static object ListOfSuppliers(int page, object pageSize, string searchValue, int rowCount)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
