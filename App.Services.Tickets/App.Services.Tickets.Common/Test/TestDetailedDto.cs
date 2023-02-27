using ProtoBuf;

namespace App.Services.Tickets.Common.Test
{
    [ProtoContract]
    public class TestDetailedDto : TestDto
    {
        [ProtoMember(1)]
        public string SomeDetailedProperty { get; set; }
    }
}