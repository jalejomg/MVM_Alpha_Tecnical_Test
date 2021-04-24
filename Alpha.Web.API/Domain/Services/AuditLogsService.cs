using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public class AuditLogsService : IAuditLogsService
    {
        private readonly IAuditLogsRepository _auditLogRepository;

        public AuditLogsService(IAuditLogsRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        public async Task<ResponseModel<IEnumerable<AuditLogModel>>> GetByCriteriaAsync(string action, DateTime? endDate,
            DateTime? startDate, int? tableId, string tableName)
        {
            var auditLogEntities = await _auditLogRepository.GetByCriteriaAsync(action, endDate,
                startDate, tableId, tableName);

            return new ResponseModel<IEnumerable<AuditLogModel>>
            {
                Data = AuditLogModel.MakeMany(auditLogEntities),
                Count = auditLogEntities.Count()
            };
        }
    }
}
