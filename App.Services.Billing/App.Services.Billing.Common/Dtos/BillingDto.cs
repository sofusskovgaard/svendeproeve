using ProtoBuf;

namespace App.Services.Billing.Common.Dtos
{
    [ProtoContract]
    public class BillingDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string OrderId { get; set; }

        [ProtoMember(3)]
        public string TransactionId { get; set; }
    }
}
