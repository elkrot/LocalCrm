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
    public class SalesPersonDataProvider : ISalesPersonDataProvider
    {
        private readonly Func<IDataService> _dataServiceCreator;
        public SalesPersonDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }
        public MethodResult<int> DeleteSalesPerson(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.DeleteSalesPerson(id);
            }
        }

        public SalesPerson GetSalesPersonById(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetSalesPersonById(id);
            }
        }

        public MethodResult<int> SaveSalesPerson(SalesPerson salesPerson)
        {
            using (var dataService = _dataServiceCreator())
            {
               return dataService.SaveSalesPerson(salesPerson);
            }
        }
    }
}
