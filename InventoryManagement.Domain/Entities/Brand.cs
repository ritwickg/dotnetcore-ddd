using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Brand
    {
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
