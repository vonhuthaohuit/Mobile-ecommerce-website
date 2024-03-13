using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;



namespace DoAn_LTW.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private readonly QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();

        // Hiển Thị Loại Sản Phẩm
        [HttpGet]
        public ActionResult Display_Loai_San_Pham()
        {
            List<LOAISANPHAM> lst_lsp = db.LOAISANPHAMs.OrderBy(x => x.ID).ToList();
            return View(lst_lsp);
        }
        // Thêm Loại Sản Phẩm
        [HttpGet]
        public ActionResult Create_Loai_San_Pham()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create_Loai_San_Pham(LOAISANPHAM lsp)
        {
            db.LOAISANPHAMs.Add(lsp);
            db.SaveChanges();
            return RedirectToAction("Display_Loai_San_Pham", "Category");
        }

        // Cập Nhật Loại Sản Phẩm
        [HttpGet]
        public ActionResult Update_Loai_San_Pham(int ID)
        {
            LOAISANPHAM lsp = db.LOAISANPHAMs.Where(x => x.ID == ID).SingleOrDefault();
            return View(lsp);
        }
        [HttpPost]
        public ActionResult Update_Loai_San_Pham(int ID, LOAISANPHAM LSP)
        {
            LOAISANPHAM lsp = db.LOAISANPHAMs.Where(x => x.ID == ID).SingleOrDefault();
            lsp.MALOAI = LSP.MALOAI;
            lsp.TENLOAI = LSP.TENLOAI;
            db.SaveChanges();
            return RedirectToAction("Display_Loai_San_Pham", "Category");
        }

        // Xóa Loại Sản Phẩm
        [HttpGet]
        public ActionResult Delete_Loai_San_Pham(int ID)
        {
            LOAISANPHAM lsp = db.LOAISANPHAMs.Find(ID);
            return View(lsp);
        }
        [HttpPost, ActionName("Delete_Loai_San_Pham")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_LSP(int ID)
        {
            LOAISANPHAM lsp = db.LOAISANPHAMs.Find(ID);
            db.LOAISANPHAMs.Remove(lsp);
            db.SaveChanges();
            var reloaded_LSP = db.LOAISANPHAMs.ToList();
            return View("Display_Loai_San_Pham", reloaded_LSP);
        }
    }
}