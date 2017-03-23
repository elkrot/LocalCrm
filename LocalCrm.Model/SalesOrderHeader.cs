using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalCrm.Model
{
    public partial class SalesOrderHeader
    {
        public int SalesOrderId { get; set; }

        public string OrderNo { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<decimal> OrderTotal { get; set; }
        public Nullable<int> SalesPersonId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<decimal> OrderPrice { get; set; }
        public Nullable<decimal> Prepayment { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> TransportCompanyId { get; set; }
        public Nullable<decimal> ShipingCost { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<decimal> ReceivedForDelivery { get; set; }
        public Nullable<decimal> ReceivedForOrder { get; set; }
        public Nullable<int> Status { get; set; }

        public string TrackNumber { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> OrderComplitDate { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }
        /* virtual */
        public virtual City City { get; set; }
        public virtual SalesPerson SalesPerson { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual TransportCompany TransportCompany { get; set; }/*  */

        /* not mapped */
        [NotMapped]
        public string CityName { get { return City==null?"":City.CityName; } }

        [NotMapped]
        public string SalesPersonName { get {
                return string.Format("{0} {1} {2}"
                    ,SalesPerson.LastName.Trim()
                    , SalesPerson.FirstName.Trim()
                    , SalesPerson.MiddleName.Trim()); } }

        [NotMapped]
        public string CustomerName { get { return string.Format("{0} {1} {2}"
                    , Customer.LastName.Trim()
                    , Customer.FirstName.Trim()
                    , Customer.MiddleName.Trim());
            } }

        [NotMapped]
        public string TransportCompanyName { get { return TransportCompany.TransportCompanyName; } }
       
        [NotMapped]
        public string StatusName
        { get
            {
                switch (Status)
                {
                    case 1:return "Отправлен";
                    case 2:return "Получен клиентом";
                    case 3:return "Выполнен";
                    case 4:return "Готов к отправке";
                    default:return "";
                }
            } }

    }
}
