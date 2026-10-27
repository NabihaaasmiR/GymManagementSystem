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
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("       GYM MANAGEMENT SYSTEM            ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. View All Members");
                Console.WriteLine("2. Add or Update Member");
                Console.WriteLine("3. Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DisplayAllMembersAsync(memberRepo);
                        break;

                    case "2":
                        await AddOrUpdateMemberAsync(memberRepo);
                        break;

                    case "3":
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

        private static async Task DisplayAllMembersAsync(MemberRepository repo)
        {
            Console.Clear();
            Console.WriteLine("--- Registered Members ---");
            try
            {
                // Calling the asynchronous repository method
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

            Console.WriteLine("\nPress any key to return to the main menu...");
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
                // Calling the asynchronous save method
                await repo.SaveMemberAsync(member);
                Console.WriteLine("\nMember saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError saving member: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}