
using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Membership
    {
        public Guid MembershipId { get; set; }
        public string MembershipName { get; set; }
        public double ConversionRate { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
