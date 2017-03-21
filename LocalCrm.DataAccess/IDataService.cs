using LocalCrm.Infrastructure;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataAccess
{
    public interface IDataService : IDisposable
    {
        IEnumerable<City> GetAllCities();
        IEnumerable<Status> GetAllStatuses();
        IEnumerable<Person> GetAllPersons();
        IEnumerable<SalesPerson> GetAllSalesPersons();
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<TransportCompany> GetAllTransportCompanies();
        IEnumerable<SalesOrderHeader> GetAllSalesOrders();
        SalesOrderHeader GetSalesOrderHeaderById(int friendId);

        MethodResult<int> SaveSalesOrderHeader(SalesOrderHeader salesOrderHeader);

        MethodResult<int> DeleteSalesOrderHeader(int SalesOrderHeaderId);
        /*  Sales Person */
        MethodResult<int> DeleteSalesPerson(int salesPersonId);
        SalesPerson GetSalesPersonById(int salesPersonId);
        MethodResult<int> SaveSalesPerson(SalesPerson salesPerson);
        /*  Customer */
        MethodResult<int> DeleteCustomer(int customerId);
        Customer GetCustomerById(int customerId);
        MethodResult<int> SaveCustomer(Customer customer);
        /*TransportCompany*/
        MethodResult<int> DeleteTransportCompany(int transportCompanyId);
        TransportCompany GetTransportCompanyById(int transportCompanyId);
        MethodResult<int> SaveTransportCompany(TransportCompany transportCompany);
        /*City*/
        MethodResult<int> DeleteCity(int cityId);
        City GetCityById(int cityId);
        MethodResult<int> SaveCity(City city);
    }
}
