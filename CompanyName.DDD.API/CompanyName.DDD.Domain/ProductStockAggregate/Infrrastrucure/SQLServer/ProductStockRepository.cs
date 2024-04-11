using CompanyName.DDD.Domain.Common.EFCore;
using CompanyName.DDD.Domain.ProductStockAggregate.Application.Interface;
using CompanyName.DDD.Domain.ProductStockAggregate.Domain;
namespace CompanyName.DDD.Domain.ProductStockAggregate.Infrrastrucure.SQLServer
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly Context _context;
        public ProductStockRepository(){}
        public ProductStockRepository(Context context)
        {
            _context = context;
        }
        public void Create(ProductStock productStock)
        {
            _context.ProductStocks.Add(productStock);
        }        
        public void Update(ProductStock productStock)
        {
            _context.ProductStocks.Update(productStock);
        }        
        public ProductStock? Find(Guid productCode)
        {
            var row = _context.ProductStocks.Where(x => x.ProductCode.Equals(productCode)).SingleOrDefault();
            return row;
        }
        public void SaveChanges() => _context.SaveChanges();
    }
}