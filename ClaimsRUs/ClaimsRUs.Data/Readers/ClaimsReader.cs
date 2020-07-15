using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Data.Abstractions.Readers;
using ClaimsRUs.Data.ViewModels;
using ClaimsRUs.Entity;
using ClaimsRUs.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimsRUs.Data.Readers
{
    public class ClaimsReader : IClaimsReader
    {
        private readonly Context _dbContext;

        public ClaimsReader(Context context)
        {
            _dbContext = context;
        }

        public IClaim Read(Guid id)
        {
            Claim fromDb = _dbContext.claim.FirstOrDefault(x => x.ClaimId == id) ?? throw new Exception("Claim not found");

            IClaim viewModel = ConvertToViewModel(fromDb);

            return viewModel;
        }
        

        public IEnumerable<IClaim> ReadAll()
        {
            var fromDb = _dbContext.claim.ToList();

            List<IClaim> viewModelList = new List<IClaim>();

            foreach(var record in fromDb)
            {
                viewModelList.Add(ConvertToViewModel(record));
            }

            return viewModelList;
        }

        public IClaim ConvertToViewModel(Claim fromDb)
        {
            return new ClaimViewModel()
            {
                ClaimId = fromDb.ClaimId,
                DateCreated = fromDb.DateCreated,
                DateOfClaim = fromDb.DateOfClaim,
                Description = fromDb.Description
            };
        }
    }
}
