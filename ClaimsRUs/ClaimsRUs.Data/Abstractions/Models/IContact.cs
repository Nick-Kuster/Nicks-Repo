using System;

namespace ClaimsRUs.Data.Abstractions.Models
{
    public interface IContact : IViewModel
    {
        Guid ContactId { get; set; }
        string FName { get; set; }
        string LName { get; set; }
        string MPhone { get; set; }
        string HPhone { get; set; }
        string OPhone { get; set; }
        string Street { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
    }
}
