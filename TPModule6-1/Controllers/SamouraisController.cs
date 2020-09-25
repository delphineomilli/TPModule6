using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPModule6_1.Data;
using TPModule6_1.Extension;
using TPModule6_1.Models;
using TpModule6Bo;

namespace TPModule6_1.Controllers
{
    public class SamouraisController : Controller
    {
        private TPModule6_1Context db = new TPModule6_1Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
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

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiViewModel vm = new SamouraiViewModel();
            //une arme ne peut appartenir qu’à un seul samourai
            vm.Armes = db.Armes.Where(x => !db.Samourais.Select(y => y.Arme.Id).Contains(x.Id)).ToList();
            
            vm.ArtMartiaux = db.ArtMartials.ToList();
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdSelectedArme != null)
                {
                    vm.Samourai.Arme = db.Armes.Find(vm.IdSelectedArme);
                }  
                if (vm.IdSelectedArtMartials != null)
                {
                    //regarde ds la liste si ça contient l'id qu'on lui donne,     booleen :vm.IdSelectedArtMartials.Contains(x.Id)
                    vm.Samourai.ArtMartiaux = db.ArtMartials.Where(x => vm.IdSelectedArtMartials.Contains(x.Id)).ToList();
                }

                db.Samourais.Add(vm.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            vm.Armes = db.Armes.ToList();

            return View(vm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamouraiViewModel vm = new SamouraiViewModel();
            //vm.Samourai = db.Samourais.Include(x => x.Arme).FirstOrDefault(x => x.Id == id);
            vm.Samourai = db.Samourais.Find(id);

            //une arme ne peut appartenir qu’à un seul samourai
            vm.Armes = db.Armes.Where(x => !db.Samourais.Where(z => z.Id != vm.Samourai.Id).Select(y => y.Arme.Id).Contains(x.Id)).ToList();
            vm.ArtMartiaux = db.ArtMartials.ToList();

            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }
            if (vm.Samourai.Arme != null)
            {
                vm.IdSelectedArme = vm.Samourai.Arme.Id;
            }
            if (vm.Samourai.ArtMartiaux != null)
            {
                vm.IdSelectedArtMartials = vm.Samourai.ArtMartiaux.Select(x => x.Id).ToList();
            }

            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //Include(x => x.Arme) récupère l'arme en mode Eager
                var currentSamourai = db.Samourais.Include(x => x.Arme).Include(y => y.ArtMartiaux).FirstOrDefault(x => x.Id == vm.Samourai.Id);
                currentSamourai.CopyIn(vm.Samourai);
                

                if (vm.IdSelectedArme != null)
                {
                    //afficher l'arme sélectionnés à travers l'ID
                    currentSamourai.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdSelectedArme);
                }
                else
                {
                    currentSamourai.Arme = null;
                }
                if (vm.IdSelectedArtMartials != null)
                {
                    //afficher les arts martiaux sélectionnés à travers l'ID
                    currentSamourai.ArtMartiaux = db.ArtMartials.Where(y => vm.IdSelectedArtMartials.Contains(y.Id)).ToList();
                }
                else
                {
                    currentSamourai.ArtMartiaux = null;
                }

                db.Entry(currentSamourai).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vm);
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

        // vm.Armes = db.Armes.Where(x => !db.Samourais.Where(z => z.Id != vm.Samourai.Id).Select(y => y.Arme.Id).Contains(x.Id)).ToList();

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
