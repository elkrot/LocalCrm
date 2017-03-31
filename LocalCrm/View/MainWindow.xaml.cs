
using LocalCrm.DataAccess;
using LocalCrm.Model;
using LocalCrm.View;
using LocalCrm.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalCrm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _salesOrderViewModel;
        private ReferencesViewModel _referencesViewModel;
        public MainWindow(MainViewModel viewModel, ReferencesViewModel refViewModel)
        {
            InitializeComponent();
            _salesOrderViewModel = viewModel;
            _referencesViewModel = refViewModel;
            _salesOrderViewModel.ConditionViewModel.Load();
            DataContext = _salesOrderViewModel;
        }

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new SalesOrderPage(_salesOrderViewModel);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new SalesOrderPage(_salesOrderViewModel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _referencesViewModel.Load();
            Main.Content = new ReferencesPage(_referencesViewModel);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new ReportsPage();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _salesOrderViewModel.Load();
        }
    }
}
