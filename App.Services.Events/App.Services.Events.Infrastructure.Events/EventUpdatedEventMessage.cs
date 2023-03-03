using App.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Events.Infrastructure.Events
{
    public class EventUpdatedEventMessage : IEventMessage
    {
        public string Id { get; set; }
    }
}
