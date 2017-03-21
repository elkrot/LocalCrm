using LocalCrm.Infrastructure;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataProvider
{
    public interface ITransportCompanyDataProvider
    {
        TransportCompany GetTransportCompanyById(int id);

        MethodResult<int> SaveTransportCompany(TransportCompany transportCompany);

        MethodResult<int> DeleteTransportCompany(int id);
    }
}
