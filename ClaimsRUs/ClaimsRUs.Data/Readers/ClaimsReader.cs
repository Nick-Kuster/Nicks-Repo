using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Data.Abstractions.Readers;
using System;
using System.Collections.Generic;

namespace ClaimsRUs.Data.Readers
{
    public class ClaimsReader : IClaimsReader
    {
        public IClaim Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IClaim> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
