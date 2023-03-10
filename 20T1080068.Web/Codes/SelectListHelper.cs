using _20T1080068.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20T1080068.Web.Codes
{
    public class SelectListHelper
    {

        /// <summary>
        /// Danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> countries()
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
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Chọn Loại Hàng --"
            });
            foreach (var item in CommonDataService.ListOfCategorys())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Chọn Nhà Cung Cấp --"
            });
            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }
            return list;
        }

    }
}