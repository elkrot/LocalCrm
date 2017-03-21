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
    public class SalesOrderDataProvider : ISalesOrderDataProvider
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public SalesOrderDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }
        public MethodResult<int> DeleteSalesOrderHeader(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
               return dataService.DeleteSalesOrderHeader(id);
            }
        }

        public SalesOrderHeader GetSalesOrderHeaderById(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetSalesOrderHeaderById(id);
            }
        }

        public MethodResult<int> SaveSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.SaveSalesOrderHeader(salesOrderHeader);
            }
        }
    }
}
