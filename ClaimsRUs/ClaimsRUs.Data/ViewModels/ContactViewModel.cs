using ClaimsRUs.Data.Abstractions.Models;
using System;
namespace ClaimsRUs.Models
{
    public class ContactViewModel : IContact
    {
        public string DisplayName => $"{LName}, {FName}";
        public Guid ContactId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string MPhone { get; set; }
        public string HPhone { get; set; }
        public string OPhone { get; set; }
        public string Street { get; set; }
        public string City  { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
