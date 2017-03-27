using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.Model
{
    public class SalesOrderDto
    {
        //       № заказа 
        public DateTime OrderDate;
        //       № заказа 
        public string OrderNo;
        //     Сумма 
        public decimal OrderTotal;
        //     Получатель 
        public string CustomerName;
        //     Условия доставки 
        public string TransportCompanyName;
        //     Город   
        public string CityName;
        //     Телефон
        public string Phone;

    }
}
