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
    public class MusicasArtistasController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: MusicasArtistas
        public ActionResult Index()
        {
            var musicasArtistas = db.MusicasArtistas.Include(m => m.Artista).Include(m => m.Musica);
            return View(musicasArtistas.ToList());
        }

        // GET: MusicasArtistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicasArtista musicasArtista = db.MusicasArtistas.Find(id);
            if (musicasArtista == null)
            {
                return HttpNotFound();
            }
            return View(musicasArtista);
        }

        // GET: MusicasArtistas/Create
        public ActionResult Create()
        {
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome");
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome");
            return View();
        }

        // POST: MusicasArtistas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMusicasArtista,idMusica,idArtista")] MusicasArtista musicasArtista)
        {
            if (ModelState.IsValid)
            {
                db.MusicasArtistas.Add(musicasArtista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", musicasArtista.idArtista);
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome", musicasArtista.idMusica);
            return View(musicasArtista);
        }

        // GET: MusicasArtistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicasArtista musicasArtista = db.MusicasArtistas.Find(id);
            if (musicasArtista == null)
            {
                return HttpNotFound();
            }
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", musicasArtista.idArtista);
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome", musicasArtista.idMusica);
            return View(musicasArtista);
        }

        // POST: MusicasArtistas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMusicasArtista,idMusica,idArtista")] MusicasArtista musicasArtista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicasArtista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", musicasArtista.idArtista);
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome", musicasArtista.idMusica);
            return View(musicasArtista);
        }

        // GET: MusicasArtistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicasArtista musicasArtista = db.MusicasArtistas.Find(id);
            if (musicasArtista == null)
            {
                return HttpNotFound();
            }
            return View(musicasArtista);
        }

        // POST: MusicasArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicasArtista musicasArtista = db.MusicasArtistas.Find(id);
            db.MusicasArtistas.Remove(musicasArtista);
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
