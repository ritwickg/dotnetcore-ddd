using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Guid ProductCategoryId { get; set; }
        public Category Category { get; set; }
        public double MRP { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string ProductUnit { get; set; }
        public uint UnitQuantity { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ExpiresInDays { get; set; }
        public Guid CreatedBy { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductStock> ProductStocks { get; set; }
    }
}
