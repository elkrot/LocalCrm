using LocalCrm.DataAccess;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

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

        public IEnumerable<LookupItem> GetLookupWithCondition(Expression<Func<TransportCompany, bool>> where, Expression<Func<TransportCompany, object>> orderby)
        {
            throw new NotImplementedException();
        }
    }
}
