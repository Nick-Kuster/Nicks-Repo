using ClaimsRUs.Entity.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClaimsRUs.Data.Abstractions.Models
{
    public interface IClaim : IViewModel
    {
        Guid ClaimId { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateOfClaim { get; set; }
        string Description { get; set; }
        IEnumerable<IContactVehicle> ContactVehicles { get; set; }
    }
}
