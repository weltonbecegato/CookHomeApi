using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class CookHomeContext : DbContext
    {
        public CookHomeContext()
        {

        }

        public CookHomeContext(DbContextOptions<CookHomeContext> options) : base(options)
        {
        }

        public virtual DbSet<Agendamento> Agendamento { get; set; }
        public virtual DbSet<Avaliacao> Avaliacao { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Cozinheiro> Cozinheiro { get; set; }
        public virtual DbSet<CozinheiroCulinaria> CozinheiroCulinaria { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Pagamento> Pagamento { get; set; }
        public virtual DbSet<TipoCulinaria> TipoCulinaria { get; set; }
    }
}
