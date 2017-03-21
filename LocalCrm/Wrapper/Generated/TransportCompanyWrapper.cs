using System;   
using System.Linq;
using LocalCrm.Model;

namespace LocalCrm.Wrapper
{
  public partial class TransportCompanyWrapper : ModelWrapper<TransportCompany>
  {
    public TransportCompanyWrapper(TransportCompany model) : base(model)
    {
    }

    public System.Int32 TransportCompanyId
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 TransportCompanyIdOriginalValue => GetOriginalValue<System.Int32>(nameof(TransportCompanyId));

    public bool TransportCompanyIdIsChanged => GetIsChanged(nameof(TransportCompanyId));

    public System.String TransportCompanyName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String TransportCompanyNameOriginalValue => GetOriginalValue<System.String>(nameof(TransportCompanyName));

    public bool TransportCompanyNameIsChanged => GetIsChanged(nameof(TransportCompanyName));
  }
}
