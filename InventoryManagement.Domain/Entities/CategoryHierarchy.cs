using System;

namespace InventoryManagement.Domain.Entities
{
    public class CategoryHierarchy
    {
        public Guid CategoryHierarchyId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Hierarchy { get; set; }
    }
}
