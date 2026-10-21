using System;
using System.Collections.Generic;
using GymManagementSystem.Models;

namespace GymManagementSystem.Services
{
    public class MembershipService
    {
        private List<Membership> memberships;

        public MembershipService(List<Membership> membershipList)
        {
            memberships = membershipList;
        }

        public List<Membership> GetAllMemberships()
        {
            return memberships;
        }

        public Membership GetMembershipById(string id)
        {
            foreach (var ms in memberships)
            {
                if (ms.MembershipId.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return ms;
            }
            return null;
        }

        public void AssignMembership(Membership membership, Member member, MembershipPlan plan)
        {
            if (member == null)
                throw new Exception("Member does not exist.");

            if (plan == null)
                throw new Exception("Membership plan does not exist.");

            foreach (var ms in memberships)
            {
                if (ms.MembershipId.Equals(membership.MembershipId, StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Membership ID must be unique.");
            }

            membership.StartDate = DateTime.Today;
            membership.EndDate = membership.StartDate.AddDays(plan.DurationInDays);
            membership.AmountPaid = plan.Fee;
            membership.Status = "Active";

            memberships.Add(membership);
        }

        public void CancelMembership(string id)
        {
            var ms = GetMembershipById(id);
            if (ms == null)
                throw new Exception("Membership not found.");

            ms.Status = "Cancelled";
        }
    }
}