using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class ReferralCode
    {
        public Guid ReferralCodeId { get; set; }
        public string Code { get; set; }
        public Guid ReferredById { get; set; }
        public User User { get; set; }
        public int MaxReferrals { get; set; }
        public ICollection<ReferralCodeTransaction> ReferralCodeTransactions { get; set; }
    }
}
