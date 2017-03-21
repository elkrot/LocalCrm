using System;
using System.Collections.Generic;

namespace LocalCrm.Model
{
    public partial class TransportCompany
    {
        public TransportCompany()
        {
            this.SalesOrderHeaders = new List<SalesOrderHeader>();
        }

        public int TransportCompanyId { get; set; }
        public string TransportCompanyName { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
