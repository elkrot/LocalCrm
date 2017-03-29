using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.ViewModel
{
    public class ConditionViewModel : Observable
    {
        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

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
