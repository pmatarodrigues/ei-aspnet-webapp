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
    public class MusicasAlbumsController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: MusicasAlbums
        public ActionResult Index()
        {
            var musicasAlbums = db.MusicasAlbums.Include(m => m.Album).Include(m => m.Musica);
            return View(musicasAlbums.ToList());
        }

        // GET: MusicasAlbums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicasAlbum musicasAlbum = db.MusicasAlbums.Find(id);
            if (musicasAlbum == null)
            {
                return HttpNotFound();
            }
            return View(musicasAlbum);
        }

        // GET: MusicasAlbums/Create
        public ActionResult Create()
        {
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome");
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome");
            return View();
        }

        // POST: MusicasAlbums/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMusicasAlbum,idMusica,idAlbum")] MusicasAlbum musicasAlbum)
        {
            if (ModelState.IsValid)
            {
                db.MusicasAlbums.Add(musicasAlbum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", musicasAlbum.idAlbum);
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome", musicasAlbum.idMusica);
            return View(musicasAlbum);
        }

        // GET: MusicasAlbums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicasAlbum musicasAlbum = db.MusicasAlbums.Find(id);
            if (musicasAlbum == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", musicasAlbum.idAlbum);
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome", musicasAlbum.idMusica);
            return View(musicasAlbum);
        }

        // POST: MusicasAlbums/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMusicasAlbum,idMusica,idAlbum")] MusicasAlbum musicasAlbum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicasAlbum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", musicasAlbum.idAlbum);
            ViewBag.idMusica = new SelectList(db.Musicas, "idMusica", "Nome", musicasAlbum.idMusica);
            return View(musicasAlbum);
        }

        // GET: MusicasAlbums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicasAlbum musicasAlbum = db.MusicasAlbums.Find(id);
            if (musicasAlbum == null)
            {
                return HttpNotFound();
            }
            return View(musicasAlbum);
        }

        // POST: MusicasAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicasAlbum musicasAlbum = db.MusicasAlbums.Find(id);
            db.MusicasAlbums.Remove(musicasAlbum);
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
