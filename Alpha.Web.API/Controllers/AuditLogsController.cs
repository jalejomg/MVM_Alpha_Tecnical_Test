using Alpha.Web.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{
    /// <summary>
    /// This class contain all APIs endpoints about Audit logs 
    /// </summary>
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogsService _auditLogService;

        public AuditLogsController(IAuditLogsService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        [Route("api/auditLogs")]
        public async Task<IActionResult> GetAuditLogsByCriteria(DateTime? endDate,
            DateTime? startDate, int? tableId, string tableName = "Messages", string action = "")
        {
            return Ok(await _auditLogService.GetByCriteriaAsync(action, endDate, startDate, tableId, tableName));
        }
    }
}
