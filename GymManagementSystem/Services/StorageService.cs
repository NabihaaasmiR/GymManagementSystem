using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GymManagementSystem.Models;

namespace GymManagementSystem.Services
{
    public class StorageService
    {
        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GymManagementDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public List<Member> LoadMembers()
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
                            Member m = new Member();
                            m.MemberId = reader["MemberId"].ToString() ?? string.Empty;
                            m.FullName = reader["FullName"].ToString() ?? string.Empty;
                            m.Age = Convert.ToInt32(reader["Age"]);
                            m.PhoneNumber = reader["PhoneNumber"].ToString() ?? string.Empty;
                            m.JoinDate = Convert.ToDateTime(reader["JoinDate"]);
                            members.Add(m);
                        }
                    }
                }
            }

            return members;
        }

        public void SaveMembers(List<Member> members)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var m in members)
                {
                    string checkQuery = "SELECT COUNT(1) FROM Members WHERE MemberId = @MemberId";
                    bool exists = false;

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MemberId", m.MemberId);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            exists = true;
                        }
                    }

                    string query = "";
                    if (exists)
                    {
                        query = "UPDATE Members SET FullName = @FullName, Age = @Age, PhoneNumber = @PhoneNumber, JoinDate = @JoinDate WHERE MemberId = @MemberId";
                    }
                    else
                    {
                        query = "INSERT INTO Members (MemberId, FullName, Age, PhoneNumber, JoinDate) VALUES (@MemberId, @FullName, @Age, @PhoneNumber, @JoinDate)";
                    }

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

        public List<MembershipPlan> LoadPlans()
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
                            MembershipPlan p = new MembershipPlan();
                            p.PlanId = reader["PlanId"].ToString() ?? string.Empty;
                            p.PlanName = reader["PlanName"].ToString() ?? string.Empty;
                            p.DurationInDays = Convert.ToInt32(reader["DurationInDays"]);
                            p.Fee = Convert.ToDecimal(reader["Fee"]);
                            plans.Add(p);
                        }
                    }
                }
            }

            return plans;
        }

        public void SavePlans(List<MembershipPlan> plans)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var p in plans)
                {
                    string checkQuery = "SELECT COUNT(1) FROM MembershipPlans WHERE PlanId = @PlanId";
                    bool exists = false;

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@PlanId", p.PlanId);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            exists = true;
                        }
                    }

                    string query = "";
                    if (exists)
                    {
                        query = "UPDATE MembershipPlans SET PlanName = @PlanName, DurationInDays = @DurationInDays, Fee = @Fee WHERE PlanId = @PlanId";
                    }
                    else
                    {
                        query = "INSERT INTO MembershipPlans (PlanId, PlanName, DurationInDays, Fee) VALUES (@PlanId, @PlanName, @DurationInDays, @Fee)";
                    }

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

        public List<Membership> LoadMemberships()
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
                            Membership ms = new Membership();
                            ms.MembershipId = reader["MembershipId"].ToString() ?? string.Empty;
                            ms.MemberId = reader["MemberId"].ToString() ?? string.Empty;
                            ms.PlanId = reader["PlanId"].ToString() ?? string.Empty;
                            ms.StartDate = Convert.ToDateTime(reader["StartDate"]);
                            ms.EndDate = Convert.ToDateTime(reader["EndDate"]);
                            ms.Status = reader["Status"].ToString() ?? string.Empty;
                            ms.AmountPaid = Convert.ToDecimal(reader["AmountPaid"]);
                            memberships.Add(ms);
                        }
                    }
                }
            }

            return memberships;
        }

        public void SaveMemberships(List<Membership> memberships)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var ms in memberships)
                {
                    string checkQuery = "SELECT COUNT(1) FROM Memberships WHERE MembershipId = @MembershipId";
                    bool exists = false;

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MembershipId", ms.MembershipId);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            exists = true;
                        }
                    }

                    string query = "";
                    if (exists)
                    {
                        query = "UPDATE Memberships SET MemberId = @MemberId, PlanId = @PlanId, StartDate = @StartDate, EndDate = @EndDate, Status = @Status, AmountPaid = @AmountPaid WHERE MembershipId = @MembershipId";
                    }
                    else
                    {
                        query = "INSERT INTO Memberships (MembershipId, MemberId, PlanId, StartDate, EndDate, Status, AmountPaid) VALUES (@MembershipId, @MemberId, @PlanId, @StartDate, @EndDate, @Status, @AmountPaid)";
                    }

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
}