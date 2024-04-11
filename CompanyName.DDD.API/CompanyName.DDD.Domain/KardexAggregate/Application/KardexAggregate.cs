using CompanyName.DDD.Domain.Common.Log;
using CompanyName.DDD.Domain.KardexAggregate.Application.DTO;
using CompanyName.DDD.Domain.KardexAggregate.Application.Interface;
using CompanyName.DDD.Domain.KardexAggregate.Domain;
namespace CompanyName.DDD.Domain.KardexAggregate.Application
{
    public class KardexAggregate : IKardexAggregate
    {
        private readonly IKardexRepository _repository;
        private readonly ILog _log;
        private readonly string _environment;
        public KardexAggregate(IKardexRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
        }
        public void Create(KardexCommand command, Guid processId)
        {
            _log.Send(new BodyLog(processId, _environment, LogLevelStruct.Info, "Inicio de movimiento de kardex", command));
            var symbol = command.quantity < 0 ? "-" : "+";
            var kardex = new Kardex(command.productCode, command.initialStock, command.quantity, command.finalStock, symbol);
            _log.Send(new BodyLog(processId, _environment, LogLevelStruct.Info, "Registado nuevo movimiento de kardex", kardex));
            _repository.Create(kardex);
        }
    }
}