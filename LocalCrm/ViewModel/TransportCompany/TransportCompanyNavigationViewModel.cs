//    class TransportCompanyNavigationViewModel
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

    public interface ITransportCompanyNavigationViewModel
    {
        void Load();
    }

    public class TransportCompanyNavigationViewModel : ITransportCompanyNavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILookupProvider<TransportCompany> _transportCompanyLookupProvider;
        #region Constructor
        public TransportCompanyNavigationViewModel(IEventAggregator eventAggregator,
          ILookupProvider<TransportCompany> transportCompanyLookupProvider)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<TransportCompanySavedEvent>().Subscribe(OnTransportCompanySaved);
            _eventAggregator.GetEvent<TransportCompanyDeletedEvent>().Subscribe(OnTransportCompanyDeleted);
            _transportCompanyLookupProvider = transportCompanyLookupProvider;
            TransportCompanyNavigationItems = new ObservableCollection<TransportCompanyNavigationItemViewModel>();
        }
        #endregion

        #region Load
        public void Load()
        {
            TransportCompanyNavigationItems.Clear();
            foreach (var lookupItem in _transportCompanyLookupProvider.GetLookup())
            {
                TransportCompanyNavigationItems.Add(
                  new TransportCompanyNavigationItemViewModel(
                    lookupItem.Id,
                    lookupItem.DisplayValue,
                    _eventAggregator));
            }
        }
        #endregion

        public ObservableCollection<TransportCompanyNavigationItemViewModel> TransportCompanyNavigationItems { get; set; }

        #region OnTransportCompanyDeleted
        private void OnTransportCompanyDeleted(int transportCompanyId)
        {
            var navigationItem =
              TransportCompanyNavigationItems.SingleOrDefault(item => item.TransportCompanyId == transportCompanyId);
            if (navigationItem != null)
            {
                TransportCompanyNavigationItems.Remove(navigationItem);
            }
        }
        #endregion

        #region OnTransportCompanySaved
        private void OnTransportCompanySaved(TransportCompany savedTransportCompany)
        {
            var navigationItem =
              TransportCompanyNavigationItems.SingleOrDefault(item => item.TransportCompanyId == savedTransportCompany.TransportCompanyId);
            if (navigationItem != null)
            {
                navigationItem.DisplayValue = string.Format("{0} ", savedTransportCompany.TransportCompanyName);
            }
            else
            {
                Load();
            }
        }
        #endregion

    }
    /// <summary>
    /// TransportCompanyNavigationItemViewModel
    /// </summary>
    public class TransportCompanyNavigationItemViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayValue;

        #region Constructor
        public TransportCompanyNavigationItemViewModel(int transportCompanyId,
          string displayValue,
          IEventAggregator eventAggregator)
        {
            TransportCompanyId = transportCompanyId;
            DisplayValue = displayValue;
            _eventAggregator = eventAggregator;
            OpenTransportCompanyEditViewCommand = new DelegateCommand(OpenTransportCompanyEditViewExecute);
        }
        #endregion


        public ICommand OpenTransportCompanyEditViewCommand { get; set; }

        public int TransportCompanyId { get; private set; }

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

        #region OpenTransportCompanyEditViewExecute
        private void OpenTransportCompanyEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenTransportCompanyEditViewEvent>().Publish(TransportCompanyId);
        }
        #endregion

    }
}
