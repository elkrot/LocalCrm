using System;
using System.Collections.Generic;

namespace LocalCrm.Model
{
    public abstract partial class Person
    {
        public Person()
        {
         //   this.SalesOrderHeaders = new List<SalesOrderHeader>();
          //  this.SalesOrderHeaders1 = new List<SalesOrderHeader>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string AdditionalContactInfo { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
     //   public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
      //  public virtual ICollection<SalesOrderHeader> SalesOrderHeaders1 { get; set; }
    }

    public partial class SalesPerson:Person
    {
        public SalesPerson()
        {
            this.SalesOrderHeaders = new List<SalesOrderHeader>();
        }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }

    public partial class Customer:Person
    {
        public Customer()
        {
            this.SalesOrderHeaders = new List<SalesOrderHeader>();
        }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
