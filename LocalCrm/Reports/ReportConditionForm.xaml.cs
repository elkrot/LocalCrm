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
        ObservableCollection<CheckBoxListViewItem> lst;
        public ReportConditionForm()
        {
            InitializeComponent();
            lst = new ObservableCollection<CheckBoxListViewItem>() {
                new CheckBoxListViewItem( "tssss1", true )
            ,new CheckBoxListViewItem( "tssssss2", false )};

            lBStatus.ItemsSource = lst;
            lBTransportCompany.ItemsSource = lst;
        }
    }

    public class Test
    {
        public string Title { get; set; }
        public bool IsSelected { get; set; }

    }

    public class Phone
    {

        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }

    }

    public class CheckBoxListViewItem : INotifyPropertyChanged
    { private bool isChecked;
        private string text;
        public bool IsChecked { get { return isChecked; } set { if (isChecked == value) return;
                isChecked = value;
                RaisePropertyChanged("IsChecked"); } }

        public String Text { get { return text; } set { if (text == value) return;
                text = value; RaisePropertyChanged("Text"); } }

        public CheckBoxListViewItem(string t, bool c) { this.Text = t; this.IsChecked = c; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propName)
        { PropertyChangedEventHandler eh = PropertyChanged; if (eh != null)
            { eh(this, new PropertyChangedEventArgs(propName)); } } }
}
