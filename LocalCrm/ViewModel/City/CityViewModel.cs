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
   




    public class CityViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private ICityEditViewModel _selectedCityEditViewModel;
        private Func<ICityEditViewModel> _cityEditViewModelCreator;

        #region Constructor
        public CityViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            ICityNavigationViewModel navigationViewModel,
            Func<ICityEditViewModel> cityEditViewModelCreator)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenCityEditViewEvent>().Subscribe(OnOpenCityTab);
            _eventAggregator.GetEvent<CityDeletedEvent>().Subscribe(OnCityDeleted);

            CityNavigationViewModel = navigationViewModel;
            _cityEditViewModelCreator = cityEditViewModelCreator;
            CityEditViewModels = new ObservableCollection<ICityEditViewModel>();
            CloseCityTabCommand = new DelegateCommand(OnCloseCityTabExecute);
            AddCityCommand = new DelegateCommand(OnAddCityExecute);
        }
        #endregion

        public void Load()
        {
            CityNavigationViewModel.Load();
        }

        #region OnClosing
        public void OnClosing(CancelEventArgs e)
        {
            if (CityEditViewModels.Any(f => f.City.IsChanged))
            {
                var result = _messageDialogService.ShowYesNoDialog("Закрыть приложение?",
                  "Вы потеряете все изменения. Закрывать?",
                  MessageDialogResult.No);
                e.Cancel = result == MessageDialogResult.No;
            }
        }
        #endregion

        public ICommand CloseCityTabCommand { get; private set; }

        public ICommand AddCityCommand { get; set; }

        public ICityNavigationViewModel CityNavigationViewModel { get; private set; }

        // Those ViewModels represent the Tab-Pages in the UI
        public ObservableCollection<ICityEditViewModel> CityEditViewModels { get; private set; }

        #region SelectedCityEditViewModel
        public ICityEditViewModel SelectedCityEditViewModel
        {
            get { return _selectedCityEditViewModel; }
            set
            {
                _selectedCityEditViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool IsChanged => CityEditViewModels.Any(f => f.City.IsChanged);

        #region OnAddCityExecute
        private void OnAddCityExecute(object obj)
        {
            ICityEditViewModel cityEditVm = _cityEditViewModelCreator();
            CityEditViewModels.Add(cityEditVm);
            cityEditVm.Load();
            SelectedCityEditViewModel = cityEditVm;
        }
        #endregion

        #region OnOpenCityTab
        private void OnOpenCityTab(int cityId)
        {
            ICityEditViewModel cityEditVm =
              CityEditViewModels.SingleOrDefault(vm => vm.City.CityId == cityId);
            if (cityEditVm == null)
            {
                cityEditVm = _cityEditViewModelCreator();
                CityEditViewModels.Add(cityEditVm);
                cityEditVm.Load(cityId);
            }
            SelectedCityEditViewModel = cityEditVm;
        }
        #endregion

        #region OnCloseCityTabExecute
        private void OnCloseCityTabExecute(object parameter)
        {
            var cityEditVmToClose = parameter as ICityEditViewModel;
            if (cityEditVmToClose != null)
            {
                if (cityEditVmToClose.City.IsChanged)
                {
                    var result = _messageDialogService.ShowYesNoDialog("Закрыть закладку?",
                      "Изменения будут утеряны. Закрыть ее?",
                      MessageDialogResult.No);
                    if (result == MessageDialogResult.No)
                    {
                        return;
                    }
                }
                CityEditViewModels.Remove(cityEditVmToClose);
            }
        }
        #endregion

        #region OnCityDeleted
        private void OnCityDeleted(int cityId)
        {
            ICityEditViewModel cityDetailVmToClose
              = CityEditViewModels.SingleOrDefault(f => f.City.CityId == cityId);
            if (cityDetailVmToClose != null)
            {
                CityEditViewModels.Remove(cityDetailVmToClose);
            }
        }
        #endregion

    }
}
