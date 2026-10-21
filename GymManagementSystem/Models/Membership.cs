using System;

namespace GymManagementSystem.Models
{
    public class Membership
    {
        public string MembershipId { get; set; } = string.Empty;
        public string MemberId { get; set; } = string.Empty;
        public string PlanId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Active";
        public decimal AmountPaid { get; set; }
        public decimal BalancePaid { get; set; }
        public int Balance { get; set; }

    }
}