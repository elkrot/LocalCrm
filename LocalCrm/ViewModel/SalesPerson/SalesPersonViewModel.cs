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
     
public class SalesPersonViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private ISalesPersonEditViewModel _selectedSalesPersonEditViewModel;
        private Func<ISalesPersonEditViewModel> _salesPersonEditViewModelCreator;

        #region Constructor
        public SalesPersonViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            ISalesPesonNavigationViewModel navigationViewModel,
            Func<ISalesPersonEditViewModel> salesPersonEditViewModelCreator)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenSalesPersonEditViewEvent>().Subscribe(OnOpenSalesPersonTab);
            _eventAggregator.GetEvent<SalesPersonDeletedEvent>().Subscribe(OnSalesPersonDeleted);

            SalesPersonNavigationViewModel = navigationViewModel;
            _salesPersonEditViewModelCreator = salesPersonEditViewModelCreator;
            SalesPersonEditViewModels = new ObservableCollection<ISalesPersonEditViewModel>();
            CloseSalesPersonTabCommand = new DelegateCommand(OnCloseSalesPersonTabExecute);
            AddSalesPersonCommand = new DelegateCommand(OnAddSalesPersonExecute);
        }
        #endregion

        public void Load()
        {
            SalesPersonNavigationViewModel.Load();
        }

        #region OnClosing
        public void OnClosing(CancelEventArgs e)
        {
            if (SalesPersonEditViewModels.Any(f => f.SalesPerson.IsChanged))
            {
                var result = _messageDialogService.ShowYesNoDialog("Close application?",
                  "You'll lose your changes if you close this application. Close it?",
                  MessageDialogResult.No);
                e.Cancel = result == MessageDialogResult.No;
            }
        }
        #endregion

        public ICommand CloseSalesPersonTabCommand { get; private set; }

        public ICommand AddSalesPersonCommand { get; set; }

        public ISalesPesonNavigationViewModel SalesPersonNavigationViewModel { get; private set; }

        // Those ViewModels represent the Tab-Pages in the UI
        public ObservableCollection<ISalesPersonEditViewModel> SalesPersonEditViewModels { get; private set; }

        #region SelectedSalesPersonEditViewModel
        public ISalesPersonEditViewModel SelectedSalesPersonEditViewModel
        {
            get { return _selectedSalesPersonEditViewModel; }
            set
            {
                _selectedSalesPersonEditViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool IsChanged => SalesPersonEditViewModels.Any(f => f.SalesPerson.IsChanged);

        #region OnAddSalesPersonExecute
        private void OnAddSalesPersonExecute(object obj)
        {
            ISalesPersonEditViewModel salesPersonEditVm = _salesPersonEditViewModelCreator();
            SalesPersonEditViewModels.Add(salesPersonEditVm);
            salesPersonEditVm.Load();
            SelectedSalesPersonEditViewModel = salesPersonEditVm;
        }
        #endregion

        #region OnOpenSalesPersonTab
        private void OnOpenSalesPersonTab(int salesPersonId)
        {
            ISalesPersonEditViewModel salesPersonEditVm =
              SalesPersonEditViewModels.SingleOrDefault(vm => vm.SalesPerson.PersonId == salesPersonId);
            if (salesPersonEditVm == null)
            {
                salesPersonEditVm = _salesPersonEditViewModelCreator();
                SalesPersonEditViewModels.Add(salesPersonEditVm);
                salesPersonEditVm.Load(salesPersonId);
            }
            SelectedSalesPersonEditViewModel = salesPersonEditVm;
        }
        #endregion

        #region OnCloseSalesPersonTabExecute
        private void OnCloseSalesPersonTabExecute(object parameter)
        {
            var salesPersonEditVmToClose = parameter as ISalesPersonEditViewModel;
            if (salesPersonEditVmToClose != null)
            {
                if (salesPersonEditVmToClose.SalesPerson.IsChanged)
                {
                    var result = _messageDialogService.ShowYesNoDialog("Закрыть закладку?",
                      "Изменения будут утеряны. Закрыть ее?",
                      MessageDialogResult.No);
                    if (result == MessageDialogResult.No)
                    {
                        return;
                    }
                }
                SalesPersonEditViewModels.Remove(salesPersonEditVmToClose);
            }
        }
        #endregion

        #region OnSalesPersonDeleted
        private void OnSalesPersonDeleted(int salesPersonId)
        {
            ISalesPersonEditViewModel salesPersonDetailVmToClose
              = SalesPersonEditViewModels.SingleOrDefault(f => f.SalesPerson.PersonId == salesPersonId);
            if (salesPersonDetailVmToClose != null)
            {
                SalesPersonEditViewModels.Remove(salesPersonDetailVmToClose);
            }
        }
        #endregion

    }
}
