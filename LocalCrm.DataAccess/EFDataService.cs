using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalCrm.Model;
using LocalCrm.DataAccess;
using System.Data.Entity;
using LocalCrm.Infrastructure;
using System.Linq.Expressions;

namespace LocalCrm.DataAccess
{
    public class EFDataService:IDataService
    {
      

        public void Dispose()
        {
            // Usually Service-Proxies are disposable. This method is added as demo-purpose
            // to show how to use an IDisposable in the client with a Func<T>. =>  Look for example at the FriendDataProvider-class
        }
        #region GetAll
        public IEnumerable<Status> GetAllStatuses()
        {
            yield return new Status { Id = 1, Name = "Отправлен" };
            yield return new Status { Id = 2, Name = "Получен клиентом" };
            yield return new Status { Id = 3, Name = "Выполнен" };
            yield return new Status { Id = 3, Name = "Новый" };

        }
        public IEnumerable<City> GetAllCities()
        {

            using (var ds = new LocalCrmContext())
            {
                return ds.Cities.ToList();
            }
        }

        public IEnumerable<Person> GetAllPersons()
        {

            using (var ds = new LocalCrmContext())
            {
                return ds.People.ToList();
            }
        }


        public IEnumerable<SalesPerson> GetAllSalesPersons()
        {

            using (var ds = new LocalCrmContext())
            {
                return ds.SalesPersons.ToList();
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {

            using (var ds = new LocalCrmContext())
            {
                return ds.Customers.ToList();
            }
        }

        public IEnumerable<SalesOrderHeader> GetAllSalesOrders()
        {

            using (var ds = new LocalCrmContext()) {
             
                return ds.SalesOrderHeaders.ToList();
            }
            
        }

        public IEnumerable<TransportCompany> GetAllTransportCompanies()
        {
            using (var ds = new LocalCrmContext())
            {
                return ds.TransportCompanies.ToList();
            }
        }
        #endregion

        #region SalesOrderHeader
        public MethodResult<int> DeleteSalesOrderHeader(int SalesOrderHeaderId)
        {

            using (var ds = new LocalCrmContext())
            {
                var soh = ds.SalesOrderHeaders.Find(SalesOrderHeaderId);
                ds.SalesOrderHeaders.Remove(soh);
                return ds.SafeSaveChanges();
            }
        }
        public SalesOrderHeader GetSalesOrderHeaderById(int SalesOrderHeaderId)
        {
            using (var ds = new LocalCrmContext())
            {
                return ds.SalesOrderHeaders.Find(SalesOrderHeaderId);
            }
        }

        public MethodResult<int> SaveSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            using (var ds = new LocalCrmContext())
            {
                if (salesOrderHeader.SalesOrderId == 0)
                {
                    ds.SalesOrderHeaders.Add(salesOrderHeader);
                }
                else {
                    ds.Entry(salesOrderHeader).State = EntityState.Modified;
                }
               
                return ds.SafeSaveChanges();
            }
        }

        public IEnumerable<SalesOrderHeader> GetSalesOrdersByPeriod(DateTime dtFirst, DateTime dtLast)
        {

            using (var ds = new LocalCrmContext())
            {

                return ds.SalesOrderHeaders
                    .Include("City")
                    .Include("TransportCompany")
                    .Include("Customer")
                    .Include("SalesPerson")
                    .Where(x => x.OrderDate >= dtFirst && x.OrderDate <= dtLast).ToList();
            }

        }
        #endregion

        #region SalesPerson
        public MethodResult<int> DeleteSalesPerson(int SalesPersonId)
        {

            using (var ds = new LocalCrmContext())
            {
                var soh = ds.SalesPersons.Find(SalesPersonId);
                ds.SalesPersons.Remove(soh);
               return ds.SafeSaveChanges();
            }
        }
        public SalesPerson GetSalesPersonById(int personId)
        {
            using (var ds = new LocalCrmContext())
            {
                return ds.SalesPersons.Find(personId);
            }
        }

        public MethodResult<int> SaveSalesPerson(SalesPerson salesPerson)
        {
            using (var ds = new LocalCrmContext())
            {
                if (salesPerson.PersonId == 0)
                {
                    ds.SalesPersons.Add(salesPerson);
                }
                else
                {
                    ds.Entry(salesPerson).State = EntityState.Modified;
                }

                return ds.SafeSaveChanges();
            }
        }


        #endregion


        #region Customer
        public MethodResult<int> DeleteCustomer(int CustomerId)
        {
            using (var ds = new LocalCrmContext())
            {
                var soh = ds.Customers.Find(CustomerId);
                ds.Customers.Remove(soh);
                return ds.SafeSaveChanges();
            }
        }

        public Customer GetCustomerById(int personId)
        {
            using (var ds = new LocalCrmContext())
            {
                return ds.Customers.Find(personId);
            }
        }

        public MethodResult<int> SaveCustomer(Customer customer)
        {
            using (var ds = new LocalCrmContext())
            {
                if (customer.PersonId == 0)
                {
                    ds.Customers.Add(customer);
                }
                else
                {
                    ds.Entry(customer).State = EntityState.Modified;
                }

               return ds.SafeSaveChanges();
            }
        }
        #endregion

        #region TransportCompany
        public MethodResult<int> DeleteTransportCompany(int TransportCompanyId)
        {
            using (var ds = new LocalCrmContext())
            {
                var soh = ds.TransportCompanies.Find(TransportCompanyId);
                ds.TransportCompanies.Remove(soh);
                return ds.SafeSaveChanges();
            }
        }

        public TransportCompany GetTransportCompanyById(int transportCompanyId)
        {
            using (var ds = new LocalCrmContext())
            {
                return ds.TransportCompanies.Find(transportCompanyId);
            }
        }

        public MethodResult<int> SaveTransportCompany(TransportCompany transportCompany)
        {
            using (var ds = new LocalCrmContext())
            {
                if (transportCompany.TransportCompanyId == 0)
                {
                    ds.TransportCompanies.Add(transportCompany);
                }
                else
                {
                    ds.Entry(transportCompany).State = EntityState.Modified;
                }

                return ds.SafeSaveChanges();
            }
        }
        #endregion


        #region City
        public MethodResult<int> DeleteCity(int CityId)
        {
            using (var ds = new LocalCrmContext())
            {
                var soh = ds.Cities.Find(CityId);
                ds.Cities.Remove(soh);
                return ds.SafeSaveChanges();
            }
        }

        public City GetCityById(int cityId)
        {
            using (var ds = new LocalCrmContext())
            {
                return ds.Cities.Find(cityId);
            }
        }

        public MethodResult<int> SaveCity(City city)
        {
            
                using (var ds = new LocalCrmContext())
            {
                if (city.CityId == 0)
                {
                    ds.Cities.Add(city);
                }
                else
                {
                    ds.Entry(city).State = EntityState.Modified;
                }

                return ds.SafeSaveChanges();
            }
        }

        public IEnumerable<SalesOrderHeader> GetSalesOrdersByCondition(Expression<Func<SalesOrderHeader, bool>> where, Expression<Func<SalesOrderHeader, object>> orderby)
        {
            using (var ds = new LocalCrmContext())
            {

                return ds.SalesOrderHeaders
                    .Include("City")
                    .Include("TransportCompany")
                    .Include("Customer")
                    .Include("SalesPerson")
                    .Where(where)
                    .OrderBy(x=>x.OrderDate).ThenBy(orderby)
                    .ToList();
            }
        }
        #endregion





    }
}
