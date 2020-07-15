using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Entity.Models;
using ClaimsRUs.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClaimsRUs.Data.ViewModels
{
    public class VehicleViewModel : IVehicle
    {
        public string DisplayName => $"{Year} {Make} {Model} : {Color}";
        public Guid VehicleId { get; set; }
        public Guid ContactId { get; set; }
        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
        public IContact Contact { get; set; }
    }     
}
