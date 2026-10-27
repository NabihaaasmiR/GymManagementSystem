using System;
using GymManagementSystem.Models;
using GymManagementSystem.Services;

namespace GymManagementSystem
{
    class Program
    {
        private static readonly MemberService _memberService = new MemberService();
        private static readonly PlanService _planService = new PlanService();
        private static readonly MembershipService _membershipService = new MembershipService();

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("       GYM MANAGEMENT SYSTEM (SQL)      ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Manage Members");
                Console.WriteLine("2. Manage Membership Plans");
                Console.WriteLine("3. Manage Memberships");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option (1-4): ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageMembersMenu();
                        break;
                    case "2":
                        ManagePlansMenu();
                        break;
                    case "3":
                        ManageMembershipsMenu();
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("\nThank you for using Gym Management System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ManageMembersMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- MANAGE MEMBERS ---");
                Console.WriteLine("1. View All Members");
                Console.WriteLine("2. Add New Member");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("--- MEMBER LIST ---");
                    var members = _memberService.GetAllMembers();
                    if (members.Count == 0)
                    {
                        Console.WriteLine("No members found in database.");
                    }
                    else
                    {
                        foreach (var m in members)
                        {
                            Console.WriteLine($"ID: {m.MemberId} | Name: {m.FullName} | Age: {m.Age} | Phone: {m.PhoneNumber} | Joined: {m.JoinDate.ToShortDateString()}");
                        }
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("--- ADD NEW MEMBER ---");

                    Console.Write("Enter Member ID: ");
                    string id = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter Full Name: ");
                    string name = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter Age: ");
                    int age = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Phone Number: ");
                    string phone = Console.ReadLine() ?? string.Empty;

                    Member newMember = new Member
                    {
                        MemberId = id,
                        FullName = name,
                        Age = age,
                        PhoneNumber = phone,
                        JoinDate = DateTime.Now
                    };

                    _memberService.AddMember(newMember);
                    Console.WriteLine("\nMember successfully added and saved to SQL Server!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    back = true;
                }
            }
        }

        private static void ManagePlansMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- MANAGE MEMBERSHIP PLANS ---");
                Console.WriteLine("1. View All Plans");
                Console.WriteLine("2. Add New Plan");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("--- PLANS LIST ---");
                    var plans = _planService.GetAllPlans();
                    if (plans.Count == 0)
                    {
                        Console.WriteLine("No plans found in database.");
                    }
                    else
                    {
                        foreach (var p in plans)
                        {
                            Console.WriteLine($"ID: {p.PlanId} | Name: {p.PlanName} | Duration: {p.DurationInDays} days | Fee: ${p.Fee}");
                        }
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("--- ADD NEW PLAN ---");

                    Console.Write("Enter Plan ID: ");
                    string id = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter Plan Name: ");
                    string name = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter Duration in Days: ");
                    int duration = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Fee Amount: ");
                    decimal fee = Convert.ToDecimal(Console.ReadLine());

                    MembershipPlan newPlan = new MembershipPlan
                    {
                        PlanId = id,
                        PlanName = name,
                        DurationInDays = duration,
                        Fee = fee
                    };

                    _planService.AddPlan(newPlan);
                    Console.WriteLine("\nPlan successfully added and saved to SQL Server!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    back = true;
                }
            }
        }

        private static void ManageMembershipsMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- MANAGE MEMBERSHIPS ---");
                Console.WriteLine("1. View All Memberships");
                Console.WriteLine("2. Assign Membership to Member");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("-- MEMBERSHIPS LIST ---");
                    var memberships = _membershipService.GetAllMemberships();
                    if (memberships.Count == 0)
                    {
                        Console.WriteLine("No memberships found in database.");
                    }
                    else
                    {
                        foreach (var ms in memberships)
                        {
                            Console.WriteLine($"ID: {ms.MembershipId} | Member ID: {ms.MemberId} | Plan ID: {ms.PlanId} | Status: {ms.Status} | Paid: ${ms.AmountPaid}");
                        }
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("--- ASSIGN MEMBERSHIP ---");

                    Console.Write("Enter Membership ID: ");
                    string msId = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter Member ID: ");
                    string mId = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter Plan ID: ");
                    string pId = Console.ReadLine() ?? string.Empty;

                    Console.Write("Enter Amount Paid: ");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());

                    Membership newMs = new Membership
                    {
                        MembershipId = msId,
                        MemberId = mId,
                        PlanId = pId,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(30), // Defaulting to 30 days or based on plan
                        Status = "Active",
                        AmountPaid = amount
                    };

                    _membershipService.AddMembership(newMs);
                    Console.WriteLine("\nMembership successfully assigned and saved to SQL Server!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    back = true;
                }
            }
        }
    }
}