using Alpha.Web.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This class declare methods to transact data from AuditLogs table
    /// </summary>
    public interface IAuditLogsRepository : IGenericReadOnlyRepository<int, AuditLog>
    {
        Task<IEnumerable<AuditLog>> GetByCriteriaAsync(string action, DateTime? endDate, DateTime? startDate,
           int? tableId, string tableName);
    }
}
