using ProtoBuf;

namespace Template.Services.ServiceName.Common.Test;

[ProtoContract]
public class TestDetailedDto : TestDto
{
    [ProtoMember(1)]
    public string SomeDetailedProperty { get; set; }
}