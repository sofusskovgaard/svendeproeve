using ProtoBuf;

namespace App.Services.Tickets.Common.Records;

[ProtoContract]
public class TicketBooking
{
    [ProtoMember(1)]
    public string Recipient { get; set; }

    [ProtoMember(2)]
    public string ProductId { get; set; }
}