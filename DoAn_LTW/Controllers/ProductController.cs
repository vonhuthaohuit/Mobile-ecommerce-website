using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private readonly QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();

        public ActionResult Index()
        {
            var chiTietSanPhams = db.CHITIETSANPHAMs.ToList();
            var loaiSanPhams = db.LOAISANPHAMs.ToList();

            var viewModel = new ViewModel
            {
                CHITIETSANPHAMs = chiTietSanPhams,
                LOAISANPHAMs = loaiSanPhams
            };
            if(Session["MaKH"] != null)
            {
                string khachHang = Session["MaKH"].ToString();
                var a = db.KHACHHANGs.FirstOrDefault(t => t.MAKHACHHANG == khachHang);
                var b = db.People.FirstOrDefault(t => t.MAPERSON == a.MAPERSON);
                ViewBag.TenKhachHang = b.TEN;
                LoginController.check1 = true;
            }
            if (LoginController.check1 == false)
            {
                ViewBag.TenKhachHang = null;
            }
            return View(viewModel);
        }

        public List<CHITIETSANPHAM> SortProducts(IEnumerable<CHITIETSANPHAM> products, string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder == "ASC")
                {
                    products = products.OrderBy(p => p.GIA);
                }
                else if (sortOrder == "DESC")
                {
                    products = products.OrderByDescending(p => p.GIA);
                }
            }

            return products.ToList();
        }

        public ActionResult Search(string searchKeyword, string sortOrder)
        {
            var searchResults = db.CHITIETSANPHAMs
                .Where(p => db.LOAISANPHAMs.Any(l => l.MALOAI == p.MALOAI && l.TENLOAI.Contains(searchKeyword)))
                .Where(p => p.TENSANPHAM.Contains(searchKeyword));

            ViewBag.searchKeyword = searchKeyword;

            IEnumerable<CHITIETSANPHAM> finalResults = null;

            if (string.IsNullOrEmpty(searchKeyword))
            {
                ViewBag.Message = "Không có kết quả bạn cần tìm.";
            }
            else
            {
                finalResults = SortProducts(searchResults, sortOrder);
            }

            ViewBag.finalResults = finalResults;

            return PartialView("Search", finalResults);
        }

        public ActionResult SearchSuggestions(string searchKeyword)
        {
            IEnumerable<CHITIETSANPHAM> suggestedProducts = GetSuggestedProducts(searchKeyword);
            return PartialView("_SearchSuggestionsPartial", suggestedProducts);
        }

        private List<CHITIETSANPHAM> GetSuggestedProducts(string searchKeyword)
        {
            var suggestedProducts = db.CHITIETSANPHAMs
                                        .Where(p => p.TENSANPHAM.Contains(searchKeyword))
                                        .Take(5)
                                        .ToList();
            return suggestedProducts;
        }

        public ActionResult ShowALL(string sortOrder)
        {
            var chiTietSanPhams = db.CHITIETSANPHAMs.ToList();
            var loaiSanPhams = db.LOAISANPHAMs.ToList();

            var viewModel = new ViewModel
            {
                CHITIETSANPHAMs = chiTietSanPhams,
                LOAISANPHAMs = loaiSanPhams
            };

            viewModel.CHITIETSANPHAMs = SortProducts(viewModel.CHITIETSANPHAMs, sortOrder);

            return View(viewModel);
        }
        public ActionResult LoaiSanPhamPartial()
        {
            List<LOAISANPHAM> loaiSP = new List<LOAISANPHAM>();
            foreach (var loai in db.LOAISANPHAMs)
            {
                if (loaiSP.Count() == 5)
                {
                    break;
                }
                loaiSP.Add(loai);
            }
            loaiSP = loaiSP.OrderBy(t => t.TENLOAI).ToList();
            return PartialView(loaiSP);
        }
        public ActionResult ShowProductByType(string id, string sortOrder)
        {
            List<CHITIETSANPHAM> products = new List<CHITIETSANPHAM>();
            ViewBag.nameLoaiSP = db.LOAISANPHAMs.FirstOrDefault(t => t.MALOAI == id)?.TENLOAI;

            if (db.CHITIETSANPHAMs.Count() == 0)
            {
                ViewBag.Error = "Không có sản phẩm nào thuộc loại này";
            }
            else
            {
                products = db.CHITIETSANPHAMs.Where(item => item.MALOAI == id).ToList();
            }

            products = SortProducts(products, sortOrder);

            return View(products);
        }

        [HttpGet]
        public ActionResult Click_Show_Detail(int ID)
        {
            if (Session["MaKH"] != null)
            {
                string khachHang = Session["MaKH"].ToString();
                var a = db.KHACHHANGs.FirstOrDefault(t => t.MAKHACHHANG == khachHang);
                var b = db.People.FirstOrDefault(t => t.MAPERSON == a.MAPERSON);
                ViewBag.TenKhachHang = b.TEN;
            }
            string ma_ctsp;
            var detailProduct = db.CHITIETSANPHAMs.Find(ID);
            ma_ctsp = detailProduct.MACHITIETSANPHAM;

            var relatedProducts = db.CHITIETSANPHAMs.Where(p => p.ID != ID).Take(5).ToList();
            var reviews = db.DANHGIAs.Where(d => d.MACHITIETSANPHAM == ma_ctsp).ToList();

            Session["MACHITIETSANPHAM"] = ma_ctsp;
            ViewBag.MACHITIETSANPHAM = ma_ctsp;

            var view_CTSP = new View_CTSP
            {
                DetailProduct = detailProduct,
                RelatedProducts = relatedProducts,
                Reviews = reviews
            };
            return View(view_CTSP);
        }

        public ActionResult ListSwiperProduct()
        {
            var chiTietSanPhams = db.CHITIETSANPHAMs.ToList();
            var loaiSanPhams = db.LOAISANPHAMs.ToList();

            var viewModel = new ViewModel
            {
                CHITIETSANPHAMs = chiTietSanPhams,
                LOAISANPHAMs = loaiSanPhams
            };

            return View(viewModel);
        }

        // ADMIN ----------------------------------------------------------------
        //----------------------------------------------------------------
        //----------------------------------------------------------------

        // Hiển Thị Loại Sản Phẩm
        [HttpGet]
        public ActionResult Display_San_Pham()
        {
            List<CHITIETSANPHAM> lst_sp = db.CHITIETSANPHAMs.OrderBy(x => x.ID).ToList();
            return View(lst_sp);
        }

        // Thêm Loại Sản Phẩm
        [HttpPost]
        public ActionResult Create_San_Pham(CHITIETSANPHAM sp, HttpPostedFileBase HinhAnhFile)
        {
            if (HinhAnhFile != null && HinhAnhFile.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(HinhAnhFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/assets/img/"), fileName);
                    HinhAnhFile.SaveAs(path);
                    sp.HINHANH = fileName;
                    db.CHITIETSANPHAMs.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("Display_San_Pham", "Product");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Lỗi khi tải lên tệp: " + ex.Message;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Vui lòng chọn một tệp để tải lên.";
            }
            return View("Create_San_Pham", sp);
        }



        // Cập Nhật Loại Sản Phẩm
        [HttpGet]
        public ActionResult Update_San_Pham(int ID)
        {
            CHITIETSANPHAM sp = db.CHITIETSANPHAMs.Find(ID);
            ViewBag.MALOAIList = new SelectList(db.LOAISANPHAMs, "MALOAI", "TenLoai");
            ViewBag.MANCCList = new SelectList(db.NHACUNGCAPs, "MANCC", "TENNCC");
            return View(sp);
        }

        // Action cập nhật sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_San_Pham(int ID, CHITIETSANPHAM SP, HttpPostedFileBase HinhAnhFile)
        {
            CHITIETSANPHAM sp = db.CHITIETSANPHAMs.Where(x => x.ID == ID).SingleOrDefault();
            if (HinhAnhFile != null && HinhAnhFile.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(HinhAnhFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    HinhAnhFile.SaveAs(path);
                    SP.HINHANH = fileName;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Lỗi khi tải lên tệp: " + ex.Message;
                    return View("Edit_San_Pham", SP);
                }
            }

            sp.MACHITIETSANPHAM = SP.MACHITIETSANPHAM;
            sp.MALOAI = SP.MALOAI;
            sp.TENSANPHAM = SP.TENSANPHAM;
            sp.GIA = SP.GIA;
            sp.MAUSAC = SP.MAUSAC;
            sp.MOTASANPHAM = SP.MOTASANPHAM;
            sp.HINHANH = SP.HINHANH;
            sp.MANCC = SP.MANCC;
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Display_San_Pham", "Product");
        }

        // Xóa Loại Sản Phẩm
        [HttpGet]
        public ActionResult Delete_San_Pham(int ID)
        {
            CHITIETSANPHAM sp = db.CHITIETSANPHAMs.Find(ID);
            return View(sp);
        }
        [HttpPost, ActionName("Delete_San_Pham")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_SP(int ID)
        {
            CHITIETSANPHAM sp = db.CHITIETSANPHAMs.Find(ID);
            db.CHITIETSANPHAMs.Remove(sp);
            db.SaveChanges();
            var reloaded_SP = db.CHITIETSANPHAMs.ToList();
            return View("Display_San_Pham", reloaded_SP);
        }

    }
}