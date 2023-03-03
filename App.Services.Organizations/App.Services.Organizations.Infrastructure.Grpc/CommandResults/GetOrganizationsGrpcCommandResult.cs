﻿using App.Infrastructure.Grpc;
using App.Services.Organizations.Common.Dtos;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandResults
{
    [ProtoContract]
    public class GetOrganizationsGrpcCommandResult : IGrpcCommandResult
    {
        [ProtoMember(1)]
        public GrpcCommandResultMetadata Metadata { get; set; }

        [ProtoMember(2)]
        public OrganizationDto[] OrganizationDtos { get; set; }
    }
}