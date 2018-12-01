using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
        public int EmprestimoID { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataProgramadaDevolucao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public decimal Multa { get; set; }
        public int LivroID { get; set; }
        public virtual Livro Livro { get; set; }
    }
}