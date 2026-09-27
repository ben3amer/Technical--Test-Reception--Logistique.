using Microsoft.EntityFrameworkCore;
using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Infrastructure.Persistence
{
    public class ReceptionLogistiqueDbContext : DbContext
    {
        public ReceptionLogistiqueDbContext(DbContextOptions<ReceptionLogistiqueDbContext> options)
            : base(options) { }

        public DbSet<Delivery> Deliveries => Set<Delivery>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Pallet> Pallets => Set<Pallet>();
        public DbSet<Carton> Cartons => Set<Carton>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReceptionLogistiqueDbContext).Assembly);
        }
    }
}