using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class KHO_CHITIETSANPHAMController : Controller
    {
        private QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();

        // GET: KHO_CHITIETSANPHAM
        public ActionResult Index()
        {
            return View(db.KHO_CHITIETSANPHAM.ToList());
        }

        // GET: KHO_CHITIETSANPHAM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO_CHITIETSANPHAM kHO_CHITIETSANPHAM = db.KHO_CHITIETSANPHAM.Find(id);
            if (kHO_CHITIETSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(kHO_CHITIETSANPHAM);
        }

        // GET: KHO_CHITIETSANPHAM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KHO_CHITIETSANPHAM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MAKHO,MACHITIETSANPHAM,SOLUONG")] KHO_CHITIETSANPHAM kHO_CHITIETSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.KHO_CHITIETSANPHAM.Add(kHO_CHITIETSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHO_CHITIETSANPHAM);
        }

        // GET: KHO_CHITIETSANPHAM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO_CHITIETSANPHAM kHO_CHITIETSANPHAM = db.KHO_CHITIETSANPHAM.Find(id);
            if (kHO_CHITIETSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(kHO_CHITIETSANPHAM);
        }

        // POST: KHO_CHITIETSANPHAM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MAKHO,MACHITIETSANPHAM,SOLUONG")] KHO_CHITIETSANPHAM kHO_CHITIETSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHO_CHITIETSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHO_CHITIETSANPHAM);
        }

        // GET: KHO_CHITIETSANPHAM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO_CHITIETSANPHAM kHO_CHITIETSANPHAM = db.KHO_CHITIETSANPHAM.Find(id);
            if (kHO_CHITIETSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(kHO_CHITIETSANPHAM);
        }

        // POST: KHO_CHITIETSANPHAM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHO_CHITIETSANPHAM kHO_CHITIETSANPHAM = db.KHO_CHITIETSANPHAM.Find(id);
            db.KHO_CHITIETSANPHAM.Remove(kHO_CHITIETSANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
