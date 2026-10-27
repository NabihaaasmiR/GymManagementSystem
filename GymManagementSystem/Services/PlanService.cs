using System;
using System.Collections.Generic;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories;

namespace GymManagementSystem.Services
{
    public class PlanService
    {
        private readonly PlanRepository _planRepository;

        public PlanService()
        {
            _planRepository = new PlanRepository();
        }

        public List<MembershipPlan> GetAllPlans()
        {
            return _planRepository.GetAllPlans();
        }

        public void AddPlan(MembershipPlan plan)
        {
            _planRepository.SavePlan(plan);
        }

        public MembershipPlan? GetPlanById(string planId)
        {
            var plans = _planRepository.GetAllPlans();
            return plans.Find(p => p.PlanId.Equals(planId, StringComparison.OrdinalIgnoreCase));
        }
    }
}