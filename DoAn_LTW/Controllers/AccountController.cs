using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register_Information()
        {
            ViewBag.MAPERSON = TempData["MAPERSON"]?.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Register_Information(PERSON model)
        {
            db.People.Add(model);
            db.SaveChanges();
            TempData["MAPERSON"] = model.MAPERSON;
            return RedirectToAction("Register_NhanVien", "Account");
        }


        public ActionResult Register_NhanVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register_NhanVien(NHANVIEN model)
        {
            model.MAPERSON = TempData["MAPERSON"]?.ToString();
            db.NHANVIENs.Add(model);
            db.SaveChanges();
            TempData["MANHANVIEN"] = model.MANHANVIEN;
            return RedirectToAction("Register_Account_NV", "Account");
        }



        public ActionResult Register_Account_NV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register_Account_NV(TAIKHOANNHANVIEN model)
        {
            model.MANHANVIEN = TempData["MANHANVIEN"]?.ToString();
            db.TAIKHOANNHANVIENs.Add(model);
            db.SaveChanges();
            return View("Register_Account_NV");
        }
    }
}