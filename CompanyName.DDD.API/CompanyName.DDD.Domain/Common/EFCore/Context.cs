using CompanyName.DDD.Domain.Common.EFCore.Configuration;
using CompanyName.DDD.Domain.TaxTypeAggregate.Domain;
using Microsoft.EntityFrameworkCore;
namespace CompanyName.DDD.Domain.Common.EFCore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<TaxType> TaxTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaxTypeConfiguration());
        }
    }
}