using System;   
using System.Linq;
using LocalCrm.Model;

namespace LocalCrm.Wrapper
{
  public partial class CityWrapper : ModelWrapper<City>
  {
    public CityWrapper(City model) : base(model)
    {
    }

    public System.Int32 CityId
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 CityIdOriginalValue => GetOriginalValue<System.Int32>(nameof(CityId));

    public bool CityIdIsChanged => GetIsChanged(nameof(CityId));

    public System.String CityName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String CityNameOriginalValue => GetOriginalValue<System.String>(nameof(CityName));

    public bool CityNameIsChanged => GetIsChanged(nameof(CityName));
  }
}
