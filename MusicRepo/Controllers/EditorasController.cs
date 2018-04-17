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
    public class EditorasController : Controller
    {
        private visualStudioDBEntities db = new visualStudioDBEntities();

        // GET: Editoras
        public ActionResult Index(String sortOrder, String searchString)
        {
            ViewBag.NomeSortParm = sortOrder == "Nome" ? "nome_desc" : "Nome";
            ViewBag.FundacaoSortParm = sortOrder == "Fundacao" ? "fundacao_desc" : "Fundacao";
            ViewBag.LocalidadeSortParm = sortOrder == "Localidade" ? "localidade_desc" : "Localidade";
            var editoras = from s in db.Editoras
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                editoras = editoras.Where(s => s.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Nome":
                    editoras = editoras.OrderBy(s => s.Nome);
                    break;
                case "nome_desc":
                    editoras = editoras.OrderByDescending(s => s.Nome);
                    break;
                case "Fundacao":
                    editoras = editoras.OrderBy(s => s.Fundacao);
                    break;
                case "fundacao_desc":
                    editoras = editoras.OrderByDescending(s => s.Fundacao);
                    break;
                case "Localidade":
                    editoras = editoras.OrderBy(s => s.Localidade);
                    break;
                case "localidade_desc":
                    editoras = editoras.OrderByDescending(s => s.Localidade);
                    break;
                default:
                    editoras = editoras.OrderBy(s => s.Nome);
                    break;
            }
            return View(editoras.ToList());
        }

        // GET: Editoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editora editora = db.Editoras.Find(id);
            if (editora == null)
            {
                return HttpNotFound();
            }
            return View(editora);
        }

        // GET: Editoras/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoras/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEditora,Nome,Fundacao,Localidade")] Editora editora)
        {
            if (ModelState.IsValid)
            {
                db.Editoras.Add(editora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editora);
        }

        // GET: Editoras/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editora editora = db.Editoras.Find(id);
            if (editora == null)
            {
                return HttpNotFound();
            }
            return View(editora);
        }

        // POST: Editoras/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEditora,Nome,Fundacao,Localidade")] Editora editora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editora);
        }

        // GET: Editoras/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editora editora = db.Editoras.Find(id);
            if (editora == null)
            {
                return HttpNotFound();
            }
            return View(editora);
        }

        // POST: Editoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Editora editora = db.Editoras.Find(id);
            db.Editoras.Remove(editora);
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
