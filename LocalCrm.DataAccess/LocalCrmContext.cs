using System.Data.Entity;
using LocalCrm.Model;
using LocalCrm.DataAccess.Mapping;
using LocalCrm.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace LocalCrm.DataAccess
{//DropCreateDatabaseAlways
    class MyContextInitializer : CreateDatabaseIfNotExists<LocalCrmContext>
    {
        protected override void Seed(LocalCrmContext db)
        {
           
               var customer = new Customer() { FirstName = "FirstName", MiddleName = "MiddleName", LastName = "Customer" };
                 db.Customers.Add(customer);
                 var salesPerson = new SalesPerson() { FirstName = "FirstName", MiddleName = "MiddleName", LastName = "SalesPerson" };
                 db.SalesPersons.Add(salesPerson);
            TransportCompany tk = new TransportCompany() { TransportCompanyName = "Новая почта" };
            db.TransportCompanies.Add(tk);
            City c = new City() { CityName = "Комсосольск" };
            db.Cities.Add(c);
                 db.SaveChanges();
           
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
                result.AddErrorMessage("Ошибка валидации");
            }
            catch (DbUpdateException )
            {
                
                result.AddErrorMessage("Ошибка сохранения в БД");
            }
            catch (System.Exception ex)
            {
                result.AddErrorMessage(ex.GetType().Name.ToString());
            }
            return result;


        }


    }
}
