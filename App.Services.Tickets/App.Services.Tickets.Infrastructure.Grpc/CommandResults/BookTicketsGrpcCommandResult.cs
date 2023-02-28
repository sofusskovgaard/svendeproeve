using App.Infrastructure.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandResults
{
    public class BookTicketsGrpcCommandResult : IGrpcCommandResult
    {
        public GrpcCommandResultMetadata Metadata { get; set; }
    }
}
