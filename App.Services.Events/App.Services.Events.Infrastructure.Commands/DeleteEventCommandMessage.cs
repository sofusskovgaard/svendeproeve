using App.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Events.Infrastructure.Commands
{
    public class DeleteEventCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
    }
}
