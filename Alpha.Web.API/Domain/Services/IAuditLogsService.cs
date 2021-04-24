using Alpha.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public interface IAuditLogsService
    {
        Task<ResponseModel<IEnumerable<AuditLogModel>>> GetByCriteriaAsync(string action, DateTime? endDate,
            DateTime? startDate, int? tableId, string tableName);
    }
}
