using CompanyName.DDD.Domain.Common.EFCore;
using CompanyName.DDD.Domain.SaleAggregate.Application.Interface;
using CompanyName.DDD.Domain.SaleAggregate.Domain;
namespace CompanyName.DDD.Domain.SaleAggregate.Infrastructure.SQLServer
{
    public class SaleRepository : ISaleRepository
    {
        private readonly Context _context;
        public SaleRepository() { }
        public SaleRepository(Context context)
        {
            _context = context;
        }
        public void Create(Sale sale)
        {
            _context.Sales.Add(sale);
        }
        public void SaveChanges() => _context.SaveChanges();
    }
}