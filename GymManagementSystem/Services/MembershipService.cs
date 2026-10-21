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
    }
}