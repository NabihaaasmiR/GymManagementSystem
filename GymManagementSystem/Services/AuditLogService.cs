using System.Collections.Generic;
using System.Threading.Tasks;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories;

namespace GymManagementSystem.Services
{
    public class AuditLogService
    {
        private readonly AuditLogRepository _repository;

        public AuditLogService(AuditLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AuditLog>> GetRecentLogsAsync(int count = 10)
        {
            // You could add business rules here (e.g., filtering or formatting) before returning
            return await _repository.GetRecentAuditLogsAsync(count);
        }
    }
}