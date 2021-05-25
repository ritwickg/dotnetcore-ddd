using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Currency
    {
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
