using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClaimsRUs.Entity.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
