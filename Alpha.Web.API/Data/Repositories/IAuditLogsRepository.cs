using Alpha.Web.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public interface IAuditLogsRepository : IGenericReadOnlyRepository<AuditLog>
    {
        Task<IEnumerable<AuditLog>> GetByCriteriaAsync(string action, DateTime endDate, DateTime startDate,
            string tableName, int tableId);
    }
}
