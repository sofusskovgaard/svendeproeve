using ProtoBuf;
using App.Common.Grpc;

namespace App.Infrastructure.Grpc
{
    public interface IUpdateEntityGrpcCommandMessage : IGrpcCommandMessage
    {
        public UpdatePropertyObject[] Properties { get; set; }
    }

    [ProtoContract]
    public class UpdatePropertyObject {

        [ProtoMember(1)]
        public string Key { get; set; }

        [ProtoMember(2)]
        public string Value { get; set; }
    }
}
