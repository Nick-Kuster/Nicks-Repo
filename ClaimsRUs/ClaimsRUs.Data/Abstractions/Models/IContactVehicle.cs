namespace ClaimsRUs.Data.Abstractions.Models
{
    public interface IContactVehicle : IViewModel
    {
        IContact Contact { get; set; }
        IVehicle Vehicle { get; set; }
    }
}
