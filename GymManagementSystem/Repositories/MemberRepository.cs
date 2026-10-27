using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories
{
    public class MemberRepository
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=GymManagementDb;Integrated Security=true;TrustServerCertificate=true;";

        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MemberId, FullName, Age, PhoneNumber, JoinDate FROM Members";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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

        public void SaveMember(Member m)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(1) FROM Members WHERE MemberId = @MemberId";
                bool exists = false;

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@MemberId", m.MemberId);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0) exists = true;
                }

                string query = exists
                    ? "UPDATE Members SET FullName = @FullName, Age = @Age, PhoneNumber = @PhoneNumber, JoinDate = @JoinDate WHERE MemberId = @MemberId"
                    : "INSERT INTO Members (MemberId, FullName, Age, PhoneNumber, JoinDate) VALUES (@MemberId, @FullName, @Age, @PhoneNumber, @JoinDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberId", m.MemberId);
                    command.Parameters.AddWithValue("@FullName", m.FullName);
                    command.Parameters.AddWithValue("@Age", m.Age);
                    command.Parameters.AddWithValue("@PhoneNumber", m.PhoneNumber);
                    command.Parameters.AddWithValue("@JoinDate", m.JoinDate);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}