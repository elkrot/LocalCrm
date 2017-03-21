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


    public class SalesOrderLookupProvider : ILookupProvider<SalesOrderHeader>
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public SalesOrderLookupProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public IEnumerable<LookupItem> GetLookup()
        {
            using (var service = _dataServiceCreator())
            {
                return service.GetAllSalesOrders()
                        .Select(f => new LookupItem
                        {
                            Id = f.SalesOrderId,
                            DisplayValue = string.Format("{0} от {1:d}", f.OrderNo, f.OrderDate)
                        })
                        .OrderBy(l => l.DisplayValue)
                        .ToList();
            }
        }

        public IEnumerable<LookupItem> GetLookupWithCondition(Expression<Func<SalesOrderHeader, bool>> where, Expression<Func<SalesOrderHeader, object>> orderby)
        {
            using (var service = _dataServiceCreator())
            {
                return service.GetSalesOrdersByCondition(where,orderby)
                        .Select(f => new LookupItem
                        {
                            Id = f.SalesOrderId,
                            DisplayValue = string.Format("{0} от {1:d}", f.OrderNo, f.OrderDate)
                        })
                        .ToList();
            }
        }
    }
}
