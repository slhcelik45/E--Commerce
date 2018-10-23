using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMisas.Models;

namespace EMisas.Controllers
{
    public class AdminDuyurularController : Controller
    {
        private DbMisas db = new DbMisas();

        // GET: AdminDuyurular
        public ActionResult Index()
        {
            var duyurulars = db.Duyurulars.Include(d => d.Tur);
            return View(duyurulars.ToList());
        }

        // GET: AdminDuyurular/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Duyurular duyurular = db.Duyurulars.Find(id);
            if (duyurular == null)
            {
                return HttpNotFound();
            }
            return View(duyurular);
        }

        // GET: AdminDuyurular/Create
        public ActionResult Create()
        {
            ViewBag.TurId = new SelectList(db.Turs, "Id", "Adi");
            return View();
        }

        // POST: AdminDuyurular/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DuyuruId,TurId,Adi,KisaAciklama,Aciklama,Resim")] Duyurular duyurular)
        {
            if (ModelState.IsValid)
            {
                db.Duyurulars.Add(duyurular);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TurId = new SelectList(db.Turs, "Id", "Adi", duyurular.TurId);
            return View(duyurular);
        }

        // GET: AdminDuyurular/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Duyurular duyurular = db.Duyurulars.Find(id);
            if (duyurular == null)
            {
                return HttpNotFound();
            }
            ViewBag.TurId = new SelectList(db.Turs, "Id", "Adi", duyurular.TurId);
            return View(duyurular);
        }

        // POST: AdminDuyurular/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DuyuruId,TurId,Adi,KisaAciklama,Aciklama,Resim")] Duyurular duyurular)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duyurular).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TurId = new SelectList(db.Turs, "Id", "Adi", duyurular.TurId);
            return View(duyurular);
        }

        // GET: AdminDuyurular/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Duyurular duyurular = db.Duyurulars.Find(id);
            if (duyurular == null)
            {
                return HttpNotFound();
            }
            return View(duyurular);
        }

        // POST: AdminDuyurular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Duyurular duyurular = db.Duyurulars.Find(id);
            db.Duyurulars.Remove(duyurular);
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
