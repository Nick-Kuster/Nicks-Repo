using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClaimsRUs.Entity.Models
{
    public class Vehicle
    {
        [Key]
        public Guid VehicleId { get; set; }
        public Contact Contact { get; set; }
        public Guid ContactId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public ICollection<ClaimContactVehicle> ClaimContactVehicles { get; set; }
    }
}
