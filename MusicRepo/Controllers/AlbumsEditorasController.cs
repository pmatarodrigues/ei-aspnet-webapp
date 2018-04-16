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
    public class AlbumsEditorasController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: AlbumsEditoras
        public ActionResult Index()
        {
            var albumsEditoras = db.AlbumsEditoras.Include(a => a.Album).Include(a => a.Editora);
            return View(albumsEditoras.ToList());
        }

        // GET: AlbumsEditoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumsEditora albumsEditora = db.AlbumsEditoras.Find(id);
            if (albumsEditora == null)
            {
                return HttpNotFound();
            }
            return View(albumsEditora);
        }

        // GET: AlbumsEditoras/Create
        public ActionResult Create()
        {
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome");
            ViewBag.idEditora = new SelectList(db.Editoras, "idEditora", "Nome");
            return View();
        }

        // POST: AlbumsEditoras/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlbumsEditora,Ano,idAlbum,idEditora")] AlbumsEditora albumsEditora)
        {
            if (ModelState.IsValid)
            {
                db.AlbumsEditoras.Add(albumsEditora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", albumsEditora.idAlbum);
            ViewBag.idEditora = new SelectList(db.Editoras, "idEditora", "Nome", albumsEditora.idEditora);
            return View(albumsEditora);
        }

        // GET: AlbumsEditoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumsEditora albumsEditora = db.AlbumsEditoras.Find(id);
            if (albumsEditora == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", albumsEditora.idAlbum);
            ViewBag.idEditora = new SelectList(db.Editoras, "idEditora", "Nome", albumsEditora.idEditora);
            return View(albumsEditora);
        }

        // POST: AlbumsEditoras/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAlbumsEditora,Ano,idAlbum,idEditora")] AlbumsEditora albumsEditora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumsEditora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAlbum = new SelectList(db.Albums, "idAlbum", "Nome", albumsEditora.idAlbum);
            ViewBag.idEditora = new SelectList(db.Editoras, "idEditora", "Nome", albumsEditora.idEditora);
            return View(albumsEditora);
        }

        // GET: AlbumsEditoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumsEditora albumsEditora = db.AlbumsEditoras.Find(id);
            if (albumsEditora == null)
            {
                return HttpNotFound();
            }
            return View(albumsEditora);
        }

        // POST: AlbumsEditoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumsEditora albumsEditora = db.AlbumsEditoras.Find(id);
            db.AlbumsEditoras.Remove(albumsEditora);
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
