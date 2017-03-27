using LocalCrm.DataProvider;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.Wrapper.Import
{
    public class SalesOrderWrapper
    {
        private SalesOrderDto Model;
        private readonly ICityDataProvider _cityDataProvider;
        private readonly ICustomerDataProvider _customerDataProvider;
        private readonly ITransportCompanyDataProvider _transportCompanyDataProvider;
        public SalesOrderWrapper(SalesOrderDto model
            , ICityDataProvider cityDataProvider
            , ICustomerDataProvider customerDataProvider
            , ITransportCompanyDataProvider transportCompanyDataProvider)
        {
            _cityDataProvider = cityDataProvider;
            _customerDataProvider = customerDataProvider;
            _transportCompanyDataProvider = transportCompanyDataProvider;

            Model = model;
        }

        public string OrderNo { get { return Model.OrderNo; } }

        public DateTime OrderDate { get { return Model.OrderDate; } }
        //     Сумма 
        public decimal OrderTotal { get { return Model.OrderTotal; } }
        //     Получатель 
        public Customer Customer
        {
            get
            {
                string[] fio = Model.CustomerName.Split(' ');
               Customer customer = _customerDataProvider.GetCustomerByCondition(
                    x=>x.LastName==fio[0] && x.FirstName==fio[1] && x.MiddleName==fio[2]);
                if (customer == null) {
                    customer = new Customer() { LastName = fio[0] , FirstName = fio[1],MiddleName = fio[2] };
                    _customerDataProvider.SaveCustomer(customer);
                }
                return customer;
            }
        }
        //     Условия доставки 
        public TransportCompany TransportCompany
        {
            get
            {
                TransportCompany transportCompany = _transportCompanyDataProvider.GetTransportCompanyByName(
                 Model.TransportCompanyName);
                if (transportCompany == null)
                {
                    transportCompany = new TransportCompany() {TransportCompanyName=Model.TransportCompanyName };
                    _transportCompanyDataProvider.SaveTransportCompany(transportCompany);
                }
                return transportCompany;
            }
        }
//     Город   
public City City
        {
            get
            {
                City city = _cityDataProvider.GetCityByName(
                 Model.CityName);
                if (city == null)
                {
                    city = new City() { CityName = Model.CityName };
                    _cityDataProvider.SaveCity(city);
                }
                return city;
            }
        }

    }
}
