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
    public class AlbumsArtistasController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: AlbumsArtistas
        public ActionResult Index()
        {
            var albumsArtistas = db.AlbumsArtistas.Include(a => a.Album).Include(a => a.Artista);
            return View(albumsArtistas.ToList());
        }

        // GET: AlbumsArtistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumsArtista albumsArtista = db.AlbumsArtistas.Find(id);
            if (albumsArtista == null)
            {
                return HttpNotFound();
            }
            return View(albumsArtista);
        }

        // GET: AlbumsArtistas/Create
        public ActionResult Create()
        {
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome");
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome");
            return View();
        }

        // POST: AlbumsArtistas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlbumsArtista,Ano,idAlbum,idArtista")] AlbumsArtista albumsArtista)
        {
            if (ModelState.IsValid)
            {
                db.AlbumsArtistas.Add(albumsArtista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", albumsArtista.idAlbum);
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", albumsArtista.idArtista);
            return View(albumsArtista);
        }

        // GET: AlbumsArtistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumsArtista albumsArtista = db.AlbumsArtistas.Find(id);
            if (albumsArtista == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", albumsArtista.idAlbum);
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", albumsArtista.idArtista);
            return View(albumsArtista);
        }

        // POST: AlbumsArtistas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAlbumsArtista,Ano,idAlbum,idArtista")] AlbumsArtista albumsArtista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumsArtista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", albumsArtista.idAlbum);
            ViewBag.idArtista = new SelectList(db.Artistas, "idArtista", "Nome", albumsArtista.idArtista);
            return View(albumsArtista);
        }

        // GET: AlbumsArtistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumsArtista albumsArtista = db.AlbumsArtistas.Find(id);
            if (albumsArtista == null)
            {
                return HttpNotFound();
            }
            return View(albumsArtista);
        }

        // POST: AlbumsArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumsArtista albumsArtista = db.AlbumsArtistas.Find(id);
            db.AlbumsArtistas.Remove(albumsArtista);
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
