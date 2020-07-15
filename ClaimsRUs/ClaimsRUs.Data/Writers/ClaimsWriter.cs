using ClaimsRUs.Data.Abstractions.Writers;
using ClaimsRUs.Data.ViewModels;
using ClaimsRUs.Entity;
using ClaimsRUs.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ClaimsRUs.Data.Writers
{
    public class ClaimsWriter : IClaimsWriter
    {
        private readonly Context _dbContext;

        public ClaimsWriter(Context context)
        {
            _dbContext = context;
        }

        public void Write(ClaimViewModel viewModel)
        {
            Claim existing = _dbContext.claim.FirstOrDefault(x => x.ClaimId == viewModel.ClaimId);

            if(existing != null)
            {
                existing.DateCreated = viewModel.DateCreated;
                existing.DateOfClaim = viewModel.DateOfClaim;
                existing.Description = viewModel.Description;
                UpdateContactVehicles(viewModel);
            }
            else
            {
                viewModel.ClaimId = Guid.NewGuid();
                Claim newClaim = new Claim()
                {
                    ClaimId = viewModel.ClaimId,
                    DateCreated = viewModel.DateCreated,
                    DateOfClaim = viewModel.DateOfClaim,
                    Description = viewModel.Description
                };
                _dbContext.claim.Add(newClaim);
                _dbContext.SaveChanges();
                UpdateContactVehicles(viewModel);
            };
        }

        private void UpdateContactVehicles(ClaimViewModel viewModel)
        {
            foreach(var contactVehicle in viewModel.ContactVehicles)
            {
                var existing = _dbContext.claimContactVehicle
                                        .FirstOrDefault(
                                        x => x.ClaimId == viewModel.ClaimId
                                        && x.ContactId == contactVehicle.ContactId
                                        && x.VehicleId == contactVehicle.VehicleId);
                if(existing == null)
                {
                    ClaimContactVehicle ccv = new ClaimContactVehicle()
                    {
                        ClaimId = viewModel.ClaimId,
                        ContactId = contactVehicle.ContactId,
                        VehicleId = contactVehicle.VehicleId
                    };
                    _dbContext.claimContactVehicle.Add(ccv);
                }
            }
            _dbContext.SaveChanges();
        }

        public void WriteAll(IEnumerable<ClaimViewModel> viewModels)
        {
            throw new NotImplementedException();
        }
    }
}
