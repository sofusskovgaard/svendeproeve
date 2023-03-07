using ProtoBuf;

namespace App.Services.Events.Common.Dtos;

[ProtoContract]
public class EventDto
{
    [ProtoMember(1)]
    public string EventName { get; set; }

    [ProtoMember(2)]
    public string Location { get; set; }

    [ProtoMember(3)]
    public IEnumerable<string> Tournaments { get; set; }

    [ProtoMember(4)]
    public DateTime StartDate { get; set; }

    [ProtoMember(5)]
    public DateTime EndDate { get; set; }
}