using LocalCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.ViewModel
{
    public class ConditionViewModel : Observable
    {
       

        public DateTime EndDate {
            get { return ConfigManager.getInstance().OrdersEndDate; }
            set { ConfigManager.getInstance().OrdersEndDate = value; }

           
        }

        public DateTime StartDate
        {
 get { return ConfigManager.getInstance().OrdersStartDate; }
            set { ConfigManager.getInstance().OrdersStartDate = value; }
        }

        public void Load()
        {
            if (StartDate == null || StartDate == DateTime.MinValue)
            {
                StartDate = DateTime.Today.AddMonths(-1);
                EndDate = DateTime.Today.AddDays(1);
            }
        }
    }
}
