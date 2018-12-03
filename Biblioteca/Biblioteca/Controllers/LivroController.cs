using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Biblioteca.Data;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            List<Livro> livro = db.Livro.ToList();

            return View(livro);
        }

        public void AlterarDisponililidadeLivro(int id)
        {
            Livro livro = db.Livro.Find(id);
            livro.Disponibilidade = !livro.Disponibilidade;
            db.Entry(livro).State = EntityState.Modified;
            db.SaveChanges();
        }

        public ActionResult NovoLivro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NovoLivro([Bind(Include = "ID,Titulo,Editora,Autor,Disponibilidade")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livro.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(livro);
        }

        public ActionResult EditarLivro(int id)
        {
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        [HttpPost]
        public ActionResult EditarLivro([Bind(Include = "LivroID,Titulo,Editora,Autor")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        public ActionResult ExcluirLivro(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        [HttpPost, ActionName("ExcluirLivro")]
        public ActionResult ExcluirConfirma(int id)
        {
            Livro livro = db.Livro.Find(id);
            db.Livro.Remove(livro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DetalhesLivro(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

    }

}