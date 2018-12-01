using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public virtual ICollection<Emprestimo> Emprestimo { get; set; }
    }
}