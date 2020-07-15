using ClaimsRUs.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ClaimsRUs.Data.Abstractions.Writers
{
    public interface IClaimsWriter : IWriter<ClaimViewModel>
    {
    }
}
