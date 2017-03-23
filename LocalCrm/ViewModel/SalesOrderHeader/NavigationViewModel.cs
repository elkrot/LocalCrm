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
    public interface INavigationViewModel 
    {
        ConditionViewModel ConditionViewModel { get; set; }
        void Load();
    }

    public class NavigationViewModel : INavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILookupProvider<SalesOrderHeader> _salesOrderLookupProvider;
        #region Constructor
        public NavigationViewModel(IEventAggregator eventAggregator,
          ILookupProvider<SalesOrderHeader> salesOrderHeaderLookupProvider
)
        {
            
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SalesOrderSavedEvent>().Subscribe(OnSalesOrderHeaderSaved);
            _eventAggregator.GetEvent<SalesOrderDeletedEvent>().Subscribe(OnSalesOrderHeaderDeleted);
            _salesOrderLookupProvider = salesOrderHeaderLookupProvider;
            NavigationItems = new ObservableCollection<NavigationItemViewModel>();
        }
        #endregion

        #region Load
        public void Load()
        {
            ConditionViewModel.Load();
            NavigationItems.Clear();
            foreach (var friendLookupItem in 
                _salesOrderLookupProvider.GetLookupWithCondition(
                     x=>x.OrderDate>= ConditionViewModel.StartDate && x.OrderDate <= ConditionViewModel.EndDate
                    , y=>y.OrderNo
                ))
            {
                NavigationItems.Add(
                  new NavigationItemViewModel(
                    friendLookupItem.Id,
                    friendLookupItem.DisplayValue,
                    _eventAggregator));
            }
        }
        #endregion
        public ConditionViewModel ConditionViewModel { get; set; }
        public ObservableCollection<NavigationItemViewModel> NavigationItems { get; set; }

        #region OnSalesOrderHeaderDeleted
        private void OnSalesOrderHeaderDeleted(int friendId)
        {
            var navigationItem =
              NavigationItems.SingleOrDefault(item => item.SalesOrderHeaderId == friendId);
            if (navigationItem != null)
            {
                NavigationItems.Remove(navigationItem);
            }
        }
        #endregion

        #region OnSalesOrderHeaderSaved
        private void OnSalesOrderHeaderSaved(SalesOrderHeader savedSalesOrderHeader)
        {
            var navigationItem =
              NavigationItems.SingleOrDefault(item => item.SalesOrderHeaderId == savedSalesOrderHeader.SalesOrderId);
            if (navigationItem != null)
            {
                navigationItem.DisplayValue = string.Format("{0} от {1:d}", savedSalesOrderHeader.OrderNo, savedSalesOrderHeader.OrderDate);
            }
            else
            {
                Load();
            }
        }
        #endregion

    }

    public class NavigationItemViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayValue;

        #region Constructor
        public NavigationItemViewModel(int salesOrderHeaderId,
          string displayValue,
          IEventAggregator eventAggregator)
        {
            SalesOrderHeaderId = salesOrderHeaderId;
            DisplayValue = displayValue;
            _eventAggregator = eventAggregator; 
            OpenSalesOrderHeaderEditViewCommand = new DelegateCommand(OpenSalesOrderHeaderEditViewExecute);
        }
        #endregion


        public ICommand OpenSalesOrderHeaderEditViewCommand { get; set; }
        
        public int SalesOrderHeaderId { get; private set; }

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

        #region OpenSalesOrderHeaderEditViewExecute
        private void OpenSalesOrderHeaderEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenSalesOrderEditViewEvent>().Publish(SalesOrderHeaderId);
        }
        #endregion

    }
}
