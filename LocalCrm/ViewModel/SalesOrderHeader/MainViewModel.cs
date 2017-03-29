using LocalCrm.Command;
using LocalCrm.DataProvider;
using LocalCrm.Event;
using LocalCrm.Infrastructure;
using LocalCrm.Model;
using LocalCrm.View.Services;
using LocalCrm.Wrapper.Import;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace LocalCrm.ViewModel
{
    public class MainViewModel : Observable
    {
        private readonly ISalesOrderDataProvider _salesOrderDataProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private ISalesOrderHeaderEditViewModel _selectedSalesOrderHeaderEditViewModel;
        private Func<ISalesOrderHeaderEditViewModel> _salesOrderHeaderEditViewModelCreator;
        public ConditionViewModel ConditionViewModel { get; private set; }

        ICityDataProvider _cityDataProvider;
        ICustomerDataProvider _customerDataProvider;
        ITransportCompanyDataProvider _transportCompanyDataProvider;


        #region Constructor
        public MainViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            INavigationViewModel navigationViewModel,
            Func<ISalesOrderHeaderEditViewModel> salesOrderHeaderEditViewModelCreator
            , ConditionViewModel conditionViewModel
            , ICityDataProvider cityDataProvider,
              ICustomerDataProvider customerDataProvider,
              ITransportCompanyDataProvider transportCompanyDataProvider,
              ISalesOrderDataProvider salesOrderDataProvider
            )
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenSalesOrderEditViewEvent>().Subscribe(OnOpenSalesOrderHeaderTab);
            _eventAggregator.GetEvent<SalesOrderDeletedEvent>().Subscribe(OnSalesOrderHeaderDeleted);

            NavigationViewModel = navigationViewModel;
            ConditionViewModel = conditionViewModel;
            NavigationViewModel.ConditionViewModel = ConditionViewModel;

            _salesOrderDataProvider = salesOrderDataProvider;

            _salesOrderHeaderEditViewModelCreator = salesOrderHeaderEditViewModelCreator;
            SalesOrderHeaderEditViewModels = new ObservableCollection<ISalesOrderHeaderEditViewModel>();
            CloseSalesOrderHeaderTabCommand = new DelegateCommand(OnCloseSalesOrderHeaderTabExecute);
            AddSalesOrderHeaderCommand = new DelegateCommand(OnAddSalesOrderHeaderExecute);
            ImportFromExcelCommand = new DelegateCommand(OnImportFromExcelExecute);
            ChangePeriodCommand = new DelegateCommand(OnChangePeriodExecute);
            _cityDataProvider = cityDataProvider;
            _customerDataProvider = customerDataProvider;
            _transportCompanyDataProvider = transportCompanyDataProvider;


        }

        private void OnChangePeriodExecute(object obj)
        {
            NavigationViewModel.Load();
        }

        private void OnImportFromExcelExecute(object obj)
        {
            var result = _messageDialogService.OpenFileDialog();
            if (result)
            {
                var importResult = FileApiUtilites.GetDataFromXlsx(_messageDialogService.FilePath);

                var salesOrders = FileApiUtilites.GetSalesOrderDto(importResult);


                StringBuilder sb = new StringBuilder();
                var sowList = new List<SalesOrderWrapper>();
                foreach (var item in salesOrders)
                {
                    var sow = new SalesOrderWrapper(item, _cityDataProvider, _customerDataProvider, _transportCompanyDataProvider);
                    sowList.Add(sow);
                }

                int i = 0;
                int z = 0;
                bool fire = false;
                foreach (var item in sowList)
                {

                    SalesOrderHeader so = new SalesOrderHeader()
                    { OrderNo = item.OrderNo
                    , OrderDate = item.OrderDate
                    , City = item.City
                    , Customer = item.Customer
                    , OrderTotal=item.OrderTotal
                    ,TransportCompany= item.TransportCompany};
                    var res = _salesOrderDataProvider.SaveSalesOrderHeader(so);
                    if (!res.Success)
                    {
                        z++;
                        fire = true;
                        foreach (var msg in res.Messages)
                        {
                            sb.AppendLine(string.Format("{0}", msg));
                        }

                    }
                    else
                    {
                        i++;
                    }
                    //sb.AppendLine(string.Format("{0} {1:d} {2}", , item.OrderDate,item.City.CityId));
                }

                if (fire)
                {


                    System.Windows.Forms.MessageBox.Show(string.Format("Ошибочных записей {0}.{1}", z, sb.ToString()));
                }

                System.Windows.Forms.MessageBox.Show(string.Format("Добавлено {0} записей.", i));
                Load();
            }


        }


        #endregion

        public void Load()
        {

            
            NavigationViewModel.Load();

        }

        #region OnClosing
        public void OnClosing(CancelEventArgs e)
        {
            if (SalesOrderHeaderEditViewModels.Any(f => f.SalesOrderHeader.IsChanged))
            {
                var result = _messageDialogService.ShowYesNoDialog("Close application?",
                  "You'll lose your changes if you close this application. Close it?",
                  MessageDialogResult.No);
                e.Cancel = result == MessageDialogResult.No;
            }
        }
        #endregion

        public ICommand CloseSalesOrderHeaderTabCommand { get; private set; }

        public ICommand AddSalesOrderHeaderCommand { get; set; }

        public ICommand ImportFromExcelCommand { get; set; }

        public ICommand ChangePeriodCommand { get; set; }

        public INavigationViewModel NavigationViewModel { get; private set; }



        // Those ViewModels represent the Tab-Pages in the UI
        public ObservableCollection<ISalesOrderHeaderEditViewModel> SalesOrderHeaderEditViewModels { get; private set; }

        #region SelectedSalesOrderHeaderEditViewModel
        public ISalesOrderHeaderEditViewModel SelectedSalesOrderHeaderEditViewModel
        {
            get { return _selectedSalesOrderHeaderEditViewModel; }
            set
            {
                _selectedSalesOrderHeaderEditViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool IsChanged => SalesOrderHeaderEditViewModels.Any(f => f.SalesOrderHeader.IsChanged);

        #region OnAddSalesOrderHeaderExecute        Добавление
        private void OnAddSalesOrderHeaderExecute(object obj)
        {
            ISalesOrderHeaderEditViewModel salesOrderHeaderEditVm = _salesOrderHeaderEditViewModelCreator();
            SalesOrderHeaderEditViewModels.Add(salesOrderHeaderEditVm);
            salesOrderHeaderEditVm.Load();
            SelectedSalesOrderHeaderEditViewModel = salesOrderHeaderEditVm;
        }
        #endregion

        #region OnOpenSalesOrderHeaderTab           Открытие
        private void OnOpenSalesOrderHeaderTab(int salesOrderHeaderId)
        {
            ISalesOrderHeaderEditViewModel salesOrderHeaderEditVm =
              SalesOrderHeaderEditViewModels.SingleOrDefault(vm => vm.SalesOrderHeader.SalesOrderId == salesOrderHeaderId);
            if (salesOrderHeaderEditVm == null)
            {
                salesOrderHeaderEditVm = _salesOrderHeaderEditViewModelCreator();
                SalesOrderHeaderEditViewModels.Add(salesOrderHeaderEditVm);
                salesOrderHeaderEditVm.Load(salesOrderHeaderId);
            }
            SelectedSalesOrderHeaderEditViewModel = salesOrderHeaderEditVm;
        }
        #endregion 

        #region OnCloseSalesOrderHeaderTabExecute   Закрытие
        private void OnCloseSalesOrderHeaderTabExecute(object parameter)
        {
            var salesOrderHeaderEditVmToClose = parameter as ISalesOrderHeaderEditViewModel;
            if (salesOrderHeaderEditVmToClose != null)
            {
                if (salesOrderHeaderEditVmToClose.SalesOrderHeader.IsChanged)
                {
                    var result = _messageDialogService.ShowYesNoDialog("Закрыть закладку?",
                      "Изменения будут утеряны. Закрыть ее?",
                      MessageDialogResult.No);
                    if (result == MessageDialogResult.No)
                    {
                        return;
                    }
                }
                SalesOrderHeaderEditViewModels.Remove(salesOrderHeaderEditVmToClose);
            }
        }
        #endregion

        #region OnSalesOrderHeaderDeleted           Удаление
        private void OnSalesOrderHeaderDeleted(int salesOrderHeaderId)
        {
            ISalesOrderHeaderEditViewModel salesOrderHeaderDetailVmToClose
              = SalesOrderHeaderEditViewModels.SingleOrDefault(f => f.SalesOrderHeader.SalesOrderId == salesOrderHeaderId);
            if (salesOrderHeaderDetailVmToClose != null)
            {
                SalesOrderHeaderEditViewModels.Remove(salesOrderHeaderDetailVmToClose);
            }
        }
        #endregion

    }
}
