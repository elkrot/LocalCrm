using System.Data.Entity;
using LocalCrm.Model;
using LocalCrm.DataAccess.Mapping;
using LocalCrm.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;

namespace LocalCrm.DataAccess
{//DropCreateDatabaseAlways
    class MyContextInitializer : CreateDatabaseIfNotExists<LocalCrmContext>
    {
        protected override void Seed(LocalCrmContext db)
        {

          /*  var customer = new Customer() { FirstName = "FirstName", MiddleName = "MiddleName", LastName = "Customer" };
            db.Customers.Add(customer);
            var salesPerson = new SalesPerson() { FirstName = "FirstName", MiddleName = "MiddleName", LastName = "SalesPerson" };
            db.SalesPersons.Add(salesPerson);
            TransportCompany tk = new TransportCompany() { TransportCompanyName = "Новая почта" };
            db.TransportCompanies.Add(tk);
            City c = new City() { CityName = "Комсомольск" };
            db.Cities.Add(c);
           // db.SaveChanges();
            SalesOrderHeader so = new SalesOrderHeader() {
                OrderNo = "a1",OrderDate = DateTime.Today,OrderTotal = 100M,City = c,Customer = customer,SalesPerson = salesPerson};
            SalesOrderHeader so2 = new SalesOrderHeader()
            {
                OrderNo = "a2",
                OrderDate = DateTime.Today,
                OrderTotal = 130M,
                City = c,
                Customer = customer,
                SalesPerson = salesPerson
            };
            SalesOrderHeader so3 = new SalesOrderHeader()
            {
                OrderNo = "a3",
                OrderDate = DateTime.Today,
                OrderTotal = 150M,
                City = c,
                Customer = customer,
                SalesPerson = salesPerson
            };
            db.SalesOrderHeaders.AddRange(new List<SalesOrderHeader>() { so,so3,so2});
            db.SaveChanges();
            */



        }
    }
    public partial class LocalCrmContext : DbContext
    {
        static LocalCrmContext()
        {
            Database.SetInitializer<LocalCrmContext>(new MyContextInitializer());
        }

        public LocalCrmContext()
            : base("Name=LocalCrmContext")
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<TransportCompany> TransportCompanies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new SalesOrderHeaderMap());
            modelBuilder.Configurations.Add(new TransportCompanyMap());
        }

        public override int SaveChanges()
        {

            try
            {
return base.SaveChanges();
            }
            catch (System.Exception)
            {

                return -1;
            }

            
        }


        public MethodResult<int> SafeSaveChanges() {
            var result = new MethodResult<int>(-1);
            int recCount = 0;
            try
            {
                recCount = base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                result.AddErrorMessage(string.Format("Ошибка валидации {0}",ex.Message));
            }
            catch (DbUpdateException ex)
            {
                string innerEx = ex.InnerException == null?"" : ex.InnerException.Message;
                string innerEx2 = ex.InnerException.InnerException == null ? "" : ex.InnerException.InnerException.Message;

                result.AddErrorMessage(string.Format("Ошибка сохранения в БД {0} {1} {2}", ex.Message,innerEx, innerEx2));
            }
            catch (System.Exception ex)
            {
                result.AddErrorMessage(ex.GetType().Name.ToString());
            }
            return result;


        }


    }
}
