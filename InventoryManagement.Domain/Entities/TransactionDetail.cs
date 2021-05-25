using System;

namespace InventoryManagement.Domain.Entities
{
    public class TransactionDetail
    {
        public Guid TransactionDetailId { get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public Guid StockId { get; set; }
        public ProductStock ProductStock { get; set; }
        public double UnitPrice { get; set; }
        public long Units { get; set; }
        public double DiscountPercentage { get; set; }
        public double GrossAmount { get; set; }
    }
}
