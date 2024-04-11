namespace CompanyName.DDD.Domain.SaleAggregate.Domain
{
    public class Sale
    {
        public Guid SaleCode { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal Total { get; private set; }
        public List<SaleDetail> SaleDetails { get; private set; }
        public Sale() { }
        public Sale(decimal subTotal, decimal taxAmount, decimal total)
        {
            SaleCode = Guid.NewGuid();
            SaleDate = DateTime.Now;
            Subtotal = subTotal;
            TaxAmount = taxAmount;
            Total = total;
            SaleDetails = [];
        }
        public SaleDetail AddDetail(Guid productCode, decimal igvPercent, decimal price, int quantity, decimal subtotal, decimal taxAmount, decimal total)
        {
            var saleDetail = new SaleDetail(SaleCode, productCode, price, quantity, subtotal, taxAmount, total);
            SaleDetails.Add(saleDetail);
            return saleDetail;
        }
        public bool ValidateTotal()
        {
            var itemsTotal = SaleDetails.Sum(x => x.Total);
            return itemsTotal == Total;
        }
    }
}