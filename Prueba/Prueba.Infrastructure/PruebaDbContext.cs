using Prueba.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure
{
    public class PruebaDbContext : DbContext
    {
        public PruebaDbContext() : base("DefaultConnection") { }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<CompraDetalle> CompraDetalle { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<FacturaDetalle> FacturaDetalle {get;set;}
        public virtual DbSet<Kardex> Kardex { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
