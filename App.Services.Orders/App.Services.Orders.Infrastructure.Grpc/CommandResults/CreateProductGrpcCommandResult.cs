﻿using App.Infrastructure.Grpc;
using App.Services.Orders.Common.Dtos;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class CreateProductGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }
        [ProtoMember(2)]
        public ProductDto Product{ get; set; }
    }
}
