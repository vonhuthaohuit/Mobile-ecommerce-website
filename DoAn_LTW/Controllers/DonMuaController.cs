using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DoAn_LTW.Models;
using DoAn_LTW.ConnectDatabase;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace DoAn_LTW.Controllers
{
    public class DonMuaController : Controller
    {
        // GET: DonHang
        private readonly QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DonMua()
        {
            DBDonHang donHang = new DBDonHang();
            Nullable<double> tongThanhTien = 0;
            string makh = Session["MaKH"].ToString();
            var donhang = db.DONMUAs.FirstOrDefault(t => t.MAKHACHHANG == makh);


            List<PROC_GET_DATA_CHI_TIET_DON_MUA_Result> data = donHang.GetDonMua(makh, donhang.MADONMUA).ToList();
            foreach (var item in data)
            {
                tongThanhTien += (item.GIA * item.SOLUONG);
            }
            ViewBag.TongThanhTien = tongThanhTien;
            return View(donHang.GetDonMua(makh, donhang.MADONMUA).ToList());
        }
        public ActionResult DonMuaDaThanhToan()
        {
            string makh = Session["MaKH"].ToString();
            var donhang = db.DONMUAs.FirstOrDefault(t => t.MAKHACHHANG == makh);
            DBDonHang donHang = new DBDonHang();
            List<PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN_Result> data = donHang.GetDonMuaDaThanhToan(makh, donhang.MADONMUA).ToList();
            Nullable<double> tongThanhTien = 0;
            foreach (var item in data)
            {
                tongThanhTien += (item.GIA * item.SOLUONG);
            }
            ViewBag.TongThanhTien = tongThanhTien;
            return View(donHang.GetDonMuaDaThanhToan(makh, donhang.MADONMUA).ToList());
        }
        public ActionResult DonMuaChuaThanhToan()
        { 
            string makh = Session["MaKH"].ToString();
            var donhang = db.DONMUAs.FirstOrDefault(t => t.MAKHACHHANG == makh);
            DBDonHang donHang = new DBDonHang();
            Nullable<double> tongThanhTien = 0;
            
            List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result> donMuaChuaThanhToanData = donHang.GetDonMuaChuaThanhToan(makh, donhang.MADONMUA).ToList();
            foreach (var item in donMuaChuaThanhToanData)
            {
                tongThanhTien += (item.GIA * item.SOLUONG);
            }
            ViewBag.TongThanhTien = tongThanhTien;
            // Lưu dữ liệu vào Session
            Session["DonMuaChuaThanhToan"] = donMuaChuaThanhToanData;
            if (donMuaChuaThanhToanData.Count == 0)
            {
                return View("NonDonMua");
            }
            return View(donMuaChuaThanhToanData);
        }
        public ActionResult XoaDonMua(string TEN)
        {
            string makh = Session["MaKH"].ToString();
            var donhang = db.DONMUAs.FirstOrDefault(t => t.MAKHACHHANG == makh);
            var CTDM = db.CHITIETDONMUAs.FirstOrDefault(t => t.MADONMUA == donhang.MADONMUA);
            DBDonHang donHang = new DBDonHang();

            foreach (var item in db.CHITIETSANPHAMs)
            {
                if (item.TENSANPHAM == TEN)
                {
                    var del = db.CHITIETDONMUAs.FirstOrDefault(t => t.MACHITIETSANPHAM == item.MACHITIETSANPHAM && t.MADONMUA == CTDM.MADONMUA);
                    db.CHITIETDONMUAs.Remove(del);
                }
            }
            
            db.SaveChanges();
            
            return View("DonMuaChuaThanhToan");

        }
        public ActionResult ThanhToanDonMua()
        {
            List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result> dataDonMuaChuaThanhToan = Session["DonMuaChuaThanhToan"] as List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result>;
            if (dataDonMuaChuaThanhToan.Count != 0)
            {
                Nullable<double> tongThanhTien = 0;
                foreach (var item in dataDonMuaChuaThanhToan)
                {
                    tongThanhTien += (item.GIA * item.SOLUONG);
                }
                ViewBag.TongThanhTien = tongThanhTien;
                Session["TongThanhTien"] = tongThanhTien;
                // Xóa dữ liệu khỏi Session nếu bạn đã sử dụng xong
                Session.Remove("DonMuaChuaThanhToan");
            }
            else
            {
                return View("NonDonMua");
            }
            return View(dataDonMuaChuaThanhToan);
        }
        public ActionResult NonDonMua()
        {
            return View();
        }
        public ActionResult SendEmail()
        {
            string makh = Session["MaKH"].ToString();
            var donhang = db.DONMUAs.FirstOrDefault(t => t.MAKHACHHANG == makh);


            string strSanPham = "";
            double? tongTien = 0;
            List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result> dataDonMuaChuaThanhToan = Session["DonMuaChuaThanhToan"] as List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result>;
            foreach (var sp in dataDonMuaChuaThanhToan)
            {
                strSanPham += "<tr>";
                strSanPham += "<td>"+ sp.TENSANPHAM +"</td>";
                strSanPham += "<td>"+ sp.SOLUONG +"</td>";
                strSanPham += "<td>" + sp.GIA + "</td>";
                strSanPham += "</tr>";
                tongTien = sp.SOLUONG * sp.GIA;
            }

            var check = db.KHACHHANGs.FirstOrDefault(t => t.MAKHACHHANG == donhang.MAKHACHHANG);
            // Địa chỉ email người nhận
            string maDonMua = donhang.MADONMUA;
            var khachHang = db.People.FirstOrDefault(t => t.MAPERSON == check.MAPERSON);
            string toEmailAddress = khachHang.EMAIL;
            string path = Server.MapPath("~/Content/templates/send2.html");
            string body = System.IO.File.ReadAllText(path);
            body = body.Replace("{{MaDonMua}}", maDonMua/*Session["DonMua"].ToString()*/);
            body = body.Replace("{{NgayDat}}", DateTime.Now.ToString());
            body = body.Replace("{{SanPham}}", strSanPham);
            body = body.Replace("{{TenKhachHang}}", khachHang.TEN/*Session["TenKhachHang"].ToString()*/);
            body = body.Replace("{{SoDienThoai}}", khachHang.SODIENTHOAI/*Session["SoDienThoai"].ToString()*/);
            body = body.Replace("{{Email}}", khachHang.EMAIL/*Session["Email"].ToString()*/);
            body = body.Replace("{{DiaChi}}", khachHang.DIACHI/*Session["DiaChi"].ToString()*/);
            body = body.Replace("{{TongTien}}", tongTien.ToString());

            // Tạo đối tượng MailMessage
            MailMessage mail = new MailMessage("vonhuthao11235@gmail.com", toEmailAddress);
            mail.Subject = "ĐƠN ĐẶT HÀNG";
            mail.Body = body;
            mail.IsBodyHtml = true;

            // Cấu hình SmtpClient
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("vonhuthao11235@gmail.com", "vusa tlus wfax oktc");
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            // Gửi email
            try
            {
                smtpClient.Send(mail);
                ViewBag.Message = "Email đã được gửi thành công!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi gửi email: " + ex.Message;
            }

            return View();
        }
        public ActionResult ThanhToan()
        {
            var person = db.People.ToList();
            return View(person);
        }
        public ActionResult ThanhToanKhachHang(string maPerson)
        {
            DBDonHang donHang = new DBDonHang();
            var a = db.KHACHHANGs.FirstOrDefault(t => t.MAPERSON == maPerson);
            string makh = a.MAKHACHHANG.ToString();
            var donhang = db.DONMUAs.FirstOrDefault(t => t.MAKHACHHANG == makh);
            Session["MaDonMua"] = donhang.MADONMUA;

            List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result> donMuaChuaThanhToanData = donHang.GetDonMuaChuaThanhToan(makh, donhang.MADONMUA).ToList();
            ViewBag.ListDonMuaChuaThanhToan = donMuaChuaThanhToanData;
            Nullable<double> tongThanhTien = 0;
            foreach (var item in donMuaChuaThanhToanData)
            {
                tongThanhTien += (item.GIA * item.SOLUONG);
            }

            ViewBag.TongTien = tongThanhTien;
            return View(donMuaChuaThanhToanData);
        }
        public ActionResult LuuCSDL()
        {
            string maDonMua = Session["MaDonMua"].ToString();
            var a = db.THANHTOANDONMUAs.FirstOrDefault(t => t.MADONMUA == maDonMua);
            var b = db.DONMUAs.FirstOrDefault(t => t.MADONMUA == maDonMua);

            a.NGAYTHANHTOAN = DateTime.Now;
            b.TRANGTHAI = "Đã Thanh Toán";
            b.HINHTHUC = true;
            foreach (var item in db.CHITIETDONMUAs)
            {
                var c = db.CHITIETDONMUAs.FirstOrDefault(t => t.MADONMUA == maDonMua);
                if (c.KIEMTRATHANHTOAN == false)
                    c.KIEMTRATHANHTOAN = true;
            }
            db.SaveChanges();
            return View();
        }
    }
}