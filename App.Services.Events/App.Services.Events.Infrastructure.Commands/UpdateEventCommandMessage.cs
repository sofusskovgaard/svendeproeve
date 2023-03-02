using App.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Events.Infrastructure.Commands
{
    public class UpdateEventCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
