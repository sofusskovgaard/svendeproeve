using ProtoBuf;

namespace App.Services.Billing.Common.Dtos;

[ProtoContract]
[ProtoInclude(5, typeof(UserCardDetailedDto))]
public class UserCardDto
{
    [ProtoMember(1)]
    public string Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string CardType { get; set; }

    [ProtoMember(4)]
    public string Mask { get; set; }
}

[ProtoContract]
public class UserCardDetailedDto : UserCardDto
{
    [ProtoMember(1)]
    public string UserId { get; set; }

    [ProtoMember(2)]
    public string ExternalId { get; set; }
}