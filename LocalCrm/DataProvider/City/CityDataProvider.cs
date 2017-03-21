using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalCrm.Model;
using LocalCrm.DataAccess;
using LocalCrm.Infrastructure;

namespace LocalCrm.DataProvider
{
    class CityDataProvider : ICityDataProvider
    {
        private readonly Func<IDataService> _dataServiceCreator;
        public CityDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public MethodResult<int> DeleteCity(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.DeleteCity(id);
            }
        }

        public City GetCityById(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetCityById(id);
            }
        }

        public MethodResult<int> SaveCity(City transportCompany)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.SaveCity(transportCompany);
            }
        }
    }
}
