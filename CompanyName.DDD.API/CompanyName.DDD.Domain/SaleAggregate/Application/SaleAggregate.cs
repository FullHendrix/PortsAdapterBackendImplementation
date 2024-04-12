using CompanyName.DDD.Domain.Common.Log;
using CompanyName.DDD.Domain.Common.Struct;
using CompanyName.DDD.Domain.ProductStockAggregate.Application.DTO;
using CompanyName.DDD.Domain.ProductStockAggregate.Application.Interface;
using CompanyName.DDD.Domain.SaleAggregate.Application.DTO;
using CompanyName.DDD.Domain.SaleAggregate.Application.Interface;
using CompanyName.DDD.Domain.SaleAggregate.Domain;
using System.ComponentModel;
namespace CompanyName.DDD.Domain.SaleAggregate.Application
{
    public class SaleAggregate : ISaleAggregate
    {
        private readonly ISaleRepository _repository;
        private readonly IProductStockAggregate _productStock;
        private readonly ILog _log;
        private readonly Guid _processId;
        private readonly string _environment;
        public SaleAggregate(ISaleRepository repository, IProductStockAggregate productStock, ILog log)
        {
            _repository = repository;
            _productStock = productStock;
            _processId = Guid.NewGuid();
            _log = log;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
        }
        public void Create(SaleCommand command)
        {
            _log.Send(new BodyLog(_processId, _environment, LogLevelStruct.Info, "Inicio de venta", command));
            var sale = new Sale(command.subtotal, command.taxAmount, command.total);
            command.details.ForEach(detail =>
            {
                var newDetail = sale.AddDetail(detail.productCode, detail.igvPercent, detail.price, detail.quantity, detail.subtotal, detail.taxAmount, detail.total);
                if (!newDetail.ValidateTotal(detail.igvPercent))
                {
                    var message = $"El total del producto con codigo {detail.productCode} no es correcto";
                    _log.Send(new BodyLog(_processId, _environment, LogLevelStruct.Error, message, detail));
                    throw new WarningException(message);
                }
                _productStock.CreateOrUpdate(new ProductStockCommand(detail.productCode, detail.quantity), _processId);
                _log.Send(new BodyLog(_processId, _environment, LogLevelStruct.Info, "Nuevo detalle", detail));
            });
            if (!sale.ValidateTotal())
            {
                var message = "El total de la venta no coincide con el total de los detaller";
                _log.Send(new BodyLog(_processId, _environment, LogLevelStruct.Error, message, command));
                throw new WarningException(message);
            }
            _repository.Create(sale);
            _repository.SaveChanges();
        }
    }
}