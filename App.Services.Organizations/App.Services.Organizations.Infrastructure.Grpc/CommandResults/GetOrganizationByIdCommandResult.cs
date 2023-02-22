using App.Infrastructure.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandResults
{
    public class GetOrganizationByIdCommandResult : IGrpcCommandResult
    {
        public GrpcCommandResultMetadata Metadata { get; set; }
    }
}
