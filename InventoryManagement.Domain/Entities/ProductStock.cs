using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class ProductStock
    {
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string BatchNumber { get; set; }
        public long Quantity { get; set; }
        public double UnitPrice { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
