using Alpha.Web.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAuditLogsByCriteria(string action, DateTime endDate,
            DateTime startDate, string tableName, int tableId, int userId)
        {
            return Ok(await _auditLogService.GetByCriteriaAsync(action, endDate, startDate, tableName, tableId, userId));
        }
    }
}
