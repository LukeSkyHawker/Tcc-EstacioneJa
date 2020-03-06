using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CentralParking0.Models;

namespace CentralParking0.Controllers
{
    public class LocadorsController : Controller
    {
        private CentralParkingEntities db = new CentralParkingEntities();

        // GET: Locadors
        public ActionResult Index()
        {
            return View(db.Locadors.ToList());
        }

        // GET: Locadors/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locador locador = db.Locadors.Find(id);
            if (locador == null)
            {
                return HttpNotFound();
            }
            return View(locador);
        }

        // GET: Locadors/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Dono,Nome_Dono,Conta_Corrente,CPF,email,usuario,senha")] Locador locador)
        {
            if (ModelState.IsValid)
            {
                db.Locadors.Add(locador);
                db.SaveChanges();
                return RedirectToAction("Login","Home");
            }

            return View(locador);
        }

        // GET: Locadors/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locador locador = db.Locadors.Find(id);
            if (locador == null)
            {
                return HttpNotFound();
            }
            return View(locador);
        }

        // POST: Locadors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Dono,Nome_Dono,Conta_Corrente,CPF,email,usuario,senha,Tipo")] Locador locador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locador);
        }

        // GET: Locadors/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locador locador = db.Locadors.Find(id);
            if (locador == null)
            {
                return HttpNotFound();
            }
            return View(locador);
        }

        // POST: Locadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Locador locador = db.Locadors.Find(id);
            db.Locadors.Remove(locador);
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
