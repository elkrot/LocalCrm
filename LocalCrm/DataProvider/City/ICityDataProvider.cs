using LocalCrm.Infrastructure;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.DataProvider
{
    public interface ICityDataProvider
    {
        City GetCityById(int id);
        City GetCityByName(string name);

        MethodResult<int> SaveCity(City city);

        MethodResult<int> DeleteCity(int id);
    }
}
