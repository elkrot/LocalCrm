using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LocalCrm.DataProvider.Lookups
{
  public interface ILookupProvider<T>
  {
    IEnumerable<LookupItem> GetLookup();
        IEnumerable<LookupItem> GetLookupWithCondition(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby);
    }
}
