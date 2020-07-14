using ClaimsRUs.Data.Abstractions.Models;
using System;

namespace ClaimsRUs.Models
{
    public class Claim : IClaim
    {
        public Guid ClaimId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfClaim { get; set; }
        public string Description { get; set; }
    }
}
