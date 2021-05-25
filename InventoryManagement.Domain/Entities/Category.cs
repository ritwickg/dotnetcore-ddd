using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<CategoryHierarchy> CategoryHierarchies { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
