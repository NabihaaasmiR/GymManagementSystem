using System;
using System.Collections.Generic;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories;

namespace GymManagementSystem.Services
{
    public class MemberService
    {
        private readonly MemberRepository _memberRepository;

        public MemberService()
        {
            _memberRepository = new MemberRepository();
        }

        public List<Member> GetAllMembers()
        {
            return _memberRepository.GetAllMembers();
        }

        public void AddMember(Member member)
        {
            _memberRepository.SaveMember(member);
        }

        public Member? GetMemberById(string memberId)
        {
            var members = _memberRepository.GetAllMembers();
            return members.Find(m => m.MemberId.Equals(memberId, StringComparison.OrdinalIgnoreCase));
        }
    }
}