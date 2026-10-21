namespace GymManagementSystem.Models
{
    public class MembershipPlan
    {
        public string PlanId { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Fee { get; set; }
    }
}