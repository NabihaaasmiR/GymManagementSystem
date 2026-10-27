using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories
{
    public class MembershipRepository
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=GymManagementDb;Integrated Security=true;TrustServerCertificate=true;";

        public List<Membership> GetAllMemberships()
        {
            List<Membership> memberships = new List<Membership>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MembershipId, MemberId, PlanId, StartDate, EndDate, Status, AmountPaid FROM Memberships";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Membership ms = new Membership
                            {
                                MembershipId = reader["MembershipId"].ToString() ?? string.Empty,
                                MemberId = reader["MemberId"].ToString() ?? string.Empty,
                                PlanId = reader["PlanId"].ToString() ?? string.Empty,
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                EndDate = Convert.ToDateTime(reader["EndDate"]),
                                Status = reader["Status"].ToString() ?? string.Empty,
                                AmountPaid = Convert.ToDecimal(reader["AmountPaid"])
                            };
                            memberships.Add(ms);
                        }
                    }
                }
            }

            return memberships;
        }

        public void SaveMembership(Membership ms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(1) FROM Memberships WHERE MembershipId = @MembershipId";
                bool exists = false;

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@MembershipId", ms.MembershipId);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0) exists = true;
                }

                string query = exists
                    ? "UPDATE Memberships SET MemberId = @MemberId, PlanId = @PlanId, StartDate = @StartDate, EndDate = @EndDate, Status = @Status, AmountPaid = @AmountPaid WHERE MembershipId = @MembershipId"
                    : "INSERT INTO Memberships (MembershipId, MemberId, PlanId, StartDate, EndDate, Status, AmountPaid) VALUES (@MembershipId, @MemberId, @PlanId, @StartDate, @EndDate, @Status, @AmountPaid)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MembershipId", ms.MembershipId);
                    command.Parameters.AddWithValue("@MemberId", ms.MemberId);
                    command.Parameters.AddWithValue("@PlanId", ms.PlanId);
                    command.Parameters.AddWithValue("@StartDate", ms.StartDate);
                    command.Parameters.AddWithValue("@EndDate", ms.EndDate);
                    command.Parameters.AddWithValue("@Status", ms.Status);
                    command.Parameters.AddWithValue("@AmountPaid", ms.AmountPaid);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}