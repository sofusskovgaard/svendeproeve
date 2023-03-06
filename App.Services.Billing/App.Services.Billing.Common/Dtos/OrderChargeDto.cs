using ProtoBuf;

namespace App.Services.Billing.Common.Dtos;

[ProtoContract]
[ProtoInclude(5, typeof(OrderChargeDetailedDto))]
public class OrderChargeDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Status { get; set; }

    [ProtoMember(3)]
    public decimal Amount { get; set; }

    [ProtoMember(4)]
    public string? TransactionNumber { get; set; }
}

[ProtoContract]
public class OrderChargeDetailedDto
{
    [ProtoMember(1)]
    public string OrderId { get; set; }
}