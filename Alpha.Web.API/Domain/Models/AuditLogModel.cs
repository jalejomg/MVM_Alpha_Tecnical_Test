using Alpha.Web.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Web.API.Domain.Models
{
    public class AuditLogModel
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int TableId { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }

        public static AuditLogModel MakeOne(AuditLog auditLogEntity)
        {
            return new AuditLogModel
            {
                Id = auditLogEntity.Id,
                TableName = auditLogEntity.TableName,
                TableId = auditLogEntity.TableId,
                Action = auditLogEntity.Action,
                Date = auditLogEntity.Date,
                User = auditLogEntity.User
            };
        }

        public static IEnumerable<AuditLogModel> MakeMany(IEnumerable<AuditLog> auditLogEntities)
        {
            return auditLogEntities.Select(auditLogEntity => MakeOne(auditLogEntity));
        }
    }
}
