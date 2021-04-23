﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Web.API.Data.Entities
{
    public class AuditLog : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TableName { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Action { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}