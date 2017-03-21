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

public interface ISalesPesonNavigationViewModel
    {
        void Load();
    }

    public class SalesPesonNavigationViewModel : ISalesPesonNavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILookupProvider<SalesPerson> _salesPersonLookupProvider;
        #region Constructor
        public SalesPesonNavigationViewModel(IEventAggregator eventAggregator,
          ILookupProvider<SalesPerson> salesPersonLookupProvider)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SalesPersonSavedEvent>().Subscribe(OnSalesPersonSaved);
            _eventAggregator.GetEvent<SalesPersonDeletedEvent>().Subscribe(OnSalesPersonDeleted);
            _salesPersonLookupProvider = salesPersonLookupProvider;
            SalesPersonNavigationItems = new ObservableCollection<SalesPersonNavigationItemViewModel>();
        }
        #endregion

        #region Load
        public void Load()
        {
            SalesPersonNavigationItems.Clear();
            foreach (var lookupItem in _salesPersonLookupProvider.GetLookup())
            {
                SalesPersonNavigationItems.Add(
                  new SalesPersonNavigationItemViewModel(
                    lookupItem.Id,
                    lookupItem.DisplayValue,
                    _eventAggregator));
            }
        }
        #endregion

        public ObservableCollection<SalesPersonNavigationItemViewModel> SalesPersonNavigationItems { get; set; }

        #region OnSalesPersonDeleted
        private void OnSalesPersonDeleted(int salesPersonId)
        {
            var navigationItem =
              SalesPersonNavigationItems.SingleOrDefault(item => item.SalesPersonId == salesPersonId);
            if (navigationItem != null)
            {
                SalesPersonNavigationItems.Remove(navigationItem);
            }
        }
        #endregion

        #region OnSalesPersonSaved
        private void OnSalesPersonSaved(SalesPerson savedSalesPerson)
        {
            var navigationItem =
              SalesPersonNavigationItems.SingleOrDefault(item => item.SalesPersonId == savedSalesPerson.PersonId);
            if (navigationItem != null)
            {
                navigationItem.DisplayValue = string.Format("{0} от {1}", savedSalesPerson.FirstName, savedSalesPerson.LastName);
            }
            else
            {
                Load();
            }
        }
        #endregion

    }
    /// <summary>
    /// SalesPersonNavigationItemViewModel
    /// </summary>
    public class SalesPersonNavigationItemViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayValue;

        #region Constructor
        public SalesPersonNavigationItemViewModel(int salesPersonId,
          string displayValue,
          IEventAggregator eventAggregator)
        {
            SalesPersonId = salesPersonId;
            DisplayValue = displayValue;
            _eventAggregator = eventAggregator;
            OpenSalesPersonEditViewCommand = new DelegateCommand(OpenSalesPersonEditViewExecute);
        }
        #endregion


        public ICommand OpenSalesPersonEditViewCommand { get; set; }

        public int SalesPersonId { get; private set; }

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

        #region OpenSalesPersonEditViewExecute
        private void OpenSalesPersonEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenSalesPersonEditViewEvent>().Publish(SalesPersonId);
        }
        #endregion

    }
}
