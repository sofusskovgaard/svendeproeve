using ProtoBuf;

namespace App.Services.Orders.Common.Dtos
{
    [ProtoContract]
    public class OrderDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string UserId { get; set; }

        [ProtoMember(3)]
        public double Total { get; set; }

        [ProtoMember(4)]
        public OrderLine[] OrderLines { get; set; }

        [ProtoContract]
        public class OrderLine
        {
            [ProtoMember(1)]
            public string TicketId { get; set; }

            [ProtoMember(2)]
            public int Quantity { get; set; }

            [ProtoMember(3)]
            public decimal Price { get; set; }
        }
    }
}
