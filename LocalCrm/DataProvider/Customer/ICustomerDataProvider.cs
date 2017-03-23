using LocalCrm.Infrastructure;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataProvider
{
    public interface ICustomerDataProvider
    {

        Customer GetCustomerById(int id);

        MethodResult<int> SaveCustomer(Customer friend);

        MethodResult<int> DeleteCustomer(int id);

        Customer GetCustomerByCondition(Expression<Func<Customer, bool>> where);
    }
}
