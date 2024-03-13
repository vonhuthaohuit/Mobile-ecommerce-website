using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult LineChart()
        {
            using (var context = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities())
            {
                var dataPoints = context.Database.SqlQuery<DataPoint>(
                    "SELECT CONVERT(DATE, NGAYTHANHTOAN) AS NGAY, SUM(TONGTIEN) AS TONGTIEN " +
                    "FROM THANHTOANDONMUA " +
                    "GROUP BY CONVERT(DATE, NGAYTHANHTOAN) " +
                    "ORDER BY CONVERT(DATE, NGAYTHANHTOAN);")
                    .ToList();
                return View(dataPoints);
            }
        }
        public ActionResult ProductSalesChart()
        {
            using (var context = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities())
            {
                var sold_product = context.Database.SqlQuery<Sold_Product>(
                    "SELECT SP.TENSANPHAM AS ProductName, SUM(CDM.SOLUONG) AS QuantitySold " +
                    "FROM CHITIETDONMUA CDM " +
                    "JOIN CHITIETSANPHAM SP ON CDM.MACHITIETSANPHAM = SP.MACHITIETSANPHAM " +
                    "GROUP BY SP.TENSANPHAM;")
                    .ToList();

                return View(sold_product);
            }
        }
    }
}