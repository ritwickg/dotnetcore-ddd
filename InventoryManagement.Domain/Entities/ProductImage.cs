using System;

namespace InventoryManagement.Domain.Entities
{
    public class ProductImage
    {
        public Guid ImageId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
    }
}
