﻿using App.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Events.Infrastructure.Commands
{
    public class CreateEventCommandMessage : ICommandMessage
    {
        public string EventName { get; set; }
        public string Location { get; set; }
        public IEnumerable<string> Tournaments { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}