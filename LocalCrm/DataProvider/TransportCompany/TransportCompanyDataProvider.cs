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
    class TransportCompanyDataProvider : ITransportCompanyDataProvider
    {
        private readonly Func<IDataService> _dataServiceCreator;
        public TransportCompanyDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public MethodResult<int> DeleteTransportCompany(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
               return dataService.DeleteTransportCompany(id);
            }
        }

        public TransportCompany GetTransportCompanyById(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetTransportCompanyById(id);
            }
        }

        public TransportCompany GetTransportCompanyByName(string name)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetTransportCompanyByCondition(x=>x.TransportCompanyName==name);
            }
        }

        public MethodResult<int> SaveTransportCompany(TransportCompany transportCompany)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.SaveTransportCompany(transportCompany);
            }
        }
    }
}
