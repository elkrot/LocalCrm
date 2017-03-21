using LocalCrm.Command;
using LocalCrm.DataProvider.Lookups;
using LocalCrm.Event;
using LocalCrm.Model;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LocalCrm.ViewModel
{

public interface ICustomerNavigationViewModel
    {
        void Load();
    }

    public class CustomerNavigationViewModel : ICustomerNavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILookupProvider<Customer> _customerLookupProvider;
        #region Constructor
        public CustomerNavigationViewModel(IEventAggregator eventAggregator,
          ILookupProvider<Customer> customerLookupProvider)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CustomerSavedEvent>().Subscribe(OnCustomerSaved);
            _eventAggregator.GetEvent<CustomerDeletedEvent>().Subscribe(OnCustomerDeleted);
            _customerLookupProvider = customerLookupProvider;
            CustomerNavigationItems = new ObservableCollection<CustomerNavigationItemViewModel>();
        }
        #endregion

        #region Load
        public void Load()
        {
            CustomerNavigationItems.Clear();
            foreach (var lookupItem in _customerLookupProvider.GetLookup())
            {
                CustomerNavigationItems.Add(
                  new CustomerNavigationItemViewModel(
                    lookupItem.Id,
                    lookupItem.DisplayValue,
                    _eventAggregator));
            }
        }
        #endregion

        public ObservableCollection<CustomerNavigationItemViewModel> CustomerNavigationItems { get; set; }

        #region OnCustomerDeleted
        private void OnCustomerDeleted(int customerId)
        {
            var navigationItem =
              CustomerNavigationItems.SingleOrDefault(item => item.CustomerId == customerId);
            if (navigationItem != null)
            {
                CustomerNavigationItems.Remove(navigationItem);
            }
        }
        #endregion

        #region OnCustomerSaved
        private void OnCustomerSaved(Customer savedCustomer)
        {
            var navigationItem =
              CustomerNavigationItems.SingleOrDefault(item => item.CustomerId == savedCustomer.PersonId);
            if (navigationItem != null)
            {
                navigationItem.DisplayValue = string.Format("{0} от {1}", savedCustomer.FirstName, savedCustomer.LastName);
            }
            else
            {
                Load();
            }
        }
        #endregion

    }
    /// <summary>
    /// CustomerNavigationItemViewModel
    /// </summary>
    public class CustomerNavigationItemViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayValue;

        #region Constructor
        public CustomerNavigationItemViewModel(int customerId,
          string displayValue,
          IEventAggregator eventAggregator)
        {
            CustomerId = customerId;
            DisplayValue = displayValue;
            _eventAggregator = eventAggregator;
            OpenCustomerEditViewCommand = new DelegateCommand(OpenCustomerEditViewExecute);
        }
        #endregion


        public ICommand OpenCustomerEditViewCommand { get; set; }

        public int CustomerId { get; private set; }

        #region DisplayValue
        public string DisplayValue
        {
            get { return _displayValue; }
            set
            {
                _displayValue = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region OpenCustomerEditViewExecute
        private void OpenCustomerEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenCustomerEditViewEvent>().Publish(CustomerId);
        }
        #endregion

    }
}
