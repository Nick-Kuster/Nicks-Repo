using System;

namespace ClaimsRUs.Data.Abstractions.Models
{
    public interface IClaim : IViewModel
    {
        Guid ClaimId { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateOfClaim { get; set; }
        string Description { get; set; }

    }
}
