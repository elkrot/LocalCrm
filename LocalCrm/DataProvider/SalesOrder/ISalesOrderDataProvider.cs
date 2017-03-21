using LocalCrm.Infrastructure;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataProvider
{
    public interface ISalesOrderDataProvider
    {
        SalesOrderHeader GetSalesOrderHeaderById(int id);

        MethodResult<int> SaveSalesOrderHeader(SalesOrderHeader friend);

        MethodResult<int> DeleteSalesOrderHeader(int id);
    }
}
