using App.Infrastructure.Grpc;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetOrganizationsByAddressCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string Address { get; set; }
    }
}
