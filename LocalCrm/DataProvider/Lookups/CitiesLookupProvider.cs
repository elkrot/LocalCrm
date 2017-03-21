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
    class CitiesLookupProvider : ILookupProvider<City>
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public CitiesLookupProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public IEnumerable<LookupItem> GetLookup()
        {
            using (var service = _dataServiceCreator())
            {
                return service.GetAllCities()
                        .Select(c => new LookupItem
                        {
                            Id = c.CityId,
                            DisplayValue = c.CityName
                        })
                        .OrderBy(l => l.DisplayValue)
                        .ToList();
            }
        }

        public IEnumerable<LookupItem> GetLookupWithCondition(Expression<Func<City, bool>> where, Expression<Func<City, object>> orderby)
        {
            throw new NotImplementedException();
        }
    }
}
