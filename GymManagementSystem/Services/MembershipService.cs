using System;
using System.Collections.Generic;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories;

namespace GymManagementSystem.Services
{
    public class MembershipService
    {
        private readonly MembershipRepository _membershipRepository;

        public MembershipService()
        {
            _membershipRepository = new MembershipRepository();
        }

        public List<Membership> GetAllMemberships()
        {
            return _membershipRepository.GetAllMemberships();
        }

        public void AddMembership(Membership membership)
        {
            _membershipRepository.SaveMembership(membership);
        }

        public Membership? GetMembershipById(string membershipId)
        {
            var memberships = _membershipRepository.GetAllMemberships();
            return memberships.Find(ms => ms.MembershipId.Equals(membershipId, StringComparison.OrdinalIgnoreCase));
        }
    }
}