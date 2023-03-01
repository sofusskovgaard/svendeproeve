﻿using App.Data;
using App.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Data.Entities
{
    [CollectionDefinition(nameof(TicketEntity))]

    public class TicketEntity : BaseEntity
    {
        public string ProductId { get; set; }

        public string Recipient { get; set; }

        public string Status { get; set; }
    }
}