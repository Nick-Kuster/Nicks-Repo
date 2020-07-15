using ClaimsRUs.Data.Abstractions.Models;
using System.Collections.Generic;

namespace ClaimsRUs.Data.Abstractions.Writers
{
    public interface IWriter<TViewModel> where TViewModel : IViewModel
    {
        void Write(TViewModel viewModel);

        void WriteAll(IEnumerable<TViewModel> viewModels);
    }
}
