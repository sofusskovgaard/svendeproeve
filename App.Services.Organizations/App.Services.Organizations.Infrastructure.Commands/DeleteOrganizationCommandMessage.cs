using App.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.Commands
{
    public class DeleteOrganizationCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
    }
}
