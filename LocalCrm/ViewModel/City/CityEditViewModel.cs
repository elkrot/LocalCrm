
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
    public interface ICityEditViewModel
    {
        void Load(int? CityId = null);
        CityWrapper City { get; }
    }
    class CityEditViewModel : Observable, ICityEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ICityDataProvider _cityDataProvider;

        private CityWrapper _cityW;

        public CityEditViewModel(IEventAggregator eventAggregator,
   IMessageDialogService messageDialogService,
   ICityDataProvider cityDataProvider)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _cityDataProvider = cityDataProvider;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

        }
        public CityWrapper City
        {
            get
            {
                return _cityW;
            }
        }

        public void Load(int? CityId = default(int?))
        {
            var __city = CityId.HasValue ?
                _cityDataProvider.GetCityById((int)CityId) :
                new City();
            _cityW = new CityWrapper(__city);
            _cityW.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_cityW.IsChanged)
                || e.PropertyName == nameof(_cityW.IsValid))
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
            var saveRet = _cityDataProvider.SaveCity(City.Model);

            if (!saveRet.Success) {
                var result = _messageDialogService.ShowMessageDialog(
    "Ошибка при сохранении",
    string.Format("Во время сохранения записи {0} возникла исключительная ситуация  {1}"
    , City.CityName, saveRet.Messages.FirstOrDefault()), MessageDialogResult.Ok);
                City.RejectChanges();
            } else { 
            City.AcceptChanges();
            }
            _eventAggregator.GetEvent<CitySavedEvent>().Publish(City.Model);
            InvalidateCommands();
        }

        private bool OnSaveCanExecute(object arg)
        {
            return City.IsChanged && City.IsValid;
        }

        private void OnResetExecute(object obj)
        {
            City.RejectChanges();
        }

        private bool OnResetCanExecute(object arg)
        {
            return City.IsChanged;
        }

        private bool OnDeleteCanExecute(object arg)
        {
            return City != null && City.CityId > 0;
        }

        private void OnDeleteExecute(object obj)
        {
            var result = _messageDialogService.ShowYesNoDialog(
                "Удалить Продавца?",
                string.Format("Вы действительно хотите удалить{1}'{0}'"
                , City.CityName.Trim(), Environment.NewLine),
                MessageDialogResult.No);

            if (result == MessageDialogResult.Yes)
            {

                var saveRet = _cityDataProvider.DeleteCity(City.CityId); 

                if (!saveRet.Success)
                {
                     _messageDialogService.ShowMessageDialog(
        "Ошибка при сохранении",
        string.Format("Во время сохранения записи {0} {2} возникла исключительная ситуация  {1}"
        , City.CityName.Trim(), saveRet.Messages.FirstOrDefault(), Environment.NewLine), MessageDialogResult.Ok);
                    City.RejectChanges();
                }
                else
                {
                    City.AcceptChanges();
_eventAggregator.GetEvent<CityDeletedEvent>().Publish(City.CityId);
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
