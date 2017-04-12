using System;
using System.Collections.Generic;
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
        public ReportConditionForm()
        {
            InitializeComponent();
        }
    }

    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }
}
