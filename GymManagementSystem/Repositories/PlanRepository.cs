using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories
{
    public class PlanRepository
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=GymManagementDb;Integrated Security=true;TrustServerCertificate=true;";

        public List<MembershipPlan> GetAllPlans()
        {
            List<MembershipPlan> plans = new List<MembershipPlan>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PlanId, PlanName, DurationInDays, Fee FROM MembershipPlans";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MembershipPlan p = new MembershipPlan
                            {
                                PlanId = reader["PlanId"].ToString() ?? string.Empty,
                                PlanName = reader["PlanName"].ToString() ?? string.Empty,
                                DurationInDays = Convert.ToInt32(reader["DurationInDays"]),
                                Fee = Convert.ToDecimal(reader["Fee"])
                            };
                            plans.Add(p);
                        }
                    }
                }
            }

            return plans;
        }

        public void SavePlan(MembershipPlan p)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(1) FROM MembershipPlans WHERE PlanId = @PlanId";
                bool exists = false;

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@PlanId", p.PlanId);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0) exists = true;
                }

                string query = exists
                    ? "UPDATE MembershipPlans SET PlanName = @PlanName, DurationInDays = @DurationInDays, Fee = @Fee WHERE PlanId = @PlanId"
                    : "INSERT INTO MembershipPlans (PlanId, PlanName, DurationInDays, Fee) VALUES (@PlanId, @PlanName, @DurationInDays, @Fee)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlanId", p.PlanId);
                    command.Parameters.AddWithValue("@PlanName", p.PlanName);
                    command.Parameters.AddWithValue("@DurationInDays", p.DurationInDays);
                    command.Parameters.AddWithValue("@Fee", p.Fee);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}