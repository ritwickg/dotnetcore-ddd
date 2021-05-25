using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Coupon
    {
        public Guid CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
