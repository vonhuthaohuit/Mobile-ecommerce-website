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
    public class KHOesController : Controller
    {
        private QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();

        // GET: KHOes
        public ActionResult Index()
        {
            return View(db.KHOes.ToList());
        }

        // GET: KHOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO kHO = db.KHOes.Find(id);
            if (kHO == null)
            {
                return HttpNotFound();
            }
            return View(kHO);
        }

        // GET: KHOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KHOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MAKHO,TENKHO,DIACHIKHO,SODIENTHOAIKHO")] KHO kHO)
        {
            if (ModelState.IsValid)
            {
                db.KHOes.Add(kHO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHO);
        }

        // GET: KHOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO kHO = db.KHOes.Find(id);
            if (kHO == null)
            {
                return HttpNotFound();
            }
            return View(kHO);
        }

        // POST: KHOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MAKHO,TENKHO,DIACHIKHO,SODIENTHOAIKHO")] KHO kHO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHO);
        }

        // GET: KHOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO kHO = db.KHOes.Find(id);
            if (kHO == null)
            {
                return HttpNotFound();
            }
            return View(kHO);
        }

        // POST: KHOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHO kHO = db.KHOes.Find(id);
            db.KHOes.Remove(kHO);
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
