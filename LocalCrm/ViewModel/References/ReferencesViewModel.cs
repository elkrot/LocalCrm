using LocalCrm.View.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.ViewModel
{

    public class ReferencesViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
   
        
        // Будут ViewModels модели - Города, Персоны, Транспортные компании
        public ReferencesViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            SalesPersonViewModel salesPersonViewModel,
            CustomerViewModel customerViewModel,
            TransportCompanyViewModel transportCompanyViewModel,
            CityViewModel cityViewModel)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            CustomerViewModel = customerViewModel;
            SalesPersonViewModel = salesPersonViewModel;
            TransportCompanyViewModel = transportCompanyViewModel;
            CityViewModel = cityViewModel;
        }

        public void Load()
        {
            SalesPersonViewModel.Load();
            CustomerViewModel.Load();
            TransportCompanyViewModel.Load();
            CityViewModel.Load();
        }

        public SalesPersonViewModel SalesPersonViewModel { get; private set; }
        public CustomerViewModel CustomerViewModel { get; private set; }
        public TransportCompanyViewModel TransportCompanyViewModel { get; private set; }
        public CityViewModel CityViewModel { get; private set; }
    }
}
