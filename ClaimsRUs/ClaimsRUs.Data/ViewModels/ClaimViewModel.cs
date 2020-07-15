using ClaimsRUs.Data.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ClaimsRUs.Data.ViewModels
{
    public class ClaimViewModel : IClaim
    {
        public Guid ClaimId { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date of Claim")]
        public DateTime DateOfClaim { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public IEnumerable<IContactVehicle> ContactVehicles { get; set; }
    }
}
