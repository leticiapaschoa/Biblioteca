using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Data
{
    public class Context : DbContext
    {

        public Context()
            : base("Biblioteca")
        {

        }

        public DbSet<Livro> Livro { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}