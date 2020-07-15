using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Entity.Models;
using System;

namespace ClaimsRUs.Data.ViewModels
{
    public class VehicleViewModel : IVehicle
    {
        public string DisplayName => $"{Year} {Make} {Model} : {Color}";
        public Guid VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
    }     
}
