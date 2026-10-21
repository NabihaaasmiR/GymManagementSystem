using System;
using System.Collections.Generic;
using GymManagementSystem.Models;
using GymManagementSystem.Services;

namespace GymManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Member> memberList = new List<Member>();
            MemberService memberService = new MemberService(memberList);

            List<MembershipPlan> planList = new List<MembershipPlan>();
            PlanService planService = new PlanService(planList);

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Gym Management System");
                Console.WriteLine("1. Register Member");
                Console.WriteLine("2. View All Members");
                Console.WriteLine("3. Search Members");
                Console.WriteLine("4. Update Member Contact");
                Console.WriteLine("5. Delete Member");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("6. Add Membership Plan");
                Console.WriteLine("7. View All Plans");
                Console.WriteLine("8. Search Plans");
                Console.WriteLine("9. Update Plan");
                Console.WriteLine("10. Delete Plan");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("11. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    try
                    {
                        Member m = new Member();

                        Console.Write("Enter Member ID: ");
                        m.MemberId = Console.ReadLine()!;

                        Console.Write("Enter Full Name: ");
                        m.FullName = Console.ReadLine()!;

                        Console.Write("Enter Age (16-100): ");
                        if (int.TryParse(Console.ReadLine(), out int age))
                        {
                            m.Age = age;
                        }

                        Console.Write("Enter Phone Number: ");
                        m.PhoneNumber = Console.ReadLine()!;
                        m.JoinDate = DateTime.Today;

                        memberService.RegisterMember(m);
                        Console.WriteLine("Member registered successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("All Members");
                    foreach (var m in memberService.GetAllMembers())
                    {
                        Console.WriteLine($"ID: {m.MemberId}  Name: {m.FullName}  Age: {m.Age}  Phone: {m.PhoneNumber}  Joined: {m.JoinDate}");
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Enter search keyword (Name or Phone): ");
                    string keyword = Console.ReadLine()!;
                    Console.WriteLine("Search Results");
                    foreach (var m in memberService.SearchMembers(keyword))
                    {
                        Console.WriteLine($"ID: {m.MemberId}  Name: {m.FullName}  Phone: {m.PhoneNumber}");
                    }
                }
                else if (choice == "4")
                {
                    try
                    {
                        Console.Write("Enter Member ID to update: ");
                        string id = Console.ReadLine()!;
                        Console.Write("Enter new Phone Number: ");
                        string phone = Console.ReadLine()!;

                        memberService.UpdateMemberContact(id, phone);
                        Console.WriteLine("Member contact updated successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "5")
                {
                    try
                    {
                        Console.Write("Enter Member ID to delete: ");
                        string id = Console.ReadLine() ?? string.Empty;

                        memberService.DeleteMember(id);
                        Console.WriteLine("Member deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "6")
                {
                    try
                    {
                        MembershipPlan p = new MembershipPlan();

                        Console.Write("Enter Plan ID: ");
                        p.PlanId = Console.ReadLine()!;

                        Console.Write("Enter Plan Name: ");
                        p.PlanName = Console.ReadLine()!;

                        Console.Write("Enter Duration in Days: ");
                        if (int.TryParse(Console.ReadLine(), out int duration))
                        {
                            p.DurationInDays = duration;
                        }

                        Console.Write("Enter Fee: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal fee))
                        {
                            p.Fee = fee;
                        }

                        planService.AddPlan(p);
                        Console.WriteLine("Membership plan added successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "7")
                {
                    Console.Clear();
                    Console.WriteLine("All Membership Plans");
                    foreach (var p in planService.GetAllPlans())
                    {
                        Console.WriteLine($"ID: {p.PlanId}  Name: {p.PlanName}  Duration: {p.DurationInDays} days  Fee: {p.Fee:C}");
                    }
                }
                else if (choice == "8")
                {
                    Console.Write("Enter search keyword (Plan ID or Name): ");
                    string keyword = Console.ReadLine()!;
                    Console.WriteLine("Plan Search Results");
                    foreach (var p in planService.SearchPlans(keyword))
                    {
                        Console.WriteLine($"ID: {p.PlanId}  Name: {p.PlanName}  Duration: {p.DurationInDays} days  Fee: {p.Fee:C}");
                    }
                }
                else if (choice == "9")
                {
                    try
                    {
                        Console.Write("Enter Plan ID to update: ");
                        string id = Console.ReadLine()!;

                        Console.Write("Enter New Plan Name: ");
                        string name = Console.ReadLine()!;

                        Console.Write("Enter New Duration in Days: ");
                        int duration = 0;
                        int.TryParse(Console.ReadLine(), out duration);

                        Console.Write("Enter New Fee: ");
                        decimal fee = 0;
                        decimal.TryParse(Console.ReadLine(), out fee);

                        planService.UpdatePlan(id, name, duration, fee);
                        Console.WriteLine("Plan updated successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "10")
                {
                    try
                    {
                        Console.Write("Enter Plan ID to delete: ");
                        string id = Console.ReadLine()!;

                        planService.DeletePlan(id);
                        Console.WriteLine("Plan deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "11")
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
                if (running)
                {
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }  
        }
    }
}