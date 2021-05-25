using System;

namespace InventoryManagement.Domain.Entities
{
    public class ReferralCodeTransaction
    {
        public Guid ReferralTransactionId { get; set; }
        public Guid ReferralId { get; set; }
        public ReferralCode ReferralCode { get; set; }
        public Guid ReferredTo { get; set; }
        public User User { get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
