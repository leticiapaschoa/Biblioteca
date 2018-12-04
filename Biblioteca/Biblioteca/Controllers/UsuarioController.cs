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
    public class UsuariosController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            List<Usuario> usuario = db.Usuario.ToList();

            return View(usuario);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NovoUsuario([Bind(Include = "Nome, Sobrenome, RA")]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public ActionResult EditarUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarUsuario([Bind(Include = "UsuarioID,Nome,Sobrenome,RA")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult ExcluirUsuario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("ExcluirUsuario")]
        public ActionResult ExcluirConfirma(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}