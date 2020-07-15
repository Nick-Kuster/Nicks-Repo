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
    public class VehiclesReader : IVehiclesReader
    {
        private readonly Context _dbContext;

        public VehiclesReader(Context context)
        {
            _dbContext = context;
        }

        public IVehicle Read(Guid id)
        {
            Vehicle fromDb = _dbContext.vehicle.FirstOrDefault(x => x.VehicleId == id) ?? throw new Exception("Vehicle not found");

            IVehicle viewModel = ConvertToViewModel(fromDb);

            return viewModel;
        }

        public IEnumerable<IVehicle> ReadAll()
        {
            var fromDb = _dbContext.vehicle.ToList();

            List<IVehicle> viewModelList = new List<IVehicle>();

            foreach (var record in fromDb)
            {
                viewModelList.Add(ConvertToViewModel(record));
            }

            return viewModelList;
        }

        private IVehicle ConvertToViewModel(Vehicle fromDb)
        {
            return new VehicleViewModel()
            {
                VehicleId = fromDb.VehicleId,
                Color = fromDb.Color,
                Make = fromDb.Make,
                Model = fromDb.Model
            };
        }
    }
}
