namespace CompanyName.DDD.Domain.SaleAggregate.Application.DTO
{
    public record SaleCommand(decimal subtotal, decimal taxAmount, decimal total, List<SaleDetailCommand> details);
    public record SaleDetailCommand(Guid productCode, decimal price, decimal igvPercent, int quantity, decimal subtotal, decimal taxAmount, decimal total);
}