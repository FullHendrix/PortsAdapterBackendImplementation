using CompanyName.DDD.Domain.Common.EFCore;
using CompanyName.DDD.Domain.KardexAggregate.Application.Interface;
using CompanyName.DDD.Domain.KardexAggregate.Domain;
namespace CompanyName.DDD.Domain.KardexAggregate.Infrastructure.SQLServer
{
    public class KardexRepository : IKardexRepository
    {
        private readonly Context _context;
        public KardexRepository(){}
        public KardexRepository(Context context)
        {
            _context = context;
        }
        public void Create(Kardex command)
        {
            _context.Kardexs.Add(command);
        }
    }
}