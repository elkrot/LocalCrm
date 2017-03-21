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
    public interface ISalesPersonEditViewModel
    {
        void Load(int? SalesPersonId = null);
        SalesPersonWrapper SalesPerson { get; }
    }
    class SalesPersonEditViewModel : Observable, ISalesPersonEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ISalesPersonDataProvider _salesPersonDataProvider;

        private SalesPersonWrapper _salesPersonW;

        public SalesPersonEditViewModel(IEventAggregator eventAggregator,
   IMessageDialogService messageDialogService,
   ISalesPersonDataProvider salesPersonDataProvider)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _salesPersonDataProvider = salesPersonDataProvider;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

        }
        public SalesPersonWrapper SalesPerson
        {
            get
            {
                return _salesPersonW;
            }
        }

        public void Load(int? SalesPersonId = default(int?))
        {
            var __salesPerson = SalesPersonId.HasValue ?
                _salesPersonDataProvider.GetSalesPersonById((int)SalesPersonId) :
                new SalesPerson();
            _salesPersonW = new SalesPersonWrapper(__salesPerson);
            _salesPersonW.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_salesPersonW.IsChanged)
                || e.PropertyName == nameof(_salesPersonW.IsValid))
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
            var saveRet = _salesPersonDataProvider.SaveSalesPerson(SalesPerson.Model);

            if (!saveRet.Success)
            {


                var result = _messageDialogService.ShowMessageDialog(
    "Ошибка при сохранении",
    string.Format("Во время сохранения записи {0} {2}возникла исключительная ситуация{2}  {1}"
    , SalesPerson.FirstName.Trim(), saveRet.Messages.FirstOrDefault().Trim(), Environment.NewLine), MessageDialogResult.Ok);
                SalesPerson.RejectChanges();
            }
            else
            {
                SalesPerson.AcceptChanges();

            }

            _eventAggregator.GetEvent<SalesPersonSavedEvent>().Publish(SalesPerson.Model);
            InvalidateCommands();
        }

        private bool OnSaveCanExecute(object arg)
        {
            return SalesPerson.IsChanged && SalesPerson.IsValid;
        }

        private void OnResetExecute(object obj)
        {
            SalesPerson.RejectChanges();
        }

        private bool OnResetCanExecute(object arg)
        {
            return SalesPerson.IsChanged;
        }

        private bool OnDeleteCanExecute(object arg)
        {
            return SalesPerson != null && SalesPerson.PersonId > 0;
        }

        private void OnDeleteExecute(object obj)
        {
            var result = _messageDialogService.ShowYesNoDialog(
                "Удалить Продавца?",
                string.Format("Вы действительно хотите удалить {2} '{0} {1}'"
                , SalesPerson.FirstName.Trim(), SalesPerson.LastName.Trim(), Environment.NewLine),
                MessageDialogResult.No);

            if (result == MessageDialogResult.Yes)
            {
                var saveRet = _salesPersonDataProvider.DeleteSalesPerson(SalesPerson.PersonId);

                if (!saveRet.Success)
                {
                    _messageDialogService.ShowMessageDialog(
       "Ошибка при сохранении",
       string.Format("Во время сохранения записи {0}{2} возникла исключительная ситуация{2}  {1}"
       , SalesPerson.FirstName, saveRet.Messages.FirstOrDefault(), Environment.NewLine), MessageDialogResult.Ok);
                    SalesPerson.RejectChanges();
                }
                else
                {
                    SalesPerson.AcceptChanges();
 _eventAggregator.GetEvent<SalesPersonDeletedEvent>().Publish(SalesPerson.PersonId);
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
