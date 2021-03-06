﻿using ClaimsRUs.Data.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClaimsRUs.Data.ViewModels
{
    public class ContactVehicleViewModel : IContactVehicle
    {
        public Guid ContactId { get; set; }
        public Guid VehicleId { get; set; }
        public IContact Contact { get; set; }
        public IVehicle Vehicle { get; set ; }
    }
}
