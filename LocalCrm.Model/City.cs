using System;
using System.Collections.Generic;

namespace LocalCrm.Model
{
    public partial class City
    {
        public City()
        {
            this.SalesOrderHeaders = new List<SalesOrderHeader>();
        }

        public int CityId { get; set; }
    
        public string CityName { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
