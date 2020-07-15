using ClaimsRUs.Entity.Models;
using System;

namespace ClaimsRUs.Data.Abstractions.Models
{
    public interface IVehicle : IViewModel
    {
        Guid VehicleId { get; set; }
        Guid ContactId { get; set; }
        string Make { get; set; }
        string Model { get; set; }
        string Year { get; set; }
        string Color { get; set; }
        IContact Contact { get; set; }
    }
}
