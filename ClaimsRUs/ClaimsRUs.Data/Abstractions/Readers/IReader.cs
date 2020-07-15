using ClaimsRUs.Data.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace ClaimsRUs.Data.Abstractions.Readers
{
    public interface IReader<TViewModel> where TViewModel : IViewModel
    {
        IEnumerable<TViewModel> ReadAll();
        TViewModel Read(Guid id);
    }
}
