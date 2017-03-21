using LocalCrm.DataAccess;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataProvider.Lookups
{
    public class StatusLookupProvider : ILookupProvider<Status>
    {
        private readonly Func<IDataService> _dataServiceCreator;
        public StatusLookupProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }
        public IEnumerable<LookupItem> GetLookup()
        {
            using (var service = _dataServiceCreator())
            {
                return service.GetAllStatuses()
                        .Select(c => new LookupItem
                        {
                            Id = c.Id,
                            DisplayValue = c.Name
                        })
                        .OrderBy(l => l.DisplayValue)
                        .ToList();
            }
        }
    }
}
