using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicRepo.Models;

namespace MusicRepo.Views
{
    public class ArtistasFestivalsController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: ArtistasFestivals
        public ActionResult Index()
        {
            var artistasFestivals = db.ArtistasFestivals.Include(a => a.Artista).Include(a => a.Festival);
            return View(artistasFestivals.ToList());
        }

        // GET: ArtistasFestivals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistasFestival artistasFestival = db.ArtistasFestivals.Find(id);
            if (artistasFestival == null)
            {
                return HttpNotFound();
            }
            return View(artistasFestival);
        }

        // GET: ArtistasFestivals/Create
        public ActionResult Create()
        {
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome");
            ViewBag.idFestival = new SelectList(db.Festivals, "idFestival", "Nome");
            return View();
        }

        // POST: ArtistasFestivals/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idArtistasFestival,idArtista,idFestival")] ArtistasFestival artistasFestival)
        {
            if (ModelState.IsValid)
            {
                db.ArtistasFestivals.Add(artistasFestival);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", artistasFestival.idArtista);
            ViewBag.idFestival = new SelectList(db.Festivals, "idFestival", "Nome", artistasFestival.idFestival);
            return View(artistasFestival);
        }

        // GET: ArtistasFestivals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistasFestival artistasFestival = db.ArtistasFestivals.Find(id);
            if (artistasFestival == null)
            {
                return HttpNotFound();
            }
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", artistasFestival.idArtista);
            ViewBag.idFestival = new SelectList(db.Festivals, "idFestival", "Nome", artistasFestival.idFestival);
            return View(artistasFestival);
        }

        // POST: ArtistasFestivals/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idArtistasFestival,idArtista,idFestival")] ArtistasFestival artistasFestival)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistasFestival).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", artistasFestival.idArtista);
            ViewBag.idFestival = new SelectList(db.Festivals, "idFestival", "Nome", artistasFestival.idFestival);
            return View(artistasFestival);
        }

        // GET: ArtistasFestivals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistasFestival artistasFestival = db.ArtistasFestivals.Find(id);
            if (artistasFestival == null)
            {
                return HttpNotFound();
            }
            return View(artistasFestival);
        }

        // POST: ArtistasFestivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistasFestival artistasFestival = db.ArtistasFestivals.Find(id);
            db.ArtistasFestivals.Remove(artistasFestival);
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
