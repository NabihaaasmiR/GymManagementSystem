using System;
using System.Collections.Generic;
using GymManagementSystem.Models;

namespace GymManagementSystem.Services
{
    public class PlanService
    {
        private List<MembershipPlan> plans;
        public PlanService(List<MembershipPlan> planList)
        {
            plans = planList;
        }
        public List<MembershipPlan> GetAllPlans()
        {
            return plans;
        }
        public MembershipPlan GetPlanById(string id)
        {
            foreach (var p in plans)
            {
                if (p.PlanId.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return p;
            }
            return null;
        }

        public void AddPlan(MembershipPlan plan)
        {
            if (plan.DurationInDays <= 0)
                throw new Exception("Plan duration must be greater than zero.");

            if (plan.Fee < 0)
                throw new Exception("Plan fee cannot be negative.");

            foreach (var p in plans)
            {
                if (p.PlanId.Equals(plan.PlanId, StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Plan ID must be unique.");
            }

            plans.Add(plan);
        }

        public List<MembershipPlan> SearchPlans(string keyword)
        {
            List<MembershipPlan> result = new List<MembershipPlan>();
            foreach (var p in plans)
            {
                if (p.PlanName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    p.PlanId.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(p);
                }
            }
            return result;
        }

        public void UpdatePlan(string id, string name, int duration, decimal fee)
        {
            var p = GetPlanById(id);
            if (p == null)
                throw new Exception("Plan not found.");

            if (duration <= 0)
                throw new Exception("Duration must be greater than zero.");

            if (fee < 0)
                throw new Exception("Fee cannot be negative.");

            p.PlanName = name;
            p.DurationInDays = duration;
            p.Fee = fee;
        }

        public void DeletePlan(string id)
        {
            var p = GetPlanById(id);
            if (p == null)
                throw new Exception("Plan not found.");

            plans.Remove(p);
        }
    }
}