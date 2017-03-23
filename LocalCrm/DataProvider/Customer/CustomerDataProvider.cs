using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalCrm.Model;
using LocalCrm.DataAccess;
using LocalCrm.Infrastructure;
using System.Linq.Expressions;

namespace LocalCrm.DataProvider
{
    public class CustomerDataProvider : ICustomerDataProvider
    {
        private readonly Func<IDataService> _dataServiceCreator;
        public CustomerDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }
        public MethodResult<int> DeleteCustomer(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.DeleteCustomer(id);
            }
        }

        public Customer GetCustomerByCondition(Expression<Func<Customer, bool>> where)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetCustomerByCondition(where);
            }
        }

        public Customer GetCustomerById(int id)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetCustomerById(id);
            }
        }

        public MethodResult<int> SaveCustomer(Customer customer)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.SaveCustomer(customer);
            }
        }
    }
}
