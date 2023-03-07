using ProtoBuf;

namespace App.Services.Orders.Common.Dtos;

[ProtoContract]
public class OrderDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string UserId { get; set; }

    [ProtoMember(3)]
    public decimal Total { get; set; }

    [ProtoMember(4)]
    public string Status { get; set; }

    [ProtoMember(5)]
    public string? ChargeId { get; set; }

    [ProtoMember(6)]
    public OrderLine[] OrderLines { get; set; }

    [ProtoContract]
    public class OrderLine
    {
        [ProtoMember(1)]
        public string? ReferenceId { get; set; }

        [ProtoMember(2)]
        public string? ReferenceType { get; set; }

        [ProtoMember(3)]
        public string ProductId { get; set; }

        [ProtoMember(4)]
        public int Quantity { get; set; }

        [ProtoMember(5)]
        public decimal Price { get; set; }
    }
}