﻿using App.Infrastructure.Grpc;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetProductsGrpcCommandMessage : IGrpcCommandMessage
    {

    }
}