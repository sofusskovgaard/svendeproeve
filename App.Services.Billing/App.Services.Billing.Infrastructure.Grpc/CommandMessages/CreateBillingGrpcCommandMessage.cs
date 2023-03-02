using App.Infrastructure.Grpc;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Billing.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class CreateBillingGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string OrderId { get; set; }

    }
}
