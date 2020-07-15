using ClaimsRUs.Data.Abstractions.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClaimsRUs.Models
{
    public class ContactViewModel : IContact
    {
        public string DisplayName => $"{LName}, {FName}";
        public Guid ContactId { get; set; }

        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Display(Name = "Mobile Phone")]
        public string MPhone { get; set; }
        [Display(Name = "Home Phone")]
        public string HPhone { get; set; }
        [Display(Name = "Office Phone")]
        public string OPhone { get; set; }
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Display(Name = "City")]
        public string City  { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip")]
        public string Zip { get; set; }
    }
}
