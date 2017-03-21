using System.Collections.Generic;

namespace LocalCrm.DataProvider.Lookups
{
  public interface ILookupProvider<T>
  {
    IEnumerable<LookupItem> GetLookup();
  }
}
