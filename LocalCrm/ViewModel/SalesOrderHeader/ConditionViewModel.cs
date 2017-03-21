using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.ViewModel
{
    public class ConditionViewModel :  Observable
    {
        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public void Load()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.MaxValue;
        }
    }
}
