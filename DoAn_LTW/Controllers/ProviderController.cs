using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Provider
        private QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();

        // Hiển Thị Các Nhà Cung Cấp
        [HttpGet]
        public ActionResult Display_Nha_Cung_Cap()
        {
            List<NHACUNGCAP> lst_ncc = db.NHACUNGCAPs.OrderBy(x => x.ID).ToList();
            return View(lst_ncc);
        }

        // Thêm Nhà Cung Cấp
        [HttpGet]
        public ActionResult Create_Nha_Cung_Cap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create_Nha_Cung_Cap(NHACUNGCAP NCC)
        {
            db.NHACUNGCAPs.Add(NCC);
            db.SaveChanges();
            return RedirectToAction("Display_Nha_Cung_Cap");
        }

        // Cập Nhật Nhà Cung Cấp
        [HttpGet]
        public ActionResult Update_Nha_Cung_Cap(int ID)
        {
            NHACUNGCAP ncc = db.NHACUNGCAPs.Where(x => x.ID == ID).SingleOrDefault();
            return View(ncc);
        }
        [HttpPost]
        public ActionResult Update_Nha_Cung_Cap(int ID, NHACUNGCAP NCC)
        {
            NHACUNGCAP ncc = db.NHACUNGCAPs.Where(x => x.ID == ID).SingleOrDefault();
            ncc.MANCC = NCC.MANCC;
            ncc.TENNCC = NCC.TENNCC;
            ncc.SODIENTHOAI = NCC.SODIENTHOAI;
            db.SaveChanges();
            return RedirectToAction("Display_Nha_Cung_Cap");
        }
    }
}