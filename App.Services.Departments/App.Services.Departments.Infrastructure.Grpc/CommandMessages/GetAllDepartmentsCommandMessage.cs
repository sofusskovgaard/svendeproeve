﻿using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetAllDepartmentsCommandMessage : IGrpcCommandMessage
    {

    }
}
