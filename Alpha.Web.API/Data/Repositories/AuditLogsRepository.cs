using Alpha.Web.API.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public class AuditLogsRepository : GenericReadOnlyRepository<AuditLog>, IAuditLogsRepository
    {
        private readonly AlphaDbContext _context;

        public AuditLogsRepository(AlphaDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AuditLog>> GetByCriteriaAsync(string action, DateTime endDate, DateTime startDate,
            string tableName, int tableId)
        {
            var actionParameter = new SqlParameter("Action", action);
            var endDateParameter = new SqlParameter("EndDate", action);
            var startDateParameter = new SqlParameter("StartDate", action);
            var tableNameParameter = new SqlParameter("TableName", action);
            var tableIdParameter = new SqlParameter("TableId", action);

            return await _context.AuditLogs.FromSqlRaw("EXEC [dbo].[Filters] @Action, @EndDate, @StartDate, @TableName," +
                "@TableId", actionParameter, endDateParameter, startDateParameter, tableNameParameter,
                tableIdParameter).ToListAsync();
        }
    }
}
