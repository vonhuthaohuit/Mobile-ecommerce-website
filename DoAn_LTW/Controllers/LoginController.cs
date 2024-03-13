using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.DAO;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private readonly QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            bool CheckLoginUser = AccountCusDAO.Instance.Check_Login(username, password);
            bool CheckLogin = AccountEmpDAO.Instance.Check_Login(username, password);
            if (CheckLogin == true)
            {
                Session["User"] = username;
                var ma = db.TAIKHOANNHANVIENs.FirstOrDefault(t => t.TENTAIKHOAN == username);
                Session["MaNV"] = ma.MANHANVIEN;
                return RedirectToAction("Display_San_Pham", "Product");
            }
            else if(CheckLoginUser == true)
            {
                Session["User"] = username;
                var ma = db.TAIKHOANKHACHHANGs.FirstOrDefault(t => t.TENTAIKHOAN == username);
                Session["MaKH"] = ma.MAKHACHHANG;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                TempData["error"] = "Thông tin đăng nhập không chính xác !";
                return View();
            }
        }
        public static bool check1 = true;
        public ActionResult DangXuat()
        {
            Session.Remove("User");
            check1 = false;
            return RedirectToAction("DangNhap");
        }
        public new ActionResult Profile()
        {
            var maKH = Session["MaKH"].ToString();
            var khachHang = db.KHACHHANGs.FirstOrDefault(t => t.MAKHACHHANG == maKH);
            var profile = db.People.FirstOrDefault(t => t.MAPERSON == khachHang.MAPERSON);
            PERSON pERSON = profile;
            return View(pERSON);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON p = db.People.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TEN,SODIENTHOAI,EMAIL,DIACHI,GIOITINH")] PERSON p)
        {
            var existingPerson = db.People.FirstOrDefault(t => t.ID == p.ID);

            if (existingPerson != null)
            {
                // Cập nhật thông tin của đối tượng từ dữ liệu nhập vào
                existingPerson.TEN = p.TEN;
                existingPerson.SODIENTHOAI = p.SODIENTHOAI;
                existingPerson.EMAIL = p.EMAIL;
                existingPerson.DIACHI = p.DIACHI;
                existingPerson.GIOITINH = p.GIOITINH;

                db.SaveChanges();

                return RedirectToAction("Profile", "Login");
            }
            else
            {
                return HttpNotFound();
            }
        }



        //đăng kí tk
        public ActionResult Register()
        {
            return View();
        }
        public bool ComparePasswords(string password, string reEnterPassword)
        {
            return password == reEnterPassword;
        }
        [HttpPost]
        public ActionResult Register(string FirstName, string LastName, string Username, string Email, string Address, string GioiTinh, string Phone, string Pass, string RetryPass)
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(GioiTinh) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Pass) || string.IsNullOrEmpty(RetryPass))
            {
                Session["Error"] = "Bạn cần nhật đầy đủ thông tin !";
                return RedirectToAction("Index");
            }
            if (!ComparePasswords(Pass, RetryPass))
            {
                Session["CheckPass"] = "Mật khẩu mới không trung khớp !";
            }
            else
            {
                var CheckEmail = db.People.SingleOrDefault(t => t.EMAIL == Email);
                var CheckPhone = db.People.SingleOrDefault(t => t.SODIENTHOAI == Phone);
                if (CheckEmail != null)
                {
                    Session["CheckEmail"] = "Email đã tồn tại !";
                }
                else
                {
                    if (CheckPhone != null)
                    {
                        Session["CheckPhone"] = "Email đã tồn tại !";
                        return RedirectToAction("Register");
                    }
                    else
                    {
                        string MaxMaPS = db.People.Max(t => t.MAPERSON);
                        PERSON Ps = new PERSON();
                        Ps.MAPERSON = NewPerSonID(MaxMaPS);
                        Ps.TEN = FirstName + " " + LastName;
                        Ps.SODIENTHOAI = Phone;
                        Ps.EMAIL = Email;
                        Ps.DIACHI = Address;
                        Ps.GIOITINH = GioiTinh;
                        db.People.Add(Ps);
                        db.SaveChanges();

                        string MaxMaKH = db.KHACHHANGs.Max(t => t.MAKHACHHANG);
                        KHACHHANG KH = new KHACHHANG();
                        KH.MAKHACHHANG = NewKhachHangID(MaxMaKH);
                        KH.MAPERSON = NewPerSonID(MaxMaPS);

                        db.KHACHHANGs.Add(KH);
                        db.SaveChanges();

                        TAIKHOANKHACHHANG TK = new TAIKHOANKHACHHANG();
                        TK.TENTAIKHOAN = Username;
                        TK.MATKHAU = RetryPass;
                        TK.MAKHACHHANG = NewKhachHangID(MaxMaKH);
                        db.TAIKHOANKHACHHANGs.Add(TK);
                        db.SaveChanges();

                        return RedirectToAction("DangNhap", "Login");
                    }
                }
            }



            return View();
        }
        public string NewPerSonID(string Ma)
        {
            string MaMoi = "P";
            int doDai = Ma.Length;
            string baKiTuCuoi = Ma.Substring(doDai - 3);
            int Temp = int.Parse(baKiTuCuoi) + 1;
            if (Temp < 10)
                MaMoi += "00" + Temp;
            else if (Temp < 10 && Temp < 100)
                MaMoi += "0" + Temp;
            return MaMoi;
        }

        public string NewKhachHangID(string Ma)
        {
            string MaMoi = "KH";
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