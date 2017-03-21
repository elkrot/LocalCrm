
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
    public interface ITransportCompanyEditViewModel
    {
        void Load(int? TransportCompanyId = null);
        TransportCompanyWrapper TransportCompany { get; }
    }
    class TransportCompanyEditViewModel : Observable, ITransportCompanyEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ITransportCompanyDataProvider _transportCompanyDataProvider;

        private TransportCompanyWrapper _transportCompanyW;

        public TransportCompanyEditViewModel(IEventAggregator eventAggregator,
   IMessageDialogService messageDialogService,
   ITransportCompanyDataProvider transportCompanyDataProvider)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _transportCompanyDataProvider = transportCompanyDataProvider;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

        }
        public TransportCompanyWrapper TransportCompany
        {
            get
            {
                return _transportCompanyW;
            }
        }

        public void Load(int? TransportCompanyId = default(int?))
        {
            var __transportCompany = TransportCompanyId.HasValue ?
                _transportCompanyDataProvider.GetTransportCompanyById((int)TransportCompanyId) :
                new TransportCompany();
            _transportCompanyW = new TransportCompanyWrapper(__transportCompany);
            _transportCompanyW.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_transportCompanyW.IsChanged)
                || e.PropertyName == nameof(_transportCompanyW.IsValid))
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

            var saveRet = _transportCompanyDataProvider.SaveTransportCompany(TransportCompany.Model);

            if (!saveRet.Success)
            {
                var result = _messageDialogService.ShowMessageDialog(
    "Ошибка при сохранении",
    string.Format("Во время сохранения записи {0}{2} возникла исключительная ситуация {2} {1}"
    , TransportCompany.TransportCompanyName.Trim(), saveRet.Messages.FirstOrDefault().Trim(), Environment.NewLine), MessageDialogResult.Ok);
                TransportCompany.RejectChanges();
            }
            else
            { TransportCompany.AcceptChanges(); }
            _eventAggregator.GetEvent<TransportCompanySavedEvent>().Publish(TransportCompany.Model);
            InvalidateCommands();
        }

        private bool OnSaveCanExecute(object arg)
        {
            return TransportCompany.IsChanged && TransportCompany.IsValid;
        }

        private void OnResetExecute(object obj)
        {
            TransportCompany.RejectChanges();
        }

        private bool OnResetCanExecute(object arg)
        {
            return TransportCompany.IsChanged;
        }

        private bool OnDeleteCanExecute(object arg)
        {
            return TransportCompany != null && TransportCompany.TransportCompanyId > 0;
        }

        private void OnDeleteExecute(object obj)
        {
            var result = _messageDialogService.ShowYesNoDialog(
                "Удалить Продавца?",
                string.Format("Вы действительно хотите удалить {1} '{0}'"
                , TransportCompany.TransportCompanyName.Trim(), Environment.NewLine),
                MessageDialogResult.No);

            if (result == MessageDialogResult.Yes)
            {

                var saveRet = _transportCompanyDataProvider.DeleteTransportCompany(TransportCompany.TransportCompanyId);

                if (!saveRet.Success)
                {
                    _messageDialogService.ShowMessageDialog(
                   "Ошибка при сохранении",
                   string.Format("Во время сохранения записи {0}{2} возникла исключительная ситуация{2}  {1}"
                   , TransportCompany.TransportCompanyName.Trim()
                   ,
                   saveRet.Messages.FirstOrDefault().Trim()
                   , Environment.NewLine)
                   , MessageDialogResult.Ok);
                    TransportCompany.RejectChanges();
                }
                else
                {
                    TransportCompany.AcceptChanges();
   _eventAggregator.GetEvent<TransportCompanyDeletedEvent>().Publish(TransportCompany.TransportCompanyId);
         
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
