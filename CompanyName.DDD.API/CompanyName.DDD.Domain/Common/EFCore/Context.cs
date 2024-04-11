using CompanyName.DDD.Domain.Common.EFCore.Configuration;
using CompanyName.DDD.Domain.KardexAggregate.Domain;
using CompanyName.DDD.Domain.ProductStockAggregate.Domain;
using CompanyName.DDD.Domain.SaleAggregate.Domain;
using CompanyName.DDD.Domain.TaxTypeAggregate.Domain;
using Microsoft.EntityFrameworkCore;
namespace CompanyName.DDD.Domain.Common.EFCore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Kardex> Kardexs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaxTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new SaleDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductStockConfiguration());
            modelBuilder.ApplyConfiguration(new KardexConfiguration());
        }
    }
}