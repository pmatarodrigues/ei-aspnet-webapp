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
    public class MusicasController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: Musicas
        public ActionResult Index(String sortOrder, String searchString)
        {
            ViewBag.NomeSortParm = sortOrder == "Nome" ? "nome_desc" : "Nome";
            ViewBag.DuracaoSortParm = sortOrder == "Duracao" ? "duracao_desc" : "Duracao";
            ViewBag.ArtistaSortParm = sortOrder == "Artista" ? "artista_desc" : "Artista";
            ViewBag.AlbumSortParm = sortOrder == "Album" ? "album_desc" : "Album";

            var musicas = from s in db.Musicas
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                musicas = musicas.Where(s => s.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Nome":
                    musicas = musicas.OrderBy(s => s.Nome);
                    break;
                case "nome_desc":
                    musicas = musicas.OrderByDescending(s => s.Nome);
                    break;
                case "Duracao":
                    musicas = musicas.OrderBy(s => s.Duracao);
                    break;
                case "duracao_desc":
                    musicas = musicas.OrderByDescending(s => s.Duracao);
                    break;
                case "Artista":
                    musicas = musicas.OrderBy(s => s.Artista);
                    break;
                case "artista_desc":
                    musicas = musicas.OrderByDescending(s => s.Artista);
                    break;
                case "Album":
                    musicas = musicas.OrderBy(s => s.Album);
                    break;
                case "album_desc":
                    musicas = musicas.OrderByDescending(s => s.Album);
                    break;
                default:
                    musicas = musicas.OrderBy(s => s.Nome);
                    break;
            }
            return View(musicas.ToList());

    }

        // GET: Musicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musica musica = db.Musicas.Find(id);
            if (musica == null)
            {
                return HttpNotFound();
            }
            return View(musica);
        }

        // GET: Musicas/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Album = new SelectList(db.Albums, "idAlbum", "Nome");
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome");
            return View();
        }

        // POST: Musicas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMusica,Nome,Artista,Album,Duracao")] Musica musica)
        {
            if (ModelState.IsValid)
            {
                db.Musicas.Add(musica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Album = new SelectList(db.Albums, "idAlbum", "Nome", musica.Album);
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", musica.Artista);
            return View(musica);
        }

        // GET: Musicas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musica musica = db.Musicas.Find(id);
            if (musica == null)
            {
                return HttpNotFound();
            }
            ViewBag.Album = new SelectList(db.Albums, "idAlbum", "Nome", musica.Album);
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", musica.Artista);
            return View(musica);
        }

        // POST: Musicas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMusica,Nome,Artista,Album,Duracao")] Musica musica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Album = new SelectList(db.Albums, "idAlbum", "Nome", musica.Album);
            ViewBag.Artista = new SelectList(db.Artistas, "idArtista", "Nome", musica.Artista);
            return View(musica);
        }

        // GET: Musicas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musica musica = db.Musicas.Find(id);
            if (musica == null)
            {
                return HttpNotFound();
            }
            return View(musica);
        }

        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musica musica = db.Musicas.Find(id);
            db.Musicas.Remove(musica);
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
