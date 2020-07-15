using ClaimsRUs.Data.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace ClaimsRUs.Data.ViewModels
{
    public class ClaimViewModel : IClaim
    {
        public Guid ClaimId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfClaim { get; set; }
        public string Description { get; set; }
        public IEnumerable<IContactVehicle> ContactVehicles { get; set; }
    }
}
