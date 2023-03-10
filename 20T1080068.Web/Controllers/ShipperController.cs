using _20T1080068.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20T1080068.Web.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    { 
        private const int PAGE_SIZE = 5;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(page, PAGE_SIZE, searchValue, out rowCount);
            int pageCount = rowCount / PAGE_SIZE;
            if (rowCount % PAGE_SIZE > 0)
            {
                pageCount += 1;
            }
            ViewBag.Page = page;
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data); // truyền dữ liệu bằng model
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung người giao hàng ";
            return View("Edit");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            ViewBag.Title = "Cập nhật người giao hàng ";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            ViewBag.Title = "Xoá người giao hàng ";
            return View();
        }
    }

}