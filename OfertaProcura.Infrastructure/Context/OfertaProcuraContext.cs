using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OfertaProcura.Context
{
    public class OfertaProcuraContext : DbContext
    {
        private IConfiguration _config;

        public OfertaProcuraContext(DbContextOptions<OfertaProcuraContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(GetType()));
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("oferta_procura"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Contratacao> Contratacao { get; set; }
        public DbSet<ImagemPortifolio> ImagemPortifolio { get; set; }
        public DbSet<Portifolio> Portifolio { get; set; }
        public DbSet<Profissao> Profissao { get; set; }
        public DbSet<Profissional> Profissional { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }


}
