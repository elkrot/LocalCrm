//    class CityNavigationViewModel
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

    public interface ICityNavigationViewModel
    {
        void Load();
    }

    public class CityNavigationViewModel : ICityNavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILookupProvider<City> _cityLookupProvider;
        #region Constructor
        public CityNavigationViewModel(IEventAggregator eventAggregator,
          ILookupProvider<City> cityLookupProvider)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CitySavedEvent>().Subscribe(OnCitySaved);
            _eventAggregator.GetEvent<CityDeletedEvent>().Subscribe(OnCityDeleted);
            _cityLookupProvider = cityLookupProvider;
            CityNavigationItems = new ObservableCollection<CityNavigationItemViewModel>();
        }
        #endregion

        #region Load
        public void Load()
        {
            CityNavigationItems.Clear();
            foreach (var lookupItem in _cityLookupProvider.GetLookup())
            {
                CityNavigationItems.Add(
                  new CityNavigationItemViewModel(
                    lookupItem.Id,
                    lookupItem.DisplayValue,
                    _eventAggregator));
            }
        }
        #endregion

        public ObservableCollection<CityNavigationItemViewModel> CityNavigationItems { get; set; }

        #region OnCityDeleted
        private void OnCityDeleted(int cityId)
        {
            var navigationItem =
              CityNavigationItems.SingleOrDefault(item => item.CityId == cityId);
            if (navigationItem != null)
            {
                CityNavigationItems.Remove(navigationItem);
            }
        }
        #endregion

        #region OnCitySaved
        private void OnCitySaved(City savedCity)
        {
            var navigationItem =
              CityNavigationItems.SingleOrDefault(item => item.CityId == savedCity.CityId);
            if (navigationItem != null)
            {
                navigationItem.DisplayValue = string.Format("{0} ", savedCity.CityName);
            }
            else
            {
                Load();
            }
        }
        #endregion

    }
    /// <summary>
    /// CityNavigationItemViewModel
    /// </summary>
    public class CityNavigationItemViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayValue;

        #region Constructor
        public CityNavigationItemViewModel(int cityId,
          string displayValue,
          IEventAggregator eventAggregator)
        {
            CityId = cityId;
            DisplayValue = displayValue;
            _eventAggregator = eventAggregator;
            OpenCityEditViewCommand = new DelegateCommand(OpenCityEditViewExecute);
        }
        #endregion


        public ICommand OpenCityEditViewCommand { get; set; }

        public int CityId { get; private set; }

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

        #region OpenCityEditViewExecute
        private void OpenCityEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenCityEditViewEvent>().Publish(CityId);
        }
        #endregion

    }
}
