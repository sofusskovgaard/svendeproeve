using App.Infrastructure.Grpc;
using App.Services.Billing.Common.Dtos;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Billing.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class CreateBillingGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public BillingDto Billing { get; set; }
    }
}
