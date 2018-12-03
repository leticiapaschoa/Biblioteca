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
    public class EmprestimoController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            List<Emprestimo> emprestimo = db.Emprestimo.ToList();

            return View(emprestimo);
        }

        public ActionResult DadosEmprestimo(int id)
        {
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        public ActionResult NovoEmprestimo(int idLivro)
        {
            Emprestimo emprestimo = new Emprestimo();
            emprestimo.Livro = db.Livro.Find(idLivro);
            emprestimo.DataEmprestimo = DateTime.Today;
            emprestimo.DataProgramadaDevolucao = DateTime.Today.AddDays(15);
            emprestimo.Multa = 0.00M;
            emprestimo.LivroID = idLivro;

            return View(emprestimo);
        }

        [HttpPost]
        public ActionResult NovoEmprestimo([Bind(Include = "EmprestimoID,Usuario,DataEmprestimo,DataProgramadaDevolucao,Multa,LivroId")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                emprestimo.DataDevolucao = null;
                db.Emprestimo.Add(emprestimo);
                db.SaveChanges();

                var livroController = new LivroController();
                livroController.AlterarDisponililidadeLivro(emprestimo.LivroID);

                return RedirectToAction("Index");
            }

            return View(emprestimo);
        }

        [HttpPost, ActionName("Devolver")]
        public ActionResult DevolverEmprestimo(int id)
        {
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            emprestimo.DataDevolucao = DateTime.Today;
            db.Entry(emprestimo).State = EntityState.Modified;
            db.SaveChanges();


            var livroController = new LivroController();
            livroController.AlterarDisponililidadeLivro(emprestimo.LivroID);

            return RedirectToAction("Index");
        }

        public ActionResult RelatorioEmprestimo(string BuscaId, string BuscaValor)
        {
            
            List<Emprestimo> emprestimo = new List<Emprestimo>();

            if (BuscaId == string.Empty)
            {
                emprestimo = db.Emprestimo.ToList();
            }
            else
            {
                if (BuscaId == "Livro")
                {
                    emprestimo = db.Emprestimo.Where(l => l.Livro.Titulo.Equals(BuscaValor)).ToList();
                }
                else if (BuscaId == "Usuario")
                {
                    emprestimo = db.Emprestimo.Where(l => l.Usuario.NomeUsuario.Equals(BuscaValor)).ToList();
                }
            }


            return View(emprestimo);
            
        }      
       
        private decimal CalcularMulta(DateTime dataProgramadaDevolucao)
        {
            decimal multa = 0.00M;

            if (DateTime.Today > dataProgramadaDevolucao)
            {
                int nrDias = DateTime.Today.Subtract(dataProgramadaDevolucao).Days;
                multa = nrDias * 3.50M;
            }

            return multa;
        }      
    }
}