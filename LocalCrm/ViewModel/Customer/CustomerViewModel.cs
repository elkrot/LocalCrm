using LocalCrm.Command;
using LocalCrm.Event;
using LocalCrm.View.Services;
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
     
public class CustomerViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private ICustomerEditViewModel _selectedCustomerEditViewModel;
        private Func<ICustomerEditViewModel> _customerEditViewModelCreator;

        #region Constructor
        public CustomerViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            ICustomerNavigationViewModel navigationViewModel,
            Func<ICustomerEditViewModel> customerEditViewModelCreator)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenCustomerEditViewEvent>().Subscribe(OnOpenCustomerTab);
            _eventAggregator.GetEvent<CustomerDeletedEvent>().Subscribe(OnCustomerDeleted);

            CustomerNavigationViewModel = navigationViewModel;
            _customerEditViewModelCreator = customerEditViewModelCreator;
            CustomerEditViewModels = new ObservableCollection<ICustomerEditViewModel>();
            CloseCustomerTabCommand = new DelegateCommand(OnCloseCustomerTabExecute);
            AddCustomerCommand = new DelegateCommand(OnAddCustomerExecute);
        }
        #endregion

        public void Load()
        {
            CustomerNavigationViewModel.Load();
        }

        #region OnClosing
        public void OnClosing(CancelEventArgs e)
        {
            if (CustomerEditViewModels.Any(f => f.Customer.IsChanged))
            {
                var result = _messageDialogService.ShowYesNoDialog("Close application?",
                  "You'll lose your changes if you close this application. Close it?",
                  MessageDialogResult.No);
                e.Cancel = result == MessageDialogResult.No;
            }
        }
        #endregion

        public ICommand CloseCustomerTabCommand { get; private set; }

        public ICommand AddCustomerCommand { get; set; }

        public ICustomerNavigationViewModel CustomerNavigationViewModel { get; private set; }

        // Those ViewModels represent the Tab-Pages in the UI
        public ObservableCollection<ICustomerEditViewModel> CustomerEditViewModels { get; private set; }

        #region SelectedCustomerEditViewModel
        public ICustomerEditViewModel SelectedCustomerEditViewModel
        {
            get { return _selectedCustomerEditViewModel; }
            set
            {
                _selectedCustomerEditViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool IsChanged => CustomerEditViewModels.Any(f => f.Customer.IsChanged);

        #region OnAddCustomerExecute
        private void OnAddCustomerExecute(object obj)
        {
            ICustomerEditViewModel customerEditVm = _customerEditViewModelCreator();
            CustomerEditViewModels.Add(customerEditVm);
            customerEditVm.Load();
            SelectedCustomerEditViewModel = customerEditVm;
        }
        #endregion

        #region OnOpenCustomerTab
        private void OnOpenCustomerTab(int customerId)
        {
            ICustomerEditViewModel customerEditVm =
              CustomerEditViewModels.SingleOrDefault(vm => vm.Customer.PersonId == customerId);
            if (customerEditVm == null)
            {
                customerEditVm = _customerEditViewModelCreator();
                CustomerEditViewModels.Add(customerEditVm);
                customerEditVm.Load(customerId);
            }
            SelectedCustomerEditViewModel = customerEditVm;
        }
        #endregion

        #region OnCloseCustomerTabExecute
        private void OnCloseCustomerTabExecute(object parameter)
        {
            var customerEditVmToClose = parameter as ICustomerEditViewModel;
            if (customerEditVmToClose != null)
            {
                if (customerEditVmToClose.Customer.IsChanged)
                {
                    var result = _messageDialogService.ShowYesNoDialog("Закрыть закладку?",
                      "Изменения будут утеряны. Закрыть ее?",
                      MessageDialogResult.No);
                    if (result == MessageDialogResult.No)
                    {
                        return;
                    }
                }
                CustomerEditViewModels.Remove(customerEditVmToClose);
            }
        }
        #endregion

        #region OnCustomerDeleted
        private void OnCustomerDeleted(int customerId)
        {
            ICustomerEditViewModel customerDetailVmToClose
              = CustomerEditViewModels.SingleOrDefault(f => f.Customer.PersonId == customerId);
            if (customerDetailVmToClose != null)
            {
                CustomerEditViewModels.Remove(customerDetailVmToClose);
            }
        }
        #endregion

    }
}
