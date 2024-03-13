using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class DanhGiaController : Controller
    {
        // GET: DanhGia
        private readonly QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThemDanhGia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemDanhGia(DANHGIA DanhGia)
        {
            if (Session["MaKH"] != null)
            {
                ViewBag.MaKH = Session["MaKH"].ToString();
                ViewBag.MaCTSP = Session["MACHITIETSANPHAM"].ToString();
                if (ModelState.IsValid)
                {
                    DanhGia.MADANHGIA = TaoMaDanhGia();
                    DanhGia.MAKHACHHANG = ViewBag.MaKH;
                    DanhGia.MACHITIETSANPHAM = ViewBag.MaCTSP;
                    db.DANHGIAs.Add(DanhGia);

                    db.SaveChanges();

                    return RedirectToAction("Click_Show_Detail", "Product", new { ID = DanhGia.ID });
                }
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }

            return View(DanhGia);
        }

        public ActionResult RenderReviewForm()
        {
            return PartialView("ThemDanhGia");
        }

        public string TaoMaDanhGia()
        {
            var lastId = db.DANHGIAs
                .OrderByDescending(d => d.MADANHGIA)
                .Select(d => d.MADANHGIA)
                .FirstOrDefault();

            int nextId = 1;

            if (lastId != null)
            {
                int.TryParse(lastId.Substring(2), out int lastNumber);
                nextId = lastNumber + 1;
            }

            return $"DG{nextId:D5}";
        }

        public ActionResult hienThi()
        {
            return View();
        }

        public ActionResult ShowComment()
        {
            List<DANHGIA> listDanhGia = db.DANHGIAs.Where(d => d.DIEMDANHGIA == 5).Take(5).ToList();
            if (Session["User"] == null)
            {
                ViewBag.UserName = "";

            }
            else
            {
                ViewBag.UserName = Session["User"].ToString();
            }

            return PartialView(listDanhGia);
        }

        public ActionResult ShowAllComment()
        {
            List<DANHGIA> listDanhGia = db.DANHGIAs.ToList();
            return View(listDanhGia);
        }

        public ActionResult FilterByRating(int rating)
        {
            if (rating == 0)
            {
                var allReviews = db.DANHGIAs.Where(d => d.DIEMDANHGIA == 5).Take(5).ToList();
                return PartialView("ShowComment", allReviews);
            }
            else
            {
                var filteredReviews = db.DANHGIAs.Where(d => d.DIEMDANHGIA == rating).Take(5).ToList();
                return PartialView("ShowComment", filteredReviews);
            }
        }

        [HttpPost]
        public ActionResult XoaDanhGia(string id)
        {
            try
            {
                var review = db.DANHGIAs.FirstOrDefault(d => d.MADANHGIA == id);

                if (review != null)
                {
                    db.DANHGIAs.Remove(review);
                    db.SaveChanges();
                    var updatedReviews = db.DANHGIAs.Where(d => d.DIEMDANHGIA == 5).Take(5).ToList();
                    return PartialView("ShowComment", updatedReviews);
                }

                return Json(new { success = false, message = "Không tìm thấy đánh giá để xóa." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra: " + ex.Message });
            }
        }

    }
}