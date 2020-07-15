using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Data.Abstractions.Readers;
using ClaimsRUs.Data.ViewModels;
using ClaimsRUs.Entity;
using ClaimsRUs.Entity.Models;
using ClaimsRUs.Models;
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
            var contacts = _dbContext.contact.ToList();
            var vehicles = _dbContext.vehicle.ToList();
            var claimContactVehicles = _dbContext.claimContactVehicle.ToList();

            Claim fromDb = _dbContext.claim.FirstOrDefault(x => x.ClaimId == id) ?? throw new Exception("Claim not found");

            IClaim viewModel = ConvertToViewModel(fromDb);

            return viewModel;
        }
        

        public IEnumerable<IClaim> ReadAll()
        {
            var contacts = _dbContext.contact.ToList();
            var vehicles = _dbContext.vehicle.ToList();
            var claimContactVehicles = _dbContext.claimContactVehicle.ToList();
            var fromDb = _dbContext.claim.ToList();

            List<IClaim> viewModelList = new List<IClaim>();

            foreach(var record in fromDb)
            {
                viewModelList.Add(ConvertToViewModel(record));
            }

            return viewModelList;
        }

        private IClaim ConvertToViewModel(Claim fromDb)
        {
            var contactVehicles = fromDb.ClaimContactVehicles?
                                    .Select(x => new ContactVehicleViewModel() { Contact = ConvertContactToViewModel(x.Contact), Vehicle = ConvertVehicleToViewModel(x.Vehicle) });
            return new ClaimViewModel()
            {
                ClaimId = fromDb.ClaimId,
                DateCreated = fromDb.DateCreated,
                DateOfClaim = fromDb.DateOfClaim,
                Description = fromDb.Description,
                ContactVehicles = contactVehicles
            };
        }

        private IContact ConvertContactToViewModel(Contact fromDb)
        {
            return new ContactViewModel()
            {
                ContactId = fromDb.ContactId,
                City = fromDb.City,
                FName = fromDb.FName,
                LName = fromDb.LName,
                HPhone = fromDb.HPhone,
                MPhone = fromDb.MPhone,
                OPhone = fromDb.OPhone,
                State = fromDb.State,
                Street = fromDb.Street,
                Zip = fromDb.Zip
            };
        }

        private IVehicle ConvertVehicleToViewModel(Vehicle fromDb)
        {
            return new VehicleViewModel()
            {
                VehicleId = fromDb.VehicleId,
                Color = fromDb.Color,
                Make = fromDb.Make,
                Model = fromDb.Model,
                Year = fromDb.Year
            };
        }
    }
}
