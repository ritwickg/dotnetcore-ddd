using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class User: IdentityUser<Guid>
    {
        public string Name { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedDate { get; set;}
        public Guid MembershipId { get; set; }
        public Membership Membership { get; set; }
        public int LoyaltyPoints { get; set; }
        public ICollection<Transaction> CustomerTransactions { get; set; }
        public ICollection<Transaction> CreatedByTransactions { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ReferralCode> ReferralCodes { get; set; }
        public ICollection<ReferralCodeTransaction> ReferralCodeTransactions { get; set; }
    }
    public class UserRole: IdentityRole<Guid>
    {

    }
}
