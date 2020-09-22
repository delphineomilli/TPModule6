using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using TPModule6.Data;
using TPModule6.Models;

namespace TPModule6.Controllers
{
    public class SamouraisController : Controller
    {
        private TPModule6Context db = new TPModule6Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {

            DojoViewModel dvm = new DojoViewModel();
            dvm.samourai = db.Samourais.FirstOrDefault(x => x.Id == id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(dvm);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            //créer liste arme et la retourner

            DojoViewModel dvm = new DojoViewModel();
            dvm.Armes = db.Armes.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();

            return View(dvm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DojoViewModel dvm)
        {
            if (ModelState.IsValid)
            {
                //récupérer, rattaché l'armeId au samourai
                Samourai samourai = dvm.samourai;
                samourai.Arme = db.Armes.Find(dvm.IdArmes);
                db.Samourais.Add(samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dvm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            DojoViewModel dvm = new DojoViewModel();
            dvm.Armes = db.Armes.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList(); // retourne la liste complète d'armes

            dvm.samourai = db.Samourais.Find(id);
            dvm.IdArmes = dvm.samourai.Arme.Id;   // récupère et affiche l'arme sélectionnée

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dvm.samourai == null)
            {
                return HttpNotFound();
            }
            return View(dvm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Samourai samourai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(samourai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(samourai);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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
