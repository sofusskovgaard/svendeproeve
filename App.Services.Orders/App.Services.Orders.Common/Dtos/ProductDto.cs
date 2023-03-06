using ProtoBuf;

namespace App.Services.Orders.Common.Dtos;

[ProtoContract]
public class ProductDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string Description { get; set; }

    [ProtoMember(4)]
    public double Price { get; set; }

    [ProtoMember(5)]
    public string? ReferenceId { get; set; }

    [ProtoMember(6)]
    public string? ReferenceType { get; set; }
}