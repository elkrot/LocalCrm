using LocalCrm.Infrastructure;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataProvider
{
    public interface ISalesPersonDataProvider
    {

        SalesPerson GetSalesPersonById(int id);

        MethodResult<int> SaveSalesPerson(SalesPerson friend);

        MethodResult<int> DeleteSalesPerson(int id);
    }
}
