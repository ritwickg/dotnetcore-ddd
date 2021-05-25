using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
        public DateTime TransactionDate { get; set; }
        public double GrossAmount { get; set; }
        public double BillTotal { get; set; }
        public int ModeOfPayment { get; set; }
        public Guid CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public int LoyaltyPointsUsed { get; set; }
        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
        public ReferralCodeTransaction ReferralCodeTransaction { get; set; }
    }
}
