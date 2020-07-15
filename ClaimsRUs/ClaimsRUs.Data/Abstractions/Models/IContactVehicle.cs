using System;

namespace ClaimsRUs.Data.Abstractions.Models
{
    public interface IContactVehicle : IViewModel
    {
        Guid ContactId { get; set; }
        Guid VehicleId { get; set; }
        IContact Contact { get; set; }
        IVehicle Vehicle { get; set; }
    }
}
