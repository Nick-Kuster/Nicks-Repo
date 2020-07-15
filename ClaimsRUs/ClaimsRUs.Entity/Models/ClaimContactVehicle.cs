using System;
using System.Collections.Generic;
using System.Text;

namespace ClaimsRUs.Entity.Models
{
    public class ClaimContactVehicle
    {
        public Guid ClaimId { get; set; }
        public Claim Claim { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } 
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
