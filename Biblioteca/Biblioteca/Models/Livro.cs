using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Livro
    {
        public int LivroID { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Autor { get; set; }
        public bool Disponibilidade { get; set; }

        public virtual ICollection<Emprestimo> Emprestimo { get; set; }
    }
}