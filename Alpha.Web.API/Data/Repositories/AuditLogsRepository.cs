using Alpha.Web.API.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public class AuditLogsRepository : GenericReadOnlyRepository<int, AuditLog>, IAuditLogsRepository
    {
        private readonly AlphaDbContext _context;

        public AuditLogsRepository(AlphaDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AuditLog>> GetByCriteriaAsync(string action, DateTime? endDate, DateTime? startDate,
           int? tableId, string tableName)
        {
            var actionParameter = new SqlParameter("Action", action);

            var endDateParameter = new SqlParameter
            {
                ParameterName = "EndDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Output,
                IsNullable = true
            };

            var startDateParameter = new SqlParameter
            {
                ParameterName = "StartDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Output,
                IsNullable = true
            };

            var tableNameParameter = new SqlParameter("TableName", tableName);

            var tableIdParameter = new SqlParameter
            {
                ParameterName = "TableId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                IsNullable = true
            };

            return await _context.AuditLogs.FromSqlRaw("EXEC [dbo].[AuditLogs_GetFiltered]  @Action, @EndDate, @StartDate, " +
                "@TableName, @TableId", actionParameter, endDateParameter, startDateParameter, tableNameParameter,
                tableIdParameter).ToListAsync();
        }
    }
}
