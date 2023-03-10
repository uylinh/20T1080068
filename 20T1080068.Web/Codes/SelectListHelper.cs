using _20T1080068.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20T1080068.Web
{

        /// <summary>
        ///  Lớp chức năng lựa chọn
        /// </summary>
        public static class SelectListHelper
        {
            /// <summary>
            /// Lấy danh sách quốc gia
            /// </summary>
            /// <returns></returns>
            public static List<SelectListItem> Countries()
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem()
                {
                    Value = "",
                    Text = "--Chọn quốc gia--"
                });
                foreach (var item in CommonDataService.ListOfCountries())
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.CountryName,
                        Text = item.CountryName
                    });
                }
                return list;
            }
            /// <summary>
            /// Lấy danh sách loại hàng
            /// </summary>
            /// <returns></returns>
            public static List<SelectListItem> Categories()
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "--Chọn loại hàng--"
                });
                foreach (var item in CommonDataService.ListOfCategorys(""))
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.CategoryID.ToString(),
                        Text = item.CategoryName
                    });
                }
                return list;
            }
            /// <summary>
            /// Lấy danh sách nhà cung cấp
            /// </summary>
            /// <returns></returns>
            public static List<SelectListItem> Suppliers()
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "--Chọn nhà cung cấp--"
                });
                foreach (var item in CommonDataService.ListOfSuppliers(""))
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.SupplierID.ToString(),
                        Text = item.SupplierName
                    });
                }
                return list;
            }
            /// <summary>
            /// Lấy danh sách khách hàng
            /// </summary>
            /// <returns></returns>
            public static List<SelectListItem> Customers()
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "--Chọn khách hàng--"
                });
                foreach (var item in CommonDataService.ListOfCustomers(""))
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.CustommerID.ToString(),
                        Text = item.CustommerName
                    });
                }
                return list;
            }
            /// <summary>
            /// Lấy danh sách nhân viên
            /// </summary>
            /// <returns></returns>
            public static List<SelectListItem> Employees()
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "--Chọn nhân viên--"
                });
                foreach (var item in CommonDataService.ListOfEmployees(""))
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.EmployeeID.ToString(),
                        Text = $"{item.FirstName} {item.LastName}"
                    });
                }
                return list;
            }
            /// <summary>
            /// Lấy danh sách người giao hàng
            /// </summary>
            /// <returns></returns>
            public static List<SelectListItem> Shippers()
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "--Chọn người giao hàng--"
                });
                foreach (var item in CommonDataService.ListOfShippers(""))
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.ShipperID.ToString(),
                        Text = item.ShipperName
                    });
                }
                return list;
            }
        }
 }
