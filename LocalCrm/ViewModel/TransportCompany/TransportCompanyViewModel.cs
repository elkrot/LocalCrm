using LocalCrm.Command;
using LocalCrm.DataProvider;
using LocalCrm.Event;
using LocalCrm.View.Services;
using LocalCrm.Wrapper;
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
    /*public class TransportCompanyViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ITransportCompanyDataProvider _transportCompanyDataProvider;
        #region Constructor
        public TransportCompanyViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            ITransportCompanyDataProvider transportCompanyDataProvider)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            // _eventAggregator.GetEvent<OpenCustomerEditViewEvent>().Subscribe(OnOpenCustomerTab);
            // _eventAggregator.GetEvent<CustomerDeletedEvent>().Subscribe(OnCustomerDeleted);
            _transportCompanyDataProvider = transportCompanyDataProvider;
             AddTransportCompanyCommand = new DelegateCommand(AddTransportCompanyExecute);
            RemoveTransportCompanyCommand = new DelegateCommand(OnRemoveTransportCompanyExecute, OnRemoveTransportCompanyCanExecute);
        }
        #endregion

        public ICommand AddTransportCompanyCommand { get; private set; }

        public ICommand RemoveTransportCompanyCommand { get; set; }

        TransportCompanyWrapper _selectedTransportCompany;


        public TransportCompanyWrapper SelectedTransportCompany
        {
            get { return _selectedTransportCompany; }
            set
            {
                _selectedTransportCompany = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveTransportCompanyCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }





        private void OnSaveExecute(object obj)
        {
            _transportCompanyDataProvider.SaveTransportCompany(Friend.Model);
            Friend.AcceptChanges();
            _eventAggregator.GetEvent<FriendSavedEvent>().Publish(Friend.Model);
            InvalidateCommands();
        }

        private bool OnSaveCanExecute(object arg)
        {
            return Friend.IsChanged && Friend.IsValid;
        }

        private void OnResetExecute(object obj)
        {
            Friend.RejectChanges();
        }

        private bool OnResetCanExecute(object arg)
        {
            return Friend.IsChanged;
        }

        private bool OnDeleteCanExecute(object arg)
        {
            return Friend != null && Friend.Id > 0;
        }

        private void OnDeleteExecute(object obj)
        {
            var result = _messageDialogService.ShowYesNoDialog(
                "Delete Friend",
                string.Format("Do you really want to delete the friend '{0} {1}'", Friend.FirstName, Friend.LastName),
                MessageDialogResult.No);

            if (result == MessageDialogResult.Yes)
            {
                _friendDataProvider.DeleteFriend(Friend.Id);
                _eventAggregator.GetEvent<FriendDeletedEvent>().Publish(Friend.Id);
            }
        }

        private void OnRemoveEmailExecute(object obj)
        {
            Friend.Emails.Remove(SelectedEmail);
            ((DelegateCommand)RemoveEmailCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemoveEmailCanExecute(object arg)
        {
            return SelectedEmail != null;
        }

        private void OnAddEmailExecute(object obj)
        {
            Friend.Emails.Add(new FriendEmailWrapper(new FriendEmail()));
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ResetCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
        }

    }*/




    public class TransportCompanyViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private ITransportCompanyEditViewModel _selectedTransportCompanyEditViewModel;
        private Func<ITransportCompanyEditViewModel> _transportCompanyEditViewModelCreator;

        #region Constructor
        public TransportCompanyViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            ITransportCompanyNavigationViewModel navigationViewModel,
            Func<ITransportCompanyEditViewModel> transportCompanyEditViewModelCreator)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenTransportCompanyEditViewEvent>().Subscribe(OnOpenTransportCompanyTab);
            _eventAggregator.GetEvent<TransportCompanyDeletedEvent>().Subscribe(OnTransportCompanyDeleted);

            TransportCompanyNavigationViewModel = navigationViewModel;
            _transportCompanyEditViewModelCreator = transportCompanyEditViewModelCreator;
            TransportCompanyEditViewModels = new ObservableCollection<ITransportCompanyEditViewModel>();
            CloseTransportCompanyTabCommand = new DelegateCommand(OnCloseTransportCompanyTabExecute);
            AddTransportCompanyCommand = new DelegateCommand(OnAddTransportCompanyExecute);
        }
        #endregion

        public void Load()
        {
            TransportCompanyNavigationViewModel.Load();
        }

        #region OnClosing
        public void OnClosing(CancelEventArgs e)
        {
            if (TransportCompanyEditViewModels.Any(f => f.TransportCompany.IsChanged))
            {
                var result = _messageDialogService.ShowYesNoDialog("Закрыть приложение?",
                  "Вы потеряете все изменения. Закрывать?",
                  MessageDialogResult.No);
                e.Cancel = result == MessageDialogResult.No;
            }
        }
        #endregion

        public ICommand CloseTransportCompanyTabCommand { get; private set; }

        public ICommand AddTransportCompanyCommand { get; set; }

        public ITransportCompanyNavigationViewModel TransportCompanyNavigationViewModel { get; private set; }

        // Those ViewModels represent the Tab-Pages in the UI
        public ObservableCollection<ITransportCompanyEditViewModel> TransportCompanyEditViewModels { get; private set; }

        #region SelectedTransportCompanyEditViewModel
        public ITransportCompanyEditViewModel SelectedTransportCompanyEditViewModel
        {
            get { return _selectedTransportCompanyEditViewModel; }
            set
            {
                _selectedTransportCompanyEditViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool IsChanged => TransportCompanyEditViewModels.Any(f => f.TransportCompany.IsChanged);

        #region OnAddTransportCompanyExecute
        private void OnAddTransportCompanyExecute(object obj)
        {
            ITransportCompanyEditViewModel transportCompanyEditVm = _transportCompanyEditViewModelCreator();
            TransportCompanyEditViewModels.Add(transportCompanyEditVm);
            transportCompanyEditVm.Load();
            SelectedTransportCompanyEditViewModel = transportCompanyEditVm;
        }
        #endregion

        #region OnOpenTransportCompanyTab
        private void OnOpenTransportCompanyTab(int transportCompanyId)
        {
            ITransportCompanyEditViewModel transportCompanyEditVm =
              TransportCompanyEditViewModels.SingleOrDefault(vm => vm.TransportCompany.TransportCompanyId == transportCompanyId);
            if (transportCompanyEditVm == null)
            {
                transportCompanyEditVm = _transportCompanyEditViewModelCreator();
                TransportCompanyEditViewModels.Add(transportCompanyEditVm);
                transportCompanyEditVm.Load(transportCompanyId);
            }
            SelectedTransportCompanyEditViewModel = transportCompanyEditVm;
        }
        #endregion

        #region OnCloseTransportCompanyTabExecute
        private void OnCloseTransportCompanyTabExecute(object parameter)
        {
            var transportCompanyEditVmToClose = parameter as ITransportCompanyEditViewModel;
            if (transportCompanyEditVmToClose != null)
            {
                if (transportCompanyEditVmToClose.TransportCompany.IsChanged)
                {
                    var result = _messageDialogService.ShowYesNoDialog("Закрыть закладку?",
                      "Изменения будут утеряны. Закрыть ее?",
                      MessageDialogResult.No);
                    if (result == MessageDialogResult.No)
                    {
                        return;
                    }
                }
                TransportCompanyEditViewModels.Remove(transportCompanyEditVmToClose);
            }
        }
        #endregion

        #region OnTransportCompanyDeleted
        private void OnTransportCompanyDeleted(int transportCompanyId)
        {
            ITransportCompanyEditViewModel transportCompanyDetailVmToClose
              = TransportCompanyEditViewModels.SingleOrDefault(f => f.TransportCompany.TransportCompanyId == transportCompanyId);
            if (transportCompanyDetailVmToClose != null)
            {
                TransportCompanyEditViewModels.Remove(transportCompanyDetailVmToClose);
            }
        }
        #endregion

    }
}
