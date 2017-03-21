using LocalCrm.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalCrm.View
{
    /// <summary>
    /// Interaction logic for SalesOrderPage.xaml
    /// </summary>
    public partial class SalesOrderPage : Page
    {
        public SalesOrderPage()
        {
            InitializeComponent();
        }


        private MainViewModel _viewModel;
        public SalesOrderPage(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
