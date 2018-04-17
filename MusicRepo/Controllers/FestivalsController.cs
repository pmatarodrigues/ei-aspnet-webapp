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
    public class FestivalsController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: Festivals
        public ActionResult Index(String sortOrder, String searchString)
        {
            ViewBag.NomeSortParm = sortOrder == "Nome" ? "nome_desc" : "Nome";
            ViewBag.LocalidadeSortParm = sortOrder == "Localidade" ? "localidade_desc" : "Localidade";
            var festivals = from s in db.Festivals
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                festivals = festivals.Where(s => s.Nome.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "Nome":
                    festivals = festivals.OrderBy(s => s.Nome);
                    break;
                case "nome_desc":
                    festivals = festivals.OrderByDescending(s => s.Nome);
                    break;
                case "Localidade":
                    festivals = festivals.OrderBy(s => s.Localidade);
                    break;
                case "localidade_desc":
                    festivals = festivals.OrderByDescending(s => s.Localidade);
                    break;               
                default:
                    festivals = festivals.OrderBy(s => s.Nome);
                    break;
            }
            return View(festivals.ToList());
        }

        // GET: Festivals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return HttpNotFound();
            }
            return View(festival);
        }

        // GET: Festivals/Create
        public ActionResult Create()
        {
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome");
            return View();
        }

        // POST: Festivals/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFestival,Nome,Artista,Localidade")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                db.Festivals.Add(festival);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", festival.Artista);
            return View(festival);
        }

        // GET: Festivals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return HttpNotFound();
            }
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", festival.Artista);
            return View(festival);
        }

        // POST: Festivals/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFestival,Nome,Artista,Localidade")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                db.Entry(festival).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", festival.Artista);
            return View(festival);
        }

        // GET: Festivals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return HttpNotFound();
            }
            return View(festival);
        }

        // POST: Festivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Festival festival = db.Festivals.Find(id);
            db.Festivals.Remove(festival);
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
