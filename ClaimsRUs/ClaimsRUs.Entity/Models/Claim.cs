using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClaimsRUs.Entity.Models
{
    public class Claim
    {
        [Key]
        public Guid ClaimId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfClaim { get; set; }
        public string Description { get; set; }
    }
}
