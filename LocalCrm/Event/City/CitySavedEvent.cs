using LocalCrm.Model;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.Event
{
    class CitySavedEvent : PubSubEvent<City>
    {
    }
}
