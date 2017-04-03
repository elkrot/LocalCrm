using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LocalCrm.Infrastructure;

namespace LocalCrm.Reports
{
    public interface IReportCondition
    {
        /* get { return ConfigManager.getInstance().OrdersStartDate; }
            set { ConfigManager.getInstance().OrdersStartDate = value; }*/
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

        string ReportName { get; set; }
        object DataSet { get; set; }
        string DataSetName { get; set; }
    }
    public class ReportCondition : IReportCondition,INotifyPropertyChanged
    {
        public ReportCondition()
        {
            if (_startDate == DateTime.MinValue) { 
                _startDate = DateTime.Today.Date;
            _endDate = DateTime.Today.Date;
            }
         
        }
        public DateTime _endDate {
            get { return ConfigManager.getInstance().ReportsEndDate; }
            set { ConfigManager.getInstance().ReportsEndDate = value; }
        }

        public DateTime _startDate {
            get { return ConfigManager.getInstance().ReportsStartDate; }
            set { ConfigManager.getInstance().ReportsStartDate = value; }
        }

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
