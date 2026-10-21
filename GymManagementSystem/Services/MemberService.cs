using System;
using System.Collections.Generic;
using GymManagementSystem.Models;

namespace GymManagementSystem.Services
{
    public class MemberService
    {
        private List<Member> members;
        public MemberService(List<Member> memberList)
        {
            members = memberList;
        }
        public List<Member> GetAllMembers()
        {
            return members;
        }
        public Member GetMemberById(string id)
        {
            foreach (var m in members)
            {
                if (m.MemberId.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return m;
            }
            return null;
        }

        public void RegisterMember(Member member)
        {
            if (member.Age < 16 || member.Age > 100)
                throw new Exception("Member age must be between 16 and 100.");

            foreach (var m in members)
            {
                if (m.MemberId.Equals(member.MemberId, StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Member ID must be unique.");
            }

            members.Add(member);
        }
        public List<Member> SearchMembers(string keyword)
        {
            List<Member> result = new List<Member>();
            foreach (var m in members)
            {
                if (m.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    m.PhoneNumber.Contains(keyword))
                {
                    result.Add(m);
                }
            }
            return result;
        }

        public void UpdateMemberContact(string id, string phone)
        {
            var m = GetMemberById(id);
            if (m == null)
                throw new Exception("Member not found.");

            m.PhoneNumber = phone;
        }
        public void DeleteMember(string id)
        {
            var m = GetMemberById(id);
            if (m == null)
                throw new Exception("Member not found.");

            members.Remove(m);
        }
    }
}