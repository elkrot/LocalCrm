﻿using LocalCrm.DataAccess;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LocalCrm.DataProvider.Lookups
{
    public class SalesPersonLookupProvider : ILookupProvider<SalesPerson>
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public SalesPersonLookupProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public IEnumerable<LookupItem> GetLookup()
        {
            using (var service = _dataServiceCreator())
            {
                return service.GetAllSalesPersons()
                        .Select(c => new LookupItem
                        {
                            Id = c.PersonId,
                            DisplayValue = c.LastName.TrimEnd() +c.FirstName.First() + "." + c.MiddleName.First() + "."
                        })
                        .OrderBy(l => l.DisplayValue)
                        .ToList();
            }
        }

        public IEnumerable<LookupItem> GetLookupWithCondition(Expression<Func<SalesPerson, bool>> where, Expression<Func<SalesPerson, object>> orderby)
        {
            throw new NotImplementedException();
        }
    }


    public class CustomerLookupProvider : ILookupProvider<Customer>
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public CustomerLookupProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public IEnumerable<LookupItem> GetLookup()
        {
            using (var service = _dataServiceCreator())
            {
                return service.GetAllCustomers()
                        .Select(c => new LookupItem
                        {
                            Id = c.PersonId,
                            DisplayValue = c.LastName.TrimEnd()+" " + c.FirstName.First()+"." 
                            + c.MiddleName.First() + "."
                        })
                        
                        .OrderBy(l => l.DisplayValue)
                        .ToList();
            }
        }

        public IEnumerable<LookupItem> GetLookupWithCondition(Expression<Func<Customer, bool>> where, Expression<Func<Customer, object>> orderby)
        {
            throw new NotImplementedException();
        }
    }


}
