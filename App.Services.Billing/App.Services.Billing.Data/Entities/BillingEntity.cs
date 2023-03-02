using App.Data;
using App.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Billing.Data.Entities
{
    [CollectionDefinition(nameof(BillingEntity))]
    public class BillingEntity : BaseEntity
    {
        public string OrderId { get; set; }

        public string TransactionId { get; set; }
    }
}
