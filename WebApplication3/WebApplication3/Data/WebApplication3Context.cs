using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class WebApplication3Context : DbContext
    {
        public WebApplication3Context (DbContextOptions<WebApplication3Context> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Comentario> Comentario { get; set; }

        public DbSet<Mensagem> Mensagem { get; set; }

    }
}
