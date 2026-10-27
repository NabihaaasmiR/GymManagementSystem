using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories
{
    public class AuditLogRepository
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=GymManagementDb;Integrated Security=true;TrustServerCertificate=true;";

        // Asynchronously fetch the most recent audit logs
        public async Task<List<AuditLog>> GetRecentAuditLogsAsync(int topCount = 10)
        {
            List<AuditLog> logs = new List<AuditLog>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = $"SELECT TOP {topCount} AuditId, EntityName, EntityId, Action, Details, CreatedAt FROM AuditLogs ORDER BY CreatedAt DESC";

                await using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AuditLog log = new AuditLog
                            {
                                AuditId = Convert.ToInt64(reader["AuditId"]),
                                EntityName = reader["EntityName"].ToString() ?? string.Empty,
                                EntityId = reader["EntityId"].ToString() ?? string.Empty,
                                Action = reader["Action"].ToString() ?? string.Empty,
                                Details = reader["Details"] as string,
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            };
                            logs.Add(log);
                        }
                    }
                }
            }

            return logs;
        }
    }
}