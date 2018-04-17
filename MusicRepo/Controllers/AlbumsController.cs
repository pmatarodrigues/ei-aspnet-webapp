using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MusicRepo.Models
{
    public class AlbumsController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: Albums
        public ActionResult Index(String sortOrder, String searchString)
        {
            ViewBag.NomeSortParm = sortOrder == "Nome" ? "nome_desc" : "Nome";
            ViewBag.AnoSortParm = sortOrder == "Ano" ? "ano_desc" : "Ano";
            ViewBag.ArtistaSortParm = sortOrder == "Artista" ? "artista_desc" : "Artista";
            ViewBag.EditoraSortParm = sortOrder == "Editora" ? "editora_desc" : "Editora";
            var albums = from s in db.Albums
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Nome.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "Nome":
                    albums = albums.OrderBy(s => s.Nome);
                    break;
                case "nome_desc":
                    albums = albums.OrderByDescending(s => s.Nome);
                    break;
                case "Ano":
                    albums = albums.OrderBy(s => s.Ano);
                    break;
                case "ano_desc":
                    albums = albums.OrderByDescending(s => s.Ano);
                    break;
                case "Artista":
                    albums = albums.OrderBy(s => s.Artista);
                    break;
                case "artista_desc":
                    albums = albums.OrderByDescending(s => s.Artista);
                    break;
                case "Editora":
                    albums = albums.OrderBy(s => s.Editora);
                    break;
                case "editora_desc":
                    albums = albums.OrderByDescending(s => s.Editora);
                    break;
                default:
                    albums = albums.OrderBy(s => s.Ano);
                    break;
            }
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome");
            ViewBag.Editora = new SelectList(db.Editoras, "idEditora", "Nome");
            return View();
        }

        // POST: Albums/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlbum,Nome,Artista,Ano,Editora")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", album.Artista);
            ViewBag.Editora = new SelectList(db.Editoras, "idEditora", "Nome", album.Editora);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", album.Artista);
            ViewBag.Editora = new SelectList(db.Editoras, "idEditora", "Nome", album.Editora);
            return View(album);
        }

        // POST: Albums/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAlbum,Nome,Artista,Ano,Editora")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", album.Artista);
            ViewBag.Editora = new SelectList(db.Editoras, "idEditora", "Nome", album.Editora);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
