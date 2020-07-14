using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClaimsRUs.Entity.Models
{
    public class UserAuditTrail
    {
        [Key]
        public Guid UserAuditTrailId { get; set; }
        public Guid UserId { get; set; }
        public DateTime AuditDate { get; set; }
        public string Description { get; set; }
    }
}
