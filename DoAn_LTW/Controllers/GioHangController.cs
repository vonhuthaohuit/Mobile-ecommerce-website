using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private readonly QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();
        public ActionResult Index()
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("DangNhap", "Login");

            }
            List<Cartxx> listGioHang = getDataGioHang(Session["MaKH"].ToString());
            if (listGioHang.Count == 0)
            {
                ViewBag.ThongBao = 0;
            }
            ViewBag.Quatity = Quatity();
            ViewBag.Price = Price();
            return View(listGioHang);
        }
        public string TaoMaGioHangTuDong()
        {
            var lastId = db.GIOHANGs
                .OrderByDescending(d => d.MAGIOHANG)
                .Select(d => d.MAGIOHANG)
                .FirstOrDefault();

            int nextId = 1;

            if (lastId != null)
            {
                int.TryParse(lastId.Substring(2), out int lastNumber);
                nextId = lastNumber + 1;
            }

            return $"{nextId:D2}";
        }
        public ActionResult AddCart(string maCTSP)
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("DangNhap", "Login");
            }


            string khachHang = Session["MaKH"].ToString();
            var gh = db.GIOHANGs.FirstOrDefault(t => t.MAKHACHHANG == khachHang);

            if (gh != null)
            {
                GIOHANG gioHang = new GIOHANG();
                gioHang.MAGIOHANG = "GH" + TaoMaGioHangTuDong();
                gioHang.MACHITIETSANPHAM = maCTSP;
                gioHang.MAKHACHHANG = Session["MaKH"].ToString();
                gioHang.SOLUONG = gioHang.SOLUONG + 1;
                gioHang.THANHTIEN = 0;
                db.GIOHANGs.Add(gioHang);
                db.SaveChanges();
                Session["MaGioHang"] = gioHang.MAGIOHANG;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                GIOHANG gioHang = new GIOHANG();
                gioHang.MAGIOHANG = "GH" + TaoMaGioHangTuDong();
                gioHang.MACHITIETSANPHAM = maCTSP;
                gioHang.MAKHACHHANG = Session["MaKH"].ToString();
                gioHang.SOLUONG = 1;
                gioHang.THANHTIEN = 0;
                db.GIOHANGs.Add(gioHang);
                db.SaveChanges();
                Session["MaGioHang"] = gioHang.MAGIOHANG;
                return RedirectToAction("Index", "Product");
            }
            

        }
        //public ActionResult AddCart(string MaSP, string MaKH, int SL)
        //{
        //    string MaxMaGH = db.GIOHANGs.Max(t => t.MAGIOHANG);
        //    var NewCart = new GIOHANG();
        //    NewCart.MAGIOHANG = NewMaGH(MaxMaGH);
        //    NewCart.MACHITIETSANPHAM = MaSP;
        //    NewCart.SOLUONG = SL;
        //    NewCart.MAKHACHHANG = MaKH;
        //    db.GIOHANGs.Add(NewCart);
        //    db.SaveChanges();
        //    return RedirectToAction("");
        //}
        //public string NewMaGH(string Ma)
        //{
        //    string MaMoi = "GH";
        //    int doDai = Ma.Length;
        //    string baKiTuCuoi = Ma.Substring(doDai - 3);
        //    int Temp = int.Parse(baKiTuCuoi) + 1;
        //    if (Temp < 10)
        //        MaMoi += "00" + Temp;
        //    else if (Temp < 10 && Temp < 100)
        //        MaMoi += "0" + Temp;
        //    return MaMoi;
        //}
        public int Quatity()
        {
            int quatity = 0;
            List<Cartxx> cartxxes = Session["GioHang"] as List<Cartxx>;
            if (cartxxes != null)
            {
                quatity += cartxxes.Sum(t => t.SOLUONG);
            }
            return quatity;
        }
        public decimal Price()
        {
            decimal price = 0;
            List<Cartxx> cartxxes = Session["GioHang"] as List<Cartxx>;
            if (cartxxes != null)
            {
                price += cartxxes.Sum(t => t.THANHTIEN);
            }
            return price;
        }
        public List<Cartxx> getDataGioHang(string MaKH)
        {
            List<Cartxx> GH = Session["GioHang"] as List<Cartxx>;
            GH = null;
            if (GH == null)
            {
                GH = ListCart.Instance.LoadCart(MaKH);
                Session["GioHang"] = GH;
            }
            return GH;
        }

        public ActionResult Delete_GioHang(string MaSP)
        {
            List<Cartxx> gioHangs = getDataGioHang(Session["MaKH"].ToString());
            Cartxx gh = gioHangs.SingleOrDefault(t => t.MAGIOHANG == MaSP);


            var GH = db.GIOHANGs.SingleOrDefault(t => t.MAGIOHANG == MaSP);
            db.GIOHANGs.Remove(GH);

            // Lưu các thay đổi xuống cơ sở dữ liệu
            db.SaveChanges();
            if (gh != null)
            {
                gioHangs.RemoveAll(t => t.MAGIOHANG == MaSP);

                return RedirectToAction("Index");
            }
            if (gioHangs.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Update_GioHang(string MaSP, FormCollection f)
        {
            List<Cartxx> gioHangs = getDataGioHang(Session["MaKH"].ToString());
            Cartxx gh = gioHangs.SingleOrDefault(t => t.MAGIOHANG == MaSP);
            if (gh != null)
            {
                // Truy vấn dữ liệu
                var products = db.GIOHANGs.Where(p => p.MAGIOHANG == MaSP).ToList();

                // Thay đổi dữ liệu
                foreach (var product in products)
                {
                    product.SOLUONG = int.Parse(f["txtSoLuong"].ToString());
                }

                // Lưu các thay đổi xuống cơ sở dữ liệu
                db.SaveChanges();
            }
            // Khởi tạo đối tượng DbContext

            return RedirectToAction("Index");
        }

        public ActionResult Tru_SoLuong(string MaSP, FormCollection f)
        {
            List<Cartxx> gioHangs = getDataGioHang(Session["MaKH"].ToString());
            Cartxx gh = gioHangs.SingleOrDefault(t => t.MAGIOHANG == MaSP);
            if (gh != null)
            {
                // Truy vấn dữ liệu
                var products = db.GIOHANGs.Where(p => p.MAGIOHANG == MaSP).ToList();

                // Thay đổi dữ liệu
                foreach (var product in products)
                {

                    if (product.SOLUONG < 1)
                    {
                        return RedirectToAction("Delete_GioHang");
                    }
                    else
                    {
                        product.SOLUONG = product.SOLUONG - 1;
                    }
                }

                // Lưu các thay đổi xuống cơ sở dữ liệu
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Them_SoLuong(string MaSP, FormCollection f)
        {
            List<Cartxx> gioHangs = getDataGioHang(Session["MaKH"].ToString());
            Cartxx gh = gioHangs.SingleOrDefault(t => t.MAGIOHANG == MaSP);
            if (gh != null)
            {
                // Truy vấn dữ liệu
                var products = db.GIOHANGs.Where(p => p.MAGIOHANG == MaSP).ToList();

                // Thay đổi dữ liệu
                foreach (var product in products)
                {
                    product.SOLUONG = product.SOLUONG + 1;
                }

                // Lưu các thay đổi xuống cơ sở dữ liệu
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public string TaoMaDonMuaTuDong()
        {
            var lastId = db.DONMUAs
                .OrderByDescending(d => d.MADONMUA)
                .Select(d => d.MADONMUA)
                .FirstOrDefault();

            int nextId = 1;

            if (lastId != null)
            {
                int.TryParse(lastId.Substring(2), out int lastNumber);
                nextId = lastNumber + 1;
            }

            return $"{nextId:D3}";
        }
        public string TaoCTMaDonMuaTuDong()
        {
            var lastId = db.CHITIETDONMUAs
                .OrderByDescending(d => d.MACHITIETDONMUA)
                .Select(d => d.MACHITIETDONMUA)
                .FirstOrDefault();

            int nextId = 1;

            if (lastId != null)
            {
                int.TryParse(lastId.Substring(4), out int lastNumber);
                nextId = lastNumber + 1;
            }

            return $"{nextId:D3}";
        }
        public ActionResult LoadBill()
        {
            string maKH = Session["MaKH"].ToString();

            List<Cartxx> gioHangs = getDataGioHang(maKH);

            string Temp = db.DONMUAs.Max(t => t.MADONMUA);


            var donMua = db.DONMUAs.FirstOrDefault(t => t.MAKHACHHANG == maKH);

            //var NewDonMua = new DONMUA
            //{
            //    MADONMUA = ,
            //    MAKHACHHANG = Session["MaKH"].ToString(),
            //    NGAYDATHANG = DateTime.Now,
            //    TRANGTHAI = "Đã đặt hàng",
            //    HINHTHUC = false
            //};
            //DONMUA donhang = new DONMUA();
            //donhang.MADONMUA = NewDonMua.MADONMUA;
            //donhang.MAKHACHHANG = NewDonMua.MAKHACHHANG;
            //donhang.NGAYDATHANG = NewDonMua.NGAYDATHANG;
            //donhang.TRANGTHAI = NewDonMua.TRANGTHAI;
            //donhang.HINHTHUC = NewDonMua.HINHTHUC;

            //db.DONMUAs.Add(donhang);
            //db.SaveChanges();

            var List_GH = db.GIOHANGs.Where(t => t.MAKHACHHANG == maKH );

            foreach (var row in List_GH)
            {
                string Temp1 = db.CHITIETDONMUAs.Max(t => t.MACHITIETDONMUA);

                var NewChiTietDonMua = new CHITIETDONMUA
                {
                    MACHITIETDONMUA = "CTDM" + TaoCTMaDonMuaTuDong(),
                    MADONMUA = donMua.MADONMUA,
                    MACHITIETSANPHAM = row.MACHITIETSANPHAM,
                    SOLUONG = int.Parse(row.SOLUONG.ToString()),
                    KIEMTRATHANHTOAN = false
                };

                CHITIETDONMUA CTDH = new CHITIETDONMUA();
                CTDH.MACHITIETDONMUA = NewChiTietDonMua.MACHITIETDONMUA;
                CTDH.MADONMUA = NewChiTietDonMua.MADONMUA;
                CTDH.MACHITIETSANPHAM = NewChiTietDonMua.MACHITIETSANPHAM;
                CTDH.SOLUONG = NewChiTietDonMua.SOLUONG;
                CTDH.KIEMTRATHANHTOAN = NewChiTietDonMua.KIEMTRATHANHTOAN;
                db.CHITIETDONMUAs.Add(CTDH);

                var GH_Del = db.GIOHANGs.SingleOrDefault(t => t.MAGIOHANG == row.MAGIOHANG);
                db.GIOHANGs.Remove(GH_Del);
            }
            db.SaveChanges();
            return RedirectToAction("DonMuaChuaThanhToan", "DonMua");
        }

        public string NewMaDH(string Ma)
        {
            string MaMoi = "DH";
            int doDai = Ma.Length;
            string baKiTuCuoi = Ma.Substring(doDai - 3);
            int Temp = int.Parse(baKiTuCuoi) + 1;
            if (Temp < 10)
                MaMoi += "00" + Temp;
            else if (Temp < 10 && Temp < 100)
                MaMoi += "0" + Temp;
            return MaMoi;
        }

        public string NewMaCTDH(string Ma)
        {
            string MaMoi = "CTDH";
            int doDai = Ma.Length;
            string baKiTuCuoi = Ma.Substring(doDai - 3);
            int Temp = int.Parse(baKiTuCuoi) + 1;
            if (Temp < 10)
                MaMoi += "00" + Temp;
            else if (Temp < 10 && Temp < 100)
                MaMoi += "0" + Temp;
            return MaMoi;
        }
    }
}