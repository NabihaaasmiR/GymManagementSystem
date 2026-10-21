using System;

namespace GymManagementSystem.Models
{
    public class Member
    {
        public string MemberId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
    }
}