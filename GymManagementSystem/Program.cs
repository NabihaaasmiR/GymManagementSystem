using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories;

namespace GymManagementSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MemberRepository memberRepo = new MemberRepository();
            AuditLogRepository auditRepo = new AuditLogRepository();
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
                Console.WriteLine("4. View Audit Logs");
                Console.WriteLine("5. Exit");
                Console.WriteLine("========================================");
                Console.Write("Select an option (1-5): ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ManageMembersMenuAsync(memberRepo);
                        break;

                    case "2":
                        await ManagePlansMenuAsync();
                        break;

                    case "3":
                        await ManageMembershipsMenuAsync();
                        break;

                    case "4":
                        await DisplayAuditLogsAsync(auditRepo);
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("\nExiting application. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // --- SUBMENU: MANAGE MEMBERS ---
        private static async Task ManageMembersMenuAsync(MemberRepository repo)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("          MANAGE MEMBERS                ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. View All Members");
                Console.WriteLine("2. Add or Update Member");
                Console.WriteLine("3. Return to Main Menu");
                Console.WriteLine("========================================");
                Console.Write("Select an option (1-3): ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DisplayAllMembersAsync(repo);
                        break;
                    case "2":
                        await AddOrUpdateMemberAsync(repo);
                        break;
                    case "3":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // --- SUBMENU: MANAGE PLANS (Placeholder for future module) ---
        private static async Task ManagePlansMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Manage Membership Plans ---");
            Console.WriteLine("Plans functionality is ready for integration.");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            await Task.CompletedTask;
        }

        // --- SUBMENU: MANAGE MEMBERSHIPS (Placeholder for future module) ---
        private static async Task ManageMembershipsMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Manage Memberships ---");
            Console.WriteLine("Memberships functionality is ready for integration.");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            await Task.CompletedTask;
        }

        // --- MEMBER ACTIONS ---
        private static async Task DisplayAllMembersAsync(MemberRepository repo)
        {
            Console.Clear();
            Console.WriteLine("--- Registered Members ---");
            try
            {
                List<Member> members = await repo.GetAllMembersAsync();

                if (members.Count == 0)
                {
                    Console.WriteLine("No members found in the database.");
                }
                else
                {
                    Console.WriteLine($"{"ID",-10} {"Full Name",-20} {"Age",-5} {"Phone Number",-15} {"Join Date",-12}");
                    Console.WriteLine(new string('-', 65));

                    foreach (var m in members)
                    {
                        Console.WriteLine($"{m.MemberId,-10} {m.FullName,-20} {m.Age,-5} {m.PhoneNumber,-15} {m.JoinDate.ToShortDateString(),-12}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving members: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private static async Task AddOrUpdateMemberAsync(MemberRepository repo)
        {
            Console.Clear();
            Console.WriteLine("--- Add / Update Member ---");

            Console.Write("Enter Member ID (e.g., M001): ");
            string memberId = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(memberId))
            {
                Console.WriteLine("Member ID cannot be empty.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Full Name: ");
            string fullName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age entered.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine() ?? string.Empty;

            Member member = new Member
            {
                MemberId = memberId,
                FullName = fullName,
                Age = age,
                PhoneNumber = phoneNumber,
                JoinDate = DateTime.Now
            };

            try
            {
                await repo.SaveMemberAsync(member);
                Console.WriteLine("\nMember saved successfully and action logged to Audit Logs!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError saving member: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        // --- AUDIT LOG VIEWER ---
        private static async Task DisplayAuditLogsAsync(AuditLogRepository repo)
        {
            Console.Clear();
            Console.WriteLine("--- Recent Audit Logs (Top 10) ---");
            try
            {
                List<AuditLog> logs = await repo.GetRecentAuditLogsAsync(10);

                if (logs.Count == 0)
                {
                    Console.WriteLine("No audit logs found.");
                }
                else
                {
                    Console.WriteLine($"{"ID",-6} {"Entity",-10} {"Entity ID",-10} {"Action",-8} {"Time",-22} {"Details"}");
                    Console.WriteLine(new string('-', 90));

                    foreach (var log in logs)
                    {
                        Console.WriteLine($"{log.AuditId,-6} {log.EntityName,-10} {log.EntityId,-10} {log.Action,-8} {log.CreatedAt,-22} {log.Details}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving audit logs: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}