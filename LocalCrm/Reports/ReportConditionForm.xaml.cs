using LocalCrm.DataAccess;
using LocalCrm.DataProvider.Lookups;
using LocalCrm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LocalCrm.Reports
{
    /// <summary>
    /// Interaction logic for ReportConditionForm.xaml
    /// </summary>
    public partial class ReportConditionForm : Window
    {
        public List<CheckBoxListViewItem> lstStatus;
        public List<CheckBoxListViewItem> lstTransportCompany;


        public ReportConditionForm()
        {
            InitializeComponent();

           
            using (var service = new EFDataService())
            {
                lstStatus = service.GetAllStatuses()
                        .Select(c => new CheckBoxListViewItem(c.Name, true, c.Id) { }
                        )
                        .OrderBy(l => l.Text)
                        .ToList();
            }

            using (var service = new EFDataService())
            {
                lstTransportCompany = service.GetAllTransportCompanies()
                        .Select(c => new CheckBoxListViewItem(c.TransportCompanyName, true, c.TransportCompanyId) { }
                        )
                        .OrderBy(l => l.Text)
                        .ToList();
            }




           

            lBStatus.ItemsSource = lstStatus;
            lBTransportCompany.ItemsSource = lstTransportCompany;
           


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }



    public class CheckBoxListViewItem : INotifyPropertyChanged
    {   private bool isChecked;
        private string text;
        private int id;
        public bool IsChecked { get { return isChecked; } set { if (isChecked == value) return;
                isChecked = value;
                RaisePropertyChanged("IsChecked"); } }


        public int Id
        {
            get { return id; }
            set
            {
                if (id == value) return;
                id = value;
                RaisePropertyChanged("Id");
            }
        }



        public String Text { get { return text; } set { if (text == value) return;
                text = value; RaisePropertyChanged("Text"); } }

        public CheckBoxListViewItem(string t, bool c,int i) { this.Text = t; this.IsChecked = c;this.Id = i; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propName)
        { PropertyChangedEventHandler eh = PropertyChanged; if (eh != null)
            { eh(this, new PropertyChangedEventArgs(propName)); } }


    }
}
