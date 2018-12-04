using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NomeUsuario { get; set; }
        public string Sobrenome { get; set; }
        public string RA { get; set; }
        public virtual ICollection<Emprestimo> Emprestimo { get; set; }
    }
}