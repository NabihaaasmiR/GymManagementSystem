using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GymManagementSystem.Models;

namespace GymManagementSystem.Services
{
    public class StorageService
    {
        private const string MemberFile = "members.json";
        private const string PlanFile = "plans.json";
        private const string MembershipFile = "memberships.json";

        public List<Member> LoadMembers()
        {
            if (!File.Exists(MemberFile))
            {
                return new List<Member>();
            }

            string json = File.ReadAllText(MemberFile);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Member>();
            }

            return JsonSerializer.Deserialize<List<Member>>(json) ?? new List<Member>();
        }

        public void SaveMembers(List<Member> members)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(members, options);
            File.WriteAllText(MemberFile, json);
        }

        public List<MembershipPlan> LoadPlans()
        {
            if (!File.Exists(PlanFile))
            {
                return new List<MembershipPlan>();
            }

            string json = File.ReadAllText(PlanFile);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<MembershipPlan>();
            }

            return JsonSerializer.Deserialize<List<MembershipPlan>>(json) ?? new List<MembershipPlan>();
        }

        public void SavePlans(List<MembershipPlan> plans)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(plans, options);
            File.WriteAllText(PlanFile, json);
        }

        public List<Membership> LoadMemberships()
        {
            if (!File.Exists(MembershipFile))
            {
                return new List<Membership>();
            }

            string json = File.ReadAllText(MembershipFile);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Membership>();
            }

            return JsonSerializer.Deserialize<List<Membership>>(json) ?? new List<Membership>();
        }

        public void SaveMemberships(List<Membership> memberships)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(memberships, options);
            File.WriteAllText(MembershipFile, json);
        }
    }
}