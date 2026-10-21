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
            StorageService storageService = new StorageService();

            List<Member> memberList = storageService.LoadMembers();
            MemberService memberService = new MemberService(memberList);

            List<MembershipPlan> planList = storageService.LoadPlans();
            PlanService planService = new PlanService(planList);

            List<Membership> membershipList = storageService.LoadMemberships();
            MembershipService membershipService = new MembershipService(membershipList);

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine("    GYM MANAGEMENT SYSTEM (JSON)     ");
                Console.WriteLine("======================================");
                Console.WriteLine("--- Member Operations ---");
                Console.WriteLine("1.  Register Member");
                Console.WriteLine("2.  View All Members");
                Console.WriteLine("3.  Search Members");
                Console.WriteLine("4.  Update Member Contact");
                Console.WriteLine("5.  Delete Member");
                Console.WriteLine("--- Plan Operations ---");
                Console.WriteLine("6.  Add Membership Plan");
                Console.WriteLine("7.  View All Plans");
                Console.WriteLine("8.  Search Plans");
                Console.WriteLine("9.  Update Plan");
                Console.WriteLine("10. Delete Plan");
                Console.WriteLine("--- Subscription Operations ---");
                Console.WriteLine("11. Assign Membership");
                Console.WriteLine("12. View All Memberships");
                Console.WriteLine("13. Cancel Membership");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("14. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    try
                    {
                        Member m = new Member();

                        Console.Write("Enter Member ID: ");
                        m.MemberId = Console.ReadLine() ?? string.Empty;

                        Console.Write("Enter Full Name: ");
                        m.FullName = Console.ReadLine() ?? string.Empty;

                        Console.Write("Enter Age (16-100): ");
                        if (int.TryParse(Console.ReadLine(), out int age))
                        {
                            m.Age = age;
                        }

                        Console.Write("Enter Phone Number: ");
                        m.PhoneNumber = Console.ReadLine() ?? string.Empty;
                        m.JoinDate = DateTime.Today;

                        memberService.RegisterMember(m);
                        storageService.SaveMembers(memberList);
                        Console.WriteLine("Member registered and saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("--- All Members ---");
                    foreach (var m in memberService.GetAllMembers())
                    {
                        Console.WriteLine($"ID: {m.MemberId} | Name: {m.FullName} | Age: {m.Age} | Phone: {m.PhoneNumber} | Joined: {m.JoinDate.ToShortDateString()}");
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Enter search keyword (Name or Phone): ");
                    string keyword = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("--- Search Results ---");
                    foreach (var m in memberService.SearchMembers(keyword))
                    {
                        Console.WriteLine($"ID: {m.MemberId} | Name: {m.FullName} | Phone: {m.PhoneNumber}");
                    }
                }
                else if (choice == "4")
                {
                    try
                    {
                        Console.Write("Enter Member ID to update: ");
                        string id = Console.ReadLine() ?? string.Empty;
                        Console.Write("Enter new Phone Number: ");
                        string phone = Console.ReadLine() ?? string.Empty;

                        memberService.UpdateMemberContact(id, phone);
                        storageService.SaveMembers(memberList);
                        Console.WriteLine("Member contact updated and saved successfully!");
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
                        storageService.SaveMembers(memberList);
                        Console.WriteLine("Member deleted and saved successfully!");
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
                        p.PlanId = Console.ReadLine() ?? string.Empty;

                        Console.Write("Enter Plan Name: ");
                        p.PlanName = Console.ReadLine() ?? string.Empty;

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
                        storageService.SavePlans(planList);
                        Console.WriteLine("Membership plan added and saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "7")
                {
                    Console.Clear();
                    Console.WriteLine("--- All Membership Plans ---");
                    foreach (var p in planService.GetAllPlans())
                    {
                        Console.WriteLine($"ID: {p.PlanId} | Name: {p.PlanName} | Duration: {p.DurationInDays} days | Fee: {p.Fee:C}");
                    }
                }
                else if (choice == "8")
                {
                    Console.Write("Enter search keyword (Plan ID or Name): ");
                    string keyword = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("--- Plan Search Results ---");
                    foreach (var p in planService.SearchPlans(keyword))
                    {
                        Console.WriteLine($"ID: {p.PlanId} | Name: {p.PlanName} | Duration: {p.DurationInDays} days | Fee: {p.Fee:C}");
                    }
                }
                else if (choice == "9")
                {
                    try
                    {
                        Console.Write("Enter Plan ID to update: ");
                        string id = Console.ReadLine() ?? string.Empty;

                        Console.Write("Enter New Plan Name: ");
                        string name = Console.ReadLine() ?? string.Empty;

                        Console.Write("Enter New Duration in Days: ");
                        int.TryParse(Console.ReadLine(), out int duration);

                        Console.Write("Enter New Fee: ");
                        decimal.TryParse(Console.ReadLine(), out decimal fee);

                        planService.UpdatePlan(id, name, duration, fee);
                        storageService.SavePlans(planList);
                        Console.WriteLine("Plan updated and saved successfully!");
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
                        string id = Console.ReadLine() ?? string.Empty;

                        planService.DeletePlan(id);
                        storageService.SavePlans(planList);
                        Console.WriteLine("Plan deleted and saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "11")
                {
                    try
                    {
                        Membership ms = new Membership();

                        Console.Write("Enter Membership ID: ");
                        ms.MembershipId = Console.ReadLine() ?? string.Empty;

                        Console.Write("Enter Member ID: ");
                        string memberId = Console.ReadLine() ?? string.Empty;
                        Member member = memberService.GetMemberById(memberId);

                        Console.Write("Enter Plan ID: ");
                        string planId = Console.ReadLine() ?? string.Empty;
                        MembershipPlan plan = planService.GetPlanById(planId);

                        membershipService.AssignMembership(ms, member, plan);
                        ms.MemberId = memberId;
                        ms.PlanId = planId;

                        storageService.SaveMemberships(membershipList);
                        Console.WriteLine("Membership assigned and saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "12")
                {
                    Console.Clear();
                    Console.WriteLine("--- All Active & Cancelled Memberships ---");
                    foreach (var ms in membershipService.GetAllMemberships())
                    {
                        Console.WriteLine($"ID: {ms.MembershipId} | Member: {ms.MemberId} | Plan: {ms.PlanId} | Start: {ms.StartDate.ToShortDateString()} | End: {ms.EndDate.ToShortDateString()} | Status: {ms.Status} | Fee: {ms.AmountPaid:C}");
                    }
                }
                else if (choice == "13")
                {
                    try
                    {
                        Console.Write("Enter Membership ID to cancel: ");
                        string id = Console.ReadLine() ?? string.Empty;

                        membershipService.CancelMembership(id);
                        storageService.SaveMemberships(membershipList);
                        Console.WriteLine("Membership cancelled and saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else if (choice == "14")
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }

                if (running)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
        }
    }
}