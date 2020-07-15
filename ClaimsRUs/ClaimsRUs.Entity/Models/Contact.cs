using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClaimsRUs.Entity.Models
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string MPhone { get; set; }
        public string HPhone { get; set; }
        public string OPhone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<ClaimContactVehicle> ClaimContactVehicles { get; set; }
    }
}
