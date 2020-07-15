using ClaimsRUs.Data.Abstractions.Models;
using ClaimsRUs.Data.Abstractions.Readers;
using ClaimsRUs.Entity;
using ClaimsRUs.Entity.Models;
using ClaimsRUs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimsRUs.Data.Readers
{
    public class ContactsReader : IContactsReader
    {
        private readonly Context _dbContext;

        public ContactsReader(Context context)
        {
            _dbContext = context;
        }

        public IContact Read(Guid id)
        {
            Contact fromDb = _dbContext.contact.FirstOrDefault(x => x.ContactId == id) ?? throw new Exception("Contact not found");

            IContact viewModel = ConvertToViewModel(fromDb);

            return viewModel;
        }


        public IEnumerable<IContact> ReadAll()
        {
            var fromDb = _dbContext.contact.ToList();

            List<IContact> viewModelList = new List<IContact>();

            foreach (var record in fromDb)
            {
                viewModelList.Add(ConvertToViewModel(record));
            }

            return viewModelList;
        }

        private IContact ConvertToViewModel(Contact fromDb)
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

    }
}
