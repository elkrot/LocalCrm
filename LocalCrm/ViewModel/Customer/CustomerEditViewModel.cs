using LocalCrm.Command;
using LocalCrm.DataProvider;
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
    public interface ICustomerEditViewModel
    {
        void Load(int? CustomerId = null);
        CustomerWrapper Customer { get; }
    }
    class CustomerEditViewModel : Observable, ICustomerEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ICustomerDataProvider _customerDataProvider;

        private CustomerWrapper _customerW;

        public CustomerEditViewModel(IEventAggregator eventAggregator,
   IMessageDialogService messageDialogService,
   ICustomerDataProvider customerDataProvider)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _customerDataProvider = customerDataProvider;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

        }
        public CustomerWrapper Customer
        {
            get
            {
               return _customerW;
            }
        }

        public void Load(int? CustomerId = default(int?))
        {
            var __customer = CustomerId.HasValue ?
                _customerDataProvider.GetCustomerById((int)CustomerId) :
                new Customer();
            _customerW = new CustomerWrapper(__customer);
            _customerW.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_customerW.IsChanged)
                || e.PropertyName == nameof(_customerW.IsValid))
                {
                    InvalidateCommands();
                }
            };
        }
        #region Commands
        public ICommand SaveCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand AddEmailCommand { get; private set; }

        public ICommand RemoveEmailCommand { get; private set; }



        private void OnSaveExecute(object obj)
        {

            var saveRet = _customerDataProvider.SaveCustomer(Customer.Model);

            if (!saveRet.Success)
            {


                var result = _messageDialogService.ShowMessageDialog(
    "Ошибка при сохранении",
    string.Format("Во время сохранения записи {0}{2} возникла исключительная ситуация  {1}"
    , Customer.FirstName.Trim(), saveRet.Messages.FirstOrDefault(), Environment.NewLine), MessageDialogResult.Ok);
                Customer.RejectChanges();
            }
            else
            {
                Customer.AcceptChanges();
            }
            _eventAggregator.GetEvent<CustomerSavedEvent>().Publish(Customer.Model);
            InvalidateCommands();
        }

        private bool OnSaveCanExecute(object arg)
        {
            return Customer.IsChanged && Customer.IsValid;
        }

        private void OnResetExecute(object obj)
        {
            Customer.RejectChanges();
        }

        private bool OnResetCanExecute(object arg)
        {
            return Customer.IsChanged;
        }

        private bool OnDeleteCanExecute(object arg)
        {
            return Customer != null && Customer.PersonId > 0;
        }

        private void OnDeleteExecute(object obj)
        {
            var result = _messageDialogService.ShowYesNoDialog(
                "Удалить Продавца?",
                string.Format("Вы действительно хотите удалить {2} '{0} {1}'?"
                , Customer.FirstName.Trim(), Customer.LastName.Trim(), Environment.NewLine),
                MessageDialogResult.No);

            if (result == MessageDialogResult.Yes)
            {

                var saveRet = _customerDataProvider.DeleteCustomer(Customer.PersonId);

                if (!saveRet.Success)
                {
                    _messageDialogService.ShowMessageDialog(
       "Ошибка при сохранении",
       string.Format("Во время сохранения записи {0}{2} возникла исключительная ситуация {2} {1}"
       , Customer.FirstName, saveRet.Messages.FirstOrDefault(), Environment.NewLine), MessageDialogResult.Ok);
                    Customer.RejectChanges();
                }
                else
                {
                    Customer.AcceptChanges();
_eventAggregator.GetEvent<CustomerDeletedEvent>().Publish(Customer.PersonId);
                }


            }
        }
        #endregion
        private void InvalidateCommands()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ResetCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
        }
    }
}
