using LocalCrm.Command;
using LocalCrm.DataProvider;
using LocalCrm.DataProvider.Lookups;
using LocalCrm.Event;
using LocalCrm.Model;
using LocalCrm.View.Services;
using LocalCrm.Wrapper;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LocalCrm.ViewModel
{
    public interface ISalesOrderHeaderEditViewModel
    {
        void Load(int? SalesOrderHeaderId = null);
        SalesOrderHeaderWrapper SalesOrderHeader { get; }
    }
    public class SalesOrderHeaderEditViewModel : Observable, ISalesOrderHeaderEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ISalesOrderDataProvider _salesOrderDataProvider;

        private SalesOrderHeaderWrapper _salesOrderHeader;

        private readonly ILookupProvider<City> _cityLookupProvider;
        private readonly ILookupProvider<Status> _statusLookupProvider;
        private readonly ILookupProvider<SalesPerson> _salesPersonLookupProvider;
        private readonly ILookupProvider<Customer> _customerLookupProvider;
        private readonly ILookupProvider<TransportCompany> _transportCompanyLookupProvider;

        private IEnumerable<LookupItem> _cities;
        private IEnumerable<LookupItem> _statuses;
        private IEnumerable<LookupItem> _salesPersons;
        private IEnumerable<LookupItem> _customers;
        private IEnumerable<LookupItem> _transportCompanies;
        //private FriendEmailWrapper _selectedEmail;



        public SalesOrderHeaderEditViewModel(IEventAggregator eventAggregator,
   IMessageDialogService messageDialogService,
   ISalesOrderDataProvider salesOrderDataProvider,
   ILookupProvider<City> cityLookupProvider,
   ILookupProvider<Status> statusLookupProvider,
   ILookupProvider<SalesPerson> salesPersonLookupProvider,
   ILookupProvider<Customer> customerLookupProvider,
   ILookupProvider<TransportCompany> transportCompanyLookupProvider
   )
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _salesOrderDataProvider = salesOrderDataProvider;
            _cityLookupProvider = cityLookupProvider;
            _statusLookupProvider = statusLookupProvider;
            _salesPersonLookupProvider = salesPersonLookupProvider;
            _customerLookupProvider = customerLookupProvider;
            _transportCompanyLookupProvider = transportCompanyLookupProvider;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

           // AddEmailCommand = new DelegateCommand(OnAddEmailExecute);
          //  RemoveEmailCommand = new DelegateCommand(OnRemoveEmailExecute, OnRemoveEmailCanExecute);
        }


        #region SalesOrderHeader
        public SalesOrderHeaderWrapper SalesOrderHeader
        {
            get
            {
                return _salesOrderHeader;
            }
        }
        #endregion



        #region Load
        public void Load(int? SalesOrderHeaderId = default(int?))
        {
            CityLookup = _cityLookupProvider.GetLookup();
            StatusLookup= _statusLookupProvider.GetLookup();
            SalesPersonLookup = _salesPersonLookupProvider.GetLookup();
            CustomerLookup = _customerLookupProvider.GetLookup();
            TransportCompanyLookupProvider = _transportCompanyLookupProvider.GetLookup();
            var _salesOrder= SalesOrderHeaderId.HasValue?
                _salesOrderDataProvider.GetSalesOrderHeaderById((int)SalesOrderHeaderId):
                new SalesOrderHeader();
            _salesOrderHeader = new SalesOrderHeaderWrapper(_salesOrder);
            if (_salesOrderHeader.OrderDate == DateTime.MinValue)
                _salesOrderHeader.OrderDate = DateTime.Today;

            _salesOrderHeader.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_salesOrderHeader.IsChanged)
                || e.PropertyName == nameof(_salesOrderHeader.IsValid))
                {
                    InvalidateCommands();
                }
            };
        }
        #endregion
        

        
             public IEnumerable<LookupItem> TransportCompanyLookupProvider
        {
            get { return _transportCompanies; }
            set
            {
                _transportCompanies = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<LookupItem> CityLookup
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<LookupItem> StatusLookup
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<LookupItem> SalesPersonLookup
        {
            get { return _salesPersons; }
            set
            {
                _salesPersons = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<LookupItem> CustomerLookup
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        #region Comands
        public ICommand SaveCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand AddEmailCommand { get; private set; }

        public ICommand RemoveEmailCommand { get; private set; }



        private void OnSaveExecute(object obj)
        {
            var saveRet = _salesOrderDataProvider.SaveSalesOrderHeader(SalesOrderHeader.Model);

            if (!saveRet.Success)
            {


                var result = _messageDialogService.ShowMessageDialog(
    "Ошибка при сохранении",
    string.Format("Во время сохранения записи {0}{2} возникла исключительная ситуация{2}  {1}"
    , SalesOrderHeader.OrderNo, saveRet.Messages.FirstOrDefault(), Environment.NewLine), MessageDialogResult.Ok);
                SalesOrderHeader.RejectChanges();
            }
            else
            {
                SalesOrderHeader.AcceptChanges();
            }
            _eventAggregator.GetEvent<SalesOrderSavedEvent>().Publish(SalesOrderHeader.Model);
            InvalidateCommands();
        }

        private bool OnSaveCanExecute(object arg)
        {
            return SalesOrderHeader.IsChanged && SalesOrderHeader.IsValid;
        }

        private void OnResetExecute(object obj)
        {
            SalesOrderHeader.RejectChanges();
        }

        private bool OnResetCanExecute(object arg)
        {
            return SalesOrderHeader.IsChanged;
        }

        private bool OnDeleteCanExecute(object arg)
        {
            return SalesOrderHeader != null && SalesOrderHeader.SalesOrderId > 0;
        }

        private void OnDeleteExecute(object obj)
        {
            var result = _messageDialogService.ShowYesNoDialog(
                "Удалить заказ?",
                string.Format("Вы действительно хотите удалить заказ{2} '{0} от {1:d}'"
                , SalesOrderHeader.OrderNo, SalesOrderHeader.OrderDate, Environment.NewLine),
                MessageDialogResult.No);

            if (result == MessageDialogResult.Yes)
            {

                var saveRet = _salesOrderDataProvider.DeleteSalesOrderHeader(SalesOrderHeader.SalesOrderId);

                if (!saveRet.Success)
                {
                    _messageDialogService.ShowMessageDialog(
       "Ошибка при сохранении",
       string.Format("Во время сохранения записи {0}{2} возникла исключительная ситуация  {1}"
       , SalesOrderHeader.OrderNo.Trim(), saveRet.Messages.FirstOrDefault(), Environment.NewLine), MessageDialogResult.Ok);
                    SalesOrderHeader.RejectChanges();
                }
                else
                {
                    SalesOrderHeader.AcceptChanges();
 
                _eventAggregator.GetEvent<SalesOrderDeletedEvent>().Publish(SalesOrderHeader.SalesOrderId);

                }
                           }
        }
        /*
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
        }*/

        private void InvalidateCommands()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ResetCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
        }
        #endregion





    }
}
