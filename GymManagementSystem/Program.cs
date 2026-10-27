using System;
using Microsoft.Data.SqlClient;
using GymManagementSystem.Models;
using System.Data;

namespace GymManagementSystem.Repositories
{
    public partial class MembershipRepository
    {
        // Transactional method: Creates a membership and records an audit log atomically
        public void SaveMembershipWithTransaction(Membership ms, string adminUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 1. Begin the transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Check if membership already exists (for update vs insert)
                        string checkQuery = "SELECT COUNT(1) FROM Memberships WHERE MembershipId = @MembershipId";
                        bool exists = false;

                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@MembershipId", ms.MembershipId);
                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                            if (count > 0) exists = true;
                        }

                        // 2. Perform the Insert or Update
                        string query = exists
                            ? "UPDATE Memberships SET MemberId = @MemberId, PlanId = @PlanId, StartDate = @StartDate, EndDate = @EndDate, Status = @Status, AmountPaid = @AmountPaid WHERE MembershipId = @MembershipId"
                            : "INSERT INTO Memberships (MembershipId, MemberId, PlanId, StartDate, EndDate, Status, AmountPaid) VALUES (@MembershipId, @MemberId, @PlanId, @StartDate, @EndDate, @Status, @AmountPaid)";

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
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

                        // 3. Write an entry into the AuditLogs table within the same transaction
                        string auditQuery = "INSERT INTO AuditLogs (EntityName, EntityId, Action, Details) VALUES (@EntityName, @EntityId, @Action, @Details)";
                        using (SqlCommand auditCommand = new SqlCommand(auditQuery, connection, transaction))
                        {
                            auditCommand.Parameters.AddWithValue("@EntityName", "Membership");
                            auditCommand.Parameters.AddWithValue("@EntityId", ms.MembershipId);
                            auditCommand.Parameters.AddWithValue("@Action", exists ? "UPDATE" : "INSERT");
                            auditCommand.Parameters.AddWithValue("@Details", $"Membership {ms.MembershipId} for Member {ms.MemberId} processed successfully by {adminUser}.");
                            auditCommand.ExecuteNonQuery();
                        }

                        // 4. If everything succeeds, commit the transaction!
                        transaction.Commit();
                        Console.WriteLine("Transaction committed successfully: Membership and Audit Log saved.");
                    }
                    catch (Exception ex)
                    {
                        // 5. If any error occurs, roll back everything!
                        transaction.Rollback();
                        Console.WriteLine($"Transaction failed and rolled back. Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}