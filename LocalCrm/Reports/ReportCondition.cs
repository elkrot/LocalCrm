using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.Reports
{
    public interface IReportCondition
    {

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

        string ReportName { get; set; }
        object DataSet { get; set; }
        string DataSetName { get; set; }
    }
    public class ReportCondition : IReportCondition,INotifyPropertyChanged
    {
        public ReportCondition(DateTime startDate,DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }
        public DateTime _endDate { get; set; }

        public DateTime _startDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._startDate;
            }

            set
            {
                if (value != this._startDate)
                {
                    this._startDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this._endDate;
            }

            set
            {
                if (value != this._endDate)
                {
                    this._endDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ReportName { get; set; }

        public object DataSet { get; set; }
        public string DataSetName { get; set; }

    }
}
