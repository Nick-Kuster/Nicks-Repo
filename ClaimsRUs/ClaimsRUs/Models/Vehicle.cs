using ClaimsRUs.Data.Abstractions.Models;
using System;

namespace ClaimsRUs.Models
{
    public class Vehicle : IVehicle
    {
        public Guid VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
    }     
}
