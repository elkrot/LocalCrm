using LocalCrm.DataAccess;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataProvider.Lookups
{
    public class TransportCompanyLookupProvider : ILookupProvider<TransportCompany>
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public TransportCompanyLookupProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public IEnumerable<LookupItem> GetLookup()
        {
            using (var service = _dataServiceCreator())
            {
                return service.GetAllTransportCompanies()
                        .Select(c => new LookupItem
                        {
                            Id = c.TransportCompanyId,
                            DisplayValue = c.TransportCompanyName
                        })
                        .OrderBy(l => l.DisplayValue)
                        .ToList();
            }
        }
    }
}
