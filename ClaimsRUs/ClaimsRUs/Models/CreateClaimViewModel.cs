using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimsRUs.Models
{
    public class CreateClaimViewModel
    {
        public Guid SelectedContactId { get; set; }
        public ClaimViewModel Claim  { get; set; }
        public List<IContact> ContactList{ get; set; }
        public Guid SelectedVehicleId { get; set; }
        public List<IVehicle> VehicleList { get; set; }
    }
}
