using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories
{
    public class MemberRepository
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=GymManagementDb;Integrated Security=true;TrustServerCertificate=true;";

        // --- ASYNCHRONOUS METHODS ---
        public async Task<List<Member>> GetAllMembersAsync()
        {
            List<Member> members = new List<Member>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT MemberId, FullName, Age, PhoneNumber, JoinDate FROM Members";

                await using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Member m = new Member
                            {
                                MemberId = reader["MemberId"].ToString() ?? string.Empty,
                                FullName = reader["FullName"].ToString() ?? string.Empty,
                                Age = Convert.ToInt32(reader["Age"]),
                                PhoneNumber = reader["PhoneNumber"].ToString() ?? string.Empty,
                                JoinDate = Convert.ToDateTime(reader["JoinDate"])
                            };
                            members.Add(m);
                        }
                    }
                }
            }

            return members;
        }

        public async Task SaveMemberAsync(Member m)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string checkQuery = "SELECT COUNT(1) FROM Members WHERE MemberId = @MemberId";
                bool exists = false;

                await using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@MemberId", m.MemberId);
                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                    if (count > 0) exists = true;
                }

                string query = exists
                    ? "UPDATE Members SET FullName = @FullName, Age = @Age, PhoneNumber = @PhoneNumber, JoinDate = @JoinDate WHERE MemberId = @MemberId"
                    : "INSERT INTO Members (MemberId, FullName, Age, PhoneNumber, JoinDate) VALUES (@MemberId, @FullName, @Age, @PhoneNumber, @JoinDate)";

                await using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberId", m.MemberId);
                    command.Parameters.AddWithValue("@FullName", m.FullName);
                    command.Parameters.AddWithValue("@Age", m.Age);
                    command.Parameters.AddWithValue("@PhoneNumber", m.PhoneNumber);
                    command.Parameters.AddWithValue("@JoinDate", m.JoinDate);

                    await command.ExecuteNonQueryAsync();
                }

                // Automatically record an entry in AuditLogs for tracking
                string auditQuery = "INSERT INTO AuditLogs (EntityName, EntityId, Action, Details) VALUES (@EntityName, @EntityId, @Action, @Details)";
                await using (SqlCommand auditCommand = new SqlCommand(auditQuery, connection))
                {
                    auditCommand.Parameters.AddWithValue("@EntityName", "Member");
                    auditCommand.Parameters.AddWithValue("@EntityId", m.MemberId);
                    auditCommand.Parameters.AddWithValue("@Action", exists ? "UPDATE" : "INSERT");
                    auditCommand.Parameters.AddWithValue("@Details", $"Member {m.FullName} ({m.MemberId}) was {(exists ? "updated" : "registered")}.");
                    await auditCommand.ExecuteNonQueryAsync();
                }
            }
        }

        // --- SYNCHRONOUS WRAPPER/FALLBACK METHODS (Prevents missing method errors) ---
        public List<Member> GetAllMembers()
        {
            return GetAllMembersAsync().GetAwaiter().GetResult();
        }

        public void SaveMember(Member m)
        {
            SaveMemberAsync(m).GetAwaiter().GetResult();
        }
    }
}