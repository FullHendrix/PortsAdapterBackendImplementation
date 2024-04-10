using CompanyName.DDD.Domain.TaxTypeAggregate.Application.DTO;
using CompanyName.DDD.Domain.TaxTypeAggregate.Application.Interface;
namespace CompanyName.DDD.Domain.TaxTypeAggregate.Application
{
    public class TaxTypeAggregate : ITaxTypeAggregate
    {
        private readonly ITaxTypeRepository _repository;
        public TaxTypeAggregate(ITaxTypeRepository repository)
        {
            _repository = repository;
        }
        public List<TaxTypeSimpleResponse> GetSimpleResponse()
        {
            var rows = _repository.GetSimpleResponse();
            return rows;
        }
    }
}